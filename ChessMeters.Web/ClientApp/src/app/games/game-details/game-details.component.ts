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
        let stockfishEvaluations = result.treeMoves.map(x => x.stockfishEvaluationCentipawns);
        var minNoMateStockfishEvaluation = Math.min(...stockfishEvaluations.filter(x => x > -15300));
        var maxNoMateStockfishEvaluation = Math.max(...stockfishEvaluations.filter(x => x < 15300));
        this.chartData = [{
          name: 'Stockfish',
          series: result.treeMoves.map((element, index) => {
            var evaluation = element.stockfishEvaluationCentipawns;
            if (evaluation < minNoMateStockfishEvaluation) {
              evaluation = minNoMateStockfishEvaluation;
            } else if (evaluation > maxNoMateStockfishEvaluation) {
              evaluation = maxNoMateStockfishEvaluation;
            }
            return ({
              name: index + 1,
              value: evaluation / 100
            })
          })
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
