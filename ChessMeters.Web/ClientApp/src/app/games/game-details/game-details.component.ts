import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faAngleDoubleLeft, faAngleDoubleRight, faArrowLeft, faArrowRight, faSync } from '@fortawesome/free-solid-svg-icons';
import { NgxChessBoardView } from 'ngx-chess-board';
import { ToastrService } from 'ngx-toastr';

import { GameDetails } from '../games.models';
import { GamesService } from '../games.service';

@Component({
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.css']
})
export class GameDetailsComponent implements OnInit {
  public game: GameDetails;
  public chartData: any[];
  public faSync = faSync;
  public faArrowLeft = faArrowLeft;
  public faArrowRight = faArrowRight;
  public faAngleDoubleLeft = faAngleDoubleLeft;
  public faAngleDoubleRight = faAngleDoubleRight;
  public moveIndex: number;

  constructor(private gamesService: GamesService, private toastrService: ToastrService, private activatedRoute: ActivatedRoute) {
    this.moveIndex = 0;
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

  public move(chessBoard: NgxChessBoardView): void {
    chessBoard.move(this.game.treeMoves[this.moveIndex].move);
    this.moveIndex++;
  }

  public reset(chessBoard: NgxChessBoardView): void {
    chessBoard.reset();
    this.moveIndex = 0;
  }

  public undo(chessBoard: NgxChessBoardView): void {
    chessBoard.undo();
    if (this.moveIndex > 0)
      this.moveIndex--;
  }

  public goToEnd(chessBoard: NgxChessBoardView): void {
    for (var i = this.moveIndex; i < this.game.treeMoves.length; i++) {
      this.move(chessBoard);
    }
  }
}
