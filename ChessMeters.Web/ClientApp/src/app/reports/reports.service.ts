import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Report } from './reports.models';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public getAll(): Observable<Report[]> {
    return this.http.get<Report[]>(`${this.baseUrl}api/reports`);
  }
}
