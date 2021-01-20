import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { EditReport } from '../reports.models';
import { ReportsService } from '../reports.service';

@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html'
})
export class ReportEditorComponent implements OnInit {
  public report: EditReport;

  constructor(private reportsService: ReportsService, private toastrService: ToastrService, private router: Router,
    private activatedRoute: ActivatedRoute) {
    this.report = { description: '' };
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.reportsService.getForEdt(+params.id).subscribe(result => {
        this.report = result;
      }, () => {
        this.toastrService.error('An error occurred while trying to fetch details for your report, please try again later.');
      });
    });
  }

  public save(): void {
    this.reportsService.edit(this.report).subscribe(() => {
      this.toastrService.success('Report successfuly saved');
      this.router.navigateByUrl('/reports');
    }, () => {
      this.toastrService.error('An error occurred while trying to edit your report, please try again later.');
    });
  }
}
