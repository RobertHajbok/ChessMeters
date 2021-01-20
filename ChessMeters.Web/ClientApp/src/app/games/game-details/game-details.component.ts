import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GameDetails } from '../games.models';
import { GamesService } from '../games.service';

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html'
})
export class GameDetailsComponent implements OnInit {
  public game: GameDetails;
  public chartData: any[];

  constructor(private gamesService: GamesService, private toastrService: ToastrService, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.gamesService.getDetails(+params.id).subscribe(result => {
        this.game = result;
        this.chartData = [{
          name: 'Stockfish',
          series: result.treeMoves.map((element, index) => ({
            name: index + 1,
            value: element.stockfishEvaluationCentipawns
          }))
        }];
      }, () => {
        this.toastrService.error('An error occurred while trying to fetch the game details, please try again later.');
      });
    });
  }
}
