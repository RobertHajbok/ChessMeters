import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faAngleDoubleLeft, faAngleDoubleRight, faArrowLeft, faArrowRight, faSync } from '@fortawesome/free-solid-svg-icons';
import { NgxChessBoardView, PieceIconInput } from 'ngx-chess-board';
import { ToastrService } from 'ngx-toastr';

import { GameDetails } from '../games.models';
import { GamesService } from '../games.service';

@Component({
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.css']
})
export class GameDetailsComponent implements OnInit {
  @ViewChild('chessBoard', { static: false }) chessBoard: NgxChessBoardView;
  public game: GameDetails;
  public chartData: any[];
  public faSync = faSync;
  public faArrowLeft = faArrowLeft;
  public faArrowRight = faArrowRight;
  public faAngleDoubleLeft = faAngleDoubleLeft;
  public faAngleDoubleRight = faAngleDoubleRight;
  public moveIndex: number;
  public pieceIcons: PieceIconInput;

  constructor(private gamesService: GamesService, private toastrService: ToastrService, private activatedRoute: ActivatedRoute) {
    this.moveIndex = 0;
    this.pieceIcons = {
      blackBishopUrl: 'assets/blackBishop.svg',
      blackKingUrl: 'assets/blackKing.svg',
      blackKnightUrl: 'assets/blackKnight.svg',
      blackPawnUrl: 'assets/blackPawn.svg',
      blackQueenUrl: 'assets/blackQueen.svg',
      blackRookUrl: 'assets/blackRook.svg',
      whiteBishopUrl: 'assets/whiteBishop.svg',
      whiteKingUrl: 'assets/whiteKing.svg',
      whiteKnightUrl: 'assets/whiteKnight.svg',
      whitePawnUrl: 'assets/whitePawn.svg',
      whiteQueenUrl: 'assets/whiteQueen.svg',
      whiteRookUrl: 'assets/whiteRook.svg'
    };
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

  @HostListener('window:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key == 'ArrowRight') {
      this.move();
    } else if (event.key == 'ArrowLeft') {
      this.undo();
    }
  }

  public move(): void {
    if (this.moveIndex == this.game.treeMoves.length)
      return;
    this.chessBoard.move(this.game.treeMoves[this.moveIndex].move);
    this.moveIndex++;
  }

  public reset(): void {
    this.chessBoard.reset();
    this.moveIndex = 0;
  }

  public undo(): void {
    this.chessBoard.undo();
    if (this.moveIndex > 0)
      this.moveIndex--;
  }

  public goToEnd(): void {
    for (var i = this.moveIndex; i < this.game.treeMoves.length; i++) {
      this.move();
    }
  }

  public zeroNgStyle(tick) {
    return tick == 0 ? { stroke: '#900' } : null;
  }
}
