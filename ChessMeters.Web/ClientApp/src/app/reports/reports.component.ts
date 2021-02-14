import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faEdit, faEye, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';

import { Report, ReportAnalyzedGame } from './reports.models';
import { ReportsService } from './reports.service';

@Component({
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  public reports: Report[];
  public page: number;
  public pageSize: number;
  public faTrashAlt = faTrashAlt;
  public faEdit = faEdit;
  public faEye = faEye;

  constructor(private reportsService: ReportsService, private toastrService: ToastrService, private router: Router) {
    this.page = 1;
    this.pageSize = 10;
  }

  ngOnInit(): void {
    this.getAllReports();
    this.reportsService.getReportAnalyzedGames().subscribe((reportAnalyzedGame: ReportAnalyzedGame) => {
      let report = this.reports?.find(x => x.id == reportAnalyzedGame.reportId);
      if (!report)
        return;
      if (reportAnalyzedGame.analyzedGames > report.numberOfGames)
        report.analyzedGames = report.numberOfGames;
      else if (report.analyzedGames < reportAnalyzedGame.analyzedGames)
        report.analyzedGames = reportAnalyzedGame.analyzedGames;

      if (reportAnalyzedGame.analyzeErrorGames > report.numberOfGames)
        report.analyzeErrorGames = report.numberOfGames;
      else if (report.analyzeErrorGames < reportAnalyzedGame.analyzeErrorGames)
        report.analyzeErrorGames = reportAnalyzedGame.analyzeErrorGames;
    });
  }

  public openReportGenerator(): void {
    this.router.navigateByUrl('/reports/generate');
  }

  public openEditor(id: number): void {
    this.router.navigateByUrl(`/reports/edit/${id}`);
  }

  public delete(report: Report): void {
    if (!confirm(`Are you sure you want to delete ${report.description}?`))
      return;
    this.reportsService.delete(report.id).subscribe(() => {
      this.getAllReports()
    }, () => {
      this.toastrService.error('An error occurred while trying to delete your report, please try again later.');
    });
  }

  public view(id: number): void {
    this.router.navigateByUrl(`/reports/${id}`);
  }

  public getReportStatus(report: Report): string {
    if (report.analyzedGames == report.numberOfGames)
      return 'Ready';
    let analyzedGamesPercentage = Math.round(report.analyzedGames * 100 / report.numberOfGames);
    let errorPercentage = Math.round(report.analyzeErrorGames * 100 / report.numberOfGames);
    return `${analyzedGamesPercentage}% games analyzed, ${errorPercentage}% games errored while analyzing,
            ${100 - analyzedGamesPercentage - errorPercentage}% still pending`;
  }

  private getAllReports(): void {
    this.reportsService.getAll().subscribe(result => {
      this.reports = result;
    }, () => {
      this.toastrService.error('An error occurred while trying to fetch your reports, please try again later.');
    });
  }
}
