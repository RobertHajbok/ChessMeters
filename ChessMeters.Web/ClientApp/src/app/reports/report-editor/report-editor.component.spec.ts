import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportEditor } from './report-details.component';

describe('ReportDetailsComponent', () => {
  let component: ReportEditor;
  let fixture: ComponentFixture<ReportEditor>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportEditor ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportEditor);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
