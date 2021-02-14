import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UserLinkedAccounts } from './users.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public getUserLinkedAccounts(): Observable<UserLinkedAccounts> {
    return this.http.get<UserLinkedAccounts>(`${this.baseUrl}api/users/getLinkedAccounts`);
  }
}
