import { Component } from '@angular/core';
import { PieceIconInput } from 'ngx-chess-board';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  public pieceIcons: PieceIconInput;

  constructor(private toastrService: ToastrService) {
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

  public analyzeGame(): void {
    this.toastrService.error('Not implemented yet');
  }
}
