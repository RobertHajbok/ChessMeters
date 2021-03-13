import { Component, OnInit } from '@angular/core';
import { faChessKnight, faChessRook, faChessKing, faPlus, faMinus } from '@fortawesome/free-solid-svg-icons';

@Component({
  templateUrl: './faq-section.component.html',
  selector: 'faq-section',
})

export class FaqSectionComponent implements OnInit {
  public faChessKing = faChessKing;
  public faChessKnight = faChessKnight;
  public faChessRook = faChessRook;
  public faPlus = faPlus;
  public faMinus = faMinus;

  public accordionClass = 'faq-accordion';

  ngOnInit(): void {
  }
}
