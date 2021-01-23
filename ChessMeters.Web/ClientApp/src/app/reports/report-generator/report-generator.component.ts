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
}
