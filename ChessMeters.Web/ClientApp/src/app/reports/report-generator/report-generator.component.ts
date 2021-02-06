import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { faExchangeAlt, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';

import { Color, GamePreview } from '../../games/games.models';
import { GenerateReport } from '../reports.models';
import { ReportsService } from '../reports.service';

@Component({
  templateUrl: './report-generator.component.html'
})
export class ReportGeneratorComponent {
  public report: GenerateReport;
  public uploadedPGN: File;
  public lichessUsername: string;
  public chessComUsername: string;
  public games: GamePreview[];
  public faTrashAlt = faTrashAlt;
  public faExchangeAlt = faExchangeAlt;
  public page: number;
  public pageSize: number;
  public color = Color;

  constructor(private reportsService: ReportsService, private toastrService: ToastrService, private router: Router) {
    this.report = { description: '', pgn: '' };
    this.page = 1;
    this.pageSize = 5;
  }

  public generate(): void {
    this.reportsService.generate(this.report).subscribe(() => {
      this.toastrService.success('Report is generated in the background, we will notify you when it is ready.');
      this.router.navigateByUrl('/reports');
    }, () => {
      this.toastrService.error('An error occurred while trying to generate your report, please try again later.');
    });
  }

  public getLichessPGN(): void {
    this.reportsService.getLichessGames(this.lichessUsername).subscribe((pgn) => {
      this.report.pgn = pgn;
      this.games = this.reportsService.parsePGNForPreview(this.report.pgn);
      this.games.forEach(game => game.userColor = game.white.toUpperCase() == this.lichessUsername.toUpperCase() ? Color.White : Color.Black);
      this.toastrService.success('Lichess games successfully downloaded, PGN field populated with the games.');
    }, () => {
      this.toastrService.error('An error occurred while trying to get your Lichess games, please try again later.');
    });
  }

  public getChessComPGN(): void {
    this.reportsService.getChessComGames(this.chessComUsername).subscribe((pgn) => {
      this.report.pgn = pgn;
      this.games = this.reportsService.parsePGNForPreview(this.report.pgn);
      this.games.forEach(game => game.userColor = game.white.toUpperCase() == this.chessComUsername.toUpperCase() ? Color.White : Color.Black);
      this.toastrService.success('Chess.com games successfully downloaded, PGN field populated with the games.');
    }, () => {
      this.toastrService.error('An error occurred while trying to get your chess.com games, please try again later.');
    });
  }

  public fileChanged(e: File[]): void {
    this.uploadedPGN = e[0];
  }

  public loadPGNFromFile(): void {
    let fileReader = new FileReader();
    fileReader.onload = () => {
      this.report.pgn = <string>fileReader.result;
      this.games = this.reportsService.parsePGNForPreview(this.report.pgn);
    }
    fileReader.readAsText(this.uploadedPGN);
  }

  public updateGamesPreview(): void {
    this.games = this.reportsService.parsePGNForPreview(this.report.pgn);
  }

  public removeGameFromPreview(index: number): void {
    this.report.pgn = this.reportsService.removeGameFromPGN(this.report.pgn, this.games, index);
  }

  public changeUserColorForGamePreview(index: number): void {
    this.games[index].userColor = this.games[index].userColor == Color.White ? Color.Black : Color.White;
  }
}
