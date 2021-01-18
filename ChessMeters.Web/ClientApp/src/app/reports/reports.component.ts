import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { Report } from './reports.models';
import { ReportsService } from './reports.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html'
})
export class ReportsComponent implements OnInit {
  public reports: Report[];

  constructor(private reportsService: ReportsService, private toastrService: ToastrService, private router: Router) {
  }

  ngOnInit(): void {
    this.reportsService.getAll().subscribe(result => {
      this.reports = result;
    }, () => {
      this.toastrService.error('An error occurred while trying to fetch your reports, please try again later.');
    });
  }

  public openReportGenerator(): void {
    this.router.navigateByUrl('/reports/generate');
  }
}
