import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';

import { EditReport, GenerateReport, Report, ReportDetails } from './reports.models';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  private hubConnection: signalR.HubConnection;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router,
    private toastrService: ToastrService, private authorizeService: AuthorizeService) {
    this.startSignalrConnection();
  }

  public getAll(): Observable<Report[]> {
    return this.http.get<Report[]>(`${this.baseUrl}api/reports/getAll`);
  }

  public generate(report: GenerateReport): Observable<any> {
    const headerPost: HttpHeaders = new HttpHeaders();
    headerPost.set('Content-type', 'application/json');
    return this.http.post(`${this.baseUrl}api/reports`, report, { headers: headerPost });
  }

  public edit(report: EditReport): Observable<any> {
    const headerPut: HttpHeaders = new HttpHeaders();
    headerPut.set('Content-type', 'application/json');
    return this.http.put(`${this.baseUrl}api/reports`, report, { headers: headerPut });
  }

  public getForEdt(id: number): Observable<EditReport> {
    return this.http.get<Report>(`${this.baseUrl}api/reports/getForEdit/${id}`);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/reports/delete/${id}`);
  }

  public getDetails(id: number): Observable<ReportDetails> {
    return this.http.get<ReportDetails>(`${this.baseUrl}api/reports/getDetails/${id}`);
  }

  private startSignalrConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(`${this.baseUrl}notification`, {
      accessTokenFactory: () => this.authorizeService.getAccessToken().toPromise()
    }).build();
    this.hubConnection.start();
    this.addReportGeneratedListener();
  }

  private addReportGeneratedListener(): void {
    this.hubConnection.on('reportGenerated', (reportId) => {
      this.toastrService.success('Report successfully generated, you can click here to view it.').onTap.subscribe(() => {
        this.router.navigateByUrl(`/reports/${reportId}`);
      });
    });
  }
}
