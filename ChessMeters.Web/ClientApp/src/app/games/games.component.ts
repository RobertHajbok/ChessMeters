import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

import { Game } from './games.models';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html'
})
export class GamesComponent {
  @Input() games: Game[];
  public page: number;
  public pageSize: number;

  constructor(private router: Router) {
    this.page = 1;
    this.pageSize = 5;
  }

  public view(id: number): void {
    this.router.navigateByUrl(`/games/${id}`);
  }
}
