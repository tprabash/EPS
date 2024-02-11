import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubCategoryMasterComponent } from './sub-category-master.component';

describe('SubCategoryMasterComponent', () => {
  let component: SubCategoryMasterComponent;
  let fixture: ComponentFixture<SubCategoryMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubCategoryMasterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubCategoryMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
