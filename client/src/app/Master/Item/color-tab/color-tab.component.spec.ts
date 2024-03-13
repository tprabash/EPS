import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorTabComponent } from './color-tab.component';

describe('ColorTabComponent', () => {
  let component: ColorTabComponent;
  let fixture: ComponentFixture<ColorTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ColorTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ColorTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
