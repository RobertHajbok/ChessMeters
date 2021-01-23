import { Component, OnInit } from '@angular/core';

import { ReportsService } from './reports/reports.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(private reportsService: ReportsService) {
  }

  ngOnInit(): void {
    this.reportsService.startSignalrConnection();
  }
}
