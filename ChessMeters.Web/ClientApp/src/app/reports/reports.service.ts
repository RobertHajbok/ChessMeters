import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { EditReport, GenerateReport, Report, ReportDetails } from './reports.models';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
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
}
