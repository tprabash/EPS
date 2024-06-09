import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GatePassComponent } from './gate-pass.component';

describe('GatePassComponent', () => {
  let component: GatePassComponent;
  let fixture: ComponentFixture<GatePassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GatePassComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GatePassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
