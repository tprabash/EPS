import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IssuetoProductionComponent } from './issueto-production.component';

describe('IssuetoProductionComponent', () => {
  let component: IssuetoProductionComponent;
  let fixture: ComponentFixture<IssuetoProductionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IssuetoProductionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IssuetoProductionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
