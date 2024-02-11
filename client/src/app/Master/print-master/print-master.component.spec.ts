import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintMasterComponent } from './print-master.component';

describe('PrintMasterComponent', () => {
  let component: PrintMasterComponent;
  let fixture: ComponentFixture<PrintMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrintMasterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrintMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
