import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { GenerateReport, Report } from './reports.models';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public getAll(): Observable<Report[]> {
    return this.http.get<Report[]>(`${this.baseUrl}api/reports`);
  }

  public generate(report: GenerateReport): Observable<any> {
    const headerPost: HttpHeaders = new HttpHeaders();
    headerPost.set('Content-type', 'application/json');
    return this.http.post(`${this.baseUrl}api/reports`, report, { headers: headerPost });
  }
}
