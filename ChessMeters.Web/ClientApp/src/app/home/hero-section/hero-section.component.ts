import { Component, OnInit } from '@angular/core';
import { faChessKnight, faChessRook, faChessKing, faPlus, faMinus } from '@fortawesome/free-solid-svg-icons';

@Component({
  templateUrl: './hero-section.component.html',
  selector: 'hero-section',
  styleUrls: ['./hero-section.component.scss']
})

export class HeroSectionComponent implements OnInit {
  public faChessKing = faChessKing;
  public faChessKnight = faChessKnight;
  public faChessRook = faChessRook;
  public faPlus = faPlus;
  public faMinus = faMinus;

  ngOnInit(): void {
  }
}
