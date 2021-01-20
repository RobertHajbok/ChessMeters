import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ReportDetails } from '../reports.models';

import { ReportsService } from '../reports.service';

@Component({
  selector: 'app-report-details',
  templateUrl: './report-details.component.html'
})
export class ReportDetailsComponent implements OnInit {
  public report: ReportDetails;

  constructor(private reportsService: ReportsService, private toastrService: ToastrService, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.reportsService.getDetails(+params.id).subscribe(result => {
        this.report = result;
      }, () => {
        this.toastrService.error('An error occurred while trying to fetch your report, please try again later.');
      });
    });
  }
}
