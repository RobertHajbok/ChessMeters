import { Component, OnInit } from '@angular/core';
import { faChessKnight, faChessRook, faChessKing, faPlus, faMinus } from '@fortawesome/free-solid-svg-icons';

@Component({
  templateUrl: './how-it-works-section.component.html',
  selector: 'how-it-works-section',
  styleUrls: ['./how-it-works-section.component.scss']
})

export class HowItWorksSectionComponent implements OnInit {
  public faChessKing = faChessKing;
  public faChessKnight = faChessKnight;
  public faChessRook = faChessRook;
  public faPlus = faPlus;
  public faMinus = faMinus;

  ngOnInit(): void {
  }
}
