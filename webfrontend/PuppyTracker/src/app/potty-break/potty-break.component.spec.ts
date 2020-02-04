import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PottyBreakComponent } from './potty-break.component';

describe('PottyBreakComponent', () => {
  let component: PottyBreakComponent;
  let fixture: ComponentFixture<PottyBreakComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PottyBreakComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PottyBreakComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
