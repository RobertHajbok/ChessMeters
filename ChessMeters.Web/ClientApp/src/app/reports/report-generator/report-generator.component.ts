import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { GenerateReport } from '../reports.models';
import { ReportsService } from '../reports.service';

@Component({
  selector: 'app-report-generator',
  templateUrl: './report-generator.component.html'
})
export class ReportGeneratorComponent {
  public report: GenerateReport;
  public uploadedPGN: File;
  public lichessUsername: string;
  public chessComUsername: string;

  constructor(private reportsService: ReportsService, private toastrService: ToastrService, private router: Router) {
    this.report = { description: '', pgn: '' };
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
      this.toastrService.success('Lichess games successfully downloaded, PGN field populated with the games.');
    }, () => {
      this.toastrService.error('An error occurred while trying to get your Lichess games, please try again later.');
    });
  }

  public getChessComPGN(): void {
    this.reportsService.getChessComGames(this.chessComUsername).subscribe((pgn) => {
      this.report.pgn = pgn;
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
    }
    fileReader.readAsText(this.uploadedPGN);
  }
}
