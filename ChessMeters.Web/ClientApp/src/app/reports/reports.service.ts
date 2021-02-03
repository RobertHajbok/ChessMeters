import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

import { AuthorizeService } from '../../api-authorization/authorize.service';
import { GamePreview } from '../games/games.models';
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
    const headers: HttpHeaders = new HttpHeaders();
    headers.set('Content-type', 'application/json');
    return this.http.put(`${this.baseUrl}api/reports`, report, { headers: headers });
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

  public getLichessGames(username: string): Observable<any> {
    return this.http.get(`https://lichess.org/api/games/user/${username}?max=20`, { responseType: 'text' });
  }

  public getChessComGames(username: string): Observable<any> {
    const date = new Date();
    return this.http.get(`https://api.chess.com/pub/player/${username}/games/${date.getUTCFullYear()}/${date.getUTCMonth()}/pgn`, { responseType: 'text' });
  }

  public parsePGNForPreview(pgn: string): GamePreview[] {
    var games = [];
    let currentGame: GamePreview;
    currentGame = this.getInitializedGamePreview();
    pgn.split('\n').forEach((line) => {
      if (line.startsWith('[Event "')) {
        let event = line.substring('[Event "'.length);
        currentGame.event = event.substring(0, event.length - 2);
      } else if (line.startsWith('[White "')) {
        let white = line.substring('[White "'.length);
        currentGame.white = white.substring(0, white.length - 2);
      } else if (line.startsWith('[Black "')) {
        let black = line.substring('[Black "'.length);
        currentGame.black = black.substring(0, black.length - 2);
      } else if (line.endsWith('1-0') || line.endsWith('1/2-1/2') || line.endsWith('0-1')) {
        currentGame.result = line.split(' ').pop();
        currentGame.moves = line.substring(0, line.lastIndexOf(' '));
        games.push(currentGame);
        currentGame = this.getInitializedGamePreview();
      }
    });
    return games;
  }

  public removeGameFromPGN(pgn: string, games: GamePreview[], index: number): string {
    let gamesSplit = pgn.split('1-0\n').join('%$!').split('1/2-1/2\n').join('%$!').split('0-1\n').join('%$!').split('%$!');
    gamesSplit.splice(index, 1);
    games.splice(index, 1);
    for (let i = 0; i < games.length; i++) {
      gamesSplit[i] += games[i].result;
    }
    let newPGN = gamesSplit.join('\n');
    while (newPGN.startsWith('\n')) {
      newPGN = newPGN.substr(1);
    }
    return newPGN
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

  private getInitializedGamePreview(): GamePreview {
    return {
      white: '',
      black: '',
      moves: '',
      event: '',
      result: ''
    };
  }
}
