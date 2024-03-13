import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import {
  IComboSelectionChangeEventArgs,
  IgxColumnComponent,
  IgxComboComponent,
  IgxGridComponent,
} from 'igniteui-angular';
import { ToastrService } from 'ngx-toastr';
import { Color } from 'src/app/_models/color';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-color-tab',
  templateUrl: './color-tab.component.html',
  styleUrls: ['./color-tab.component.css'],
})
export class ColorTabComponent implements OnInit {
  assignColorForm: FormGroup;
  articleList: any[];
  pColorList: Color[];
  npColorList: Color[];
  acSaveButton: boolean = false;
  acRemoveButton: boolean = false;
  user: User;

  @ViewChild('carticle', { read: IgxComboComponent })
  public carticle: IgxComboComponent;

  @ViewChild('npColorGrid', { static: true })
  public npColorGrid: IgxGridComponent;
  @ViewChild('pColorGrid', { static: true })
  public pColorGrid: IgxGridComponent;

  public col: IgxColumnComponent;
  public pWidth: string;
  public nWidth: string;
  articleRowID: string;

  constructor(
    private fb: FormBuilder,

    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initilizeForm();
    this.loadArticles();
    this.DefaultSelection();
  }

  initilizeForm() {
 
  }

  //// ALOW SINGLE SILECTION ONLY COMBO EVENT
  singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

  public onResize(event) {
    this.col = event.column;
    this.pWidth = event.prevWidth;
    this.nWidth = event.newWidth;
  }

  loadArticles() {
   
  }

  onSelectArticle(event) {
  
  }

  //// loads both permited and not permited color list
  loadColorDetails(articleId) {

  }

  saveArticleColor() {
 
  }

  deleteArticleColor() {
 
  }

  clearGridDetails() {
    this.npColorGrid.deselectAllRows();
    this.pColorGrid.deselectAllRows();
    this.npColorList = [];
    this.pColorList = [];
  }

  DefaultSelection() {}
}
