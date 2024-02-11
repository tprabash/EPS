import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SizeMasterComponent } from './size-master.component';

describe('SizeMasterComponent', () => {
  let component: SizeMasterComponent;
  let fixture: ComponentFixture<SizeMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SizeMasterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SizeMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
