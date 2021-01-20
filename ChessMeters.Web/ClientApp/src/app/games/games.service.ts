import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { GameDetails } from './games.models';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public getDetails(id: number): Observable<GameDetails> {
    return this.http.get<GameDetails>(`${this.baseUrl}api/games/${id}`);
  }
}
