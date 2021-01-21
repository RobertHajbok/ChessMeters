import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Contact } from './contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public send(contact: Contact): Observable<any> {
    const headerPost: HttpHeaders = new HttpHeaders();
    headerPost.set('Content-type', 'application/json');
    return this.http.post(`${this.baseUrl}api/contact`, contact, { headers: headerPost });
  }
}
