import { Component, OnInit } from '@angular/core';

import { Report } from './reports.models';
import { ReportsService } from './reports.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html'
})
export class ReportsComponent implements OnInit {
  public reports: Report[];

  constructor(private reportsService: ReportsService) {
  }

  ngOnInit(): void {
    this.reportsService.getAll().subscribe(result => {
      this.reports = result;
    }, () => {
      alert('An error occurred while trying to fetch your reports, please try again later.');
    });
  }
}
