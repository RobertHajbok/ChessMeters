import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  constructor(private toastrService: ToastrService) {
  }

  public analyzeGame(): void {
    this.toastrService.error('Not implemented yet');
  }
}
