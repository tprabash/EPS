import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SizeTabComponent } from './size-tab.component';

describe('SizeTabComponent', () => {
  let component: SizeTabComponent;
  let fixture: ComponentFixture<SizeTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SizeTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SizeTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
