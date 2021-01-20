import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

import { Game } from './games.models';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html'
})
export class GamesComponent {
  @Input() games: Game[];

  constructor(private router: Router) {
  }

  public view(id: number): void {
    this.router.navigateByUrl(`/games/${id}`);
  }
}
