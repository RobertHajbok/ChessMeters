import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { faAngleDoubleLeft, faArrowLeft, faSync, faChessKnight, faChessRook, faChessKing, faPlus, faMinus } from '@fortawesome/free-solid-svg-icons';
import { NgxChessBoardView, PieceIconInput } from 'ngx-chess-board';
import { ToastrService } from 'ngx-toastr';
import { query, style, transition, trigger } from '@angular/animations';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  @ViewChild('chessBoard', { static: false }) chessBoard: NgxChessBoardView;
  public pieceIcons: PieceIconInput;
  public faSync = faSync;
  public faArrowLeft = faArrowLeft;
  public faAngleDoubleLeft = faAngleDoubleLeft;
  public boardSize: number;

  public faChessKing = faChessKing;
  public faChessKnight = faChessKnight;
  public faChessRook = faChessRook;
  public faPlus = faPlus;
  public faMinus = faMinus;

  public accordionClass = 'faq-accordion';

  constructor(private toastrService: ToastrService, private breakpointObserver: BreakpointObserver) {
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
    this.breakpointObserver
      .observe([Breakpoints.XSmall, Breakpoints.Small])
      .subscribe((result: BreakpointState) => {
        if (result.breakpoints[Breakpoints.XSmall]) {
          this.boardSize = 350;
        }
        else if (result.breakpoints[Breakpoints.Small]) {
          this.boardSize = 550;
        } else {
          this.boardSize = 750;
        }
      });
  }

  @HostListener('window:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key == 'ArrowLeft') {
      this.chessBoard.undo();
    }
  }

  public analyzeGame(): void {
    this.toastrService.error('Not implemented yet');
  }
}
