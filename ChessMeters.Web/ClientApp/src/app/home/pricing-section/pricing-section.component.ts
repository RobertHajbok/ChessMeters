import { Component, OnInit } from '@angular/core';
import { faChessKnight, faChessRook, faChessKing, faPlus, faMinus } from '@fortawesome/free-solid-svg-icons';

@Component({
  templateUrl: './pricing-section.component.html',
  selector: 'pricing-section',
})

export class PricingSectionComponent implements OnInit {
  public faChessKing = faChessKing;
  public faChessKnight = faChessKnight;
  public faChessRook = faChessRook;
  public faPlus = faPlus;
  public faMinus = faMinus;

  ngOnInit(): void {
  }
}
