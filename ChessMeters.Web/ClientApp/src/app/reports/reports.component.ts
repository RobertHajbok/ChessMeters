import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faEdit, faEye, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';

import { Report } from './reports.models';
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

  private getAllReports(): void {
    this.reportsService.getAll().subscribe(result => {
      this.reports = result;
    }, () => {
      this.toastrService.error('An error occurred while trying to fetch your reports, please try again later.');
    });
  }
}
