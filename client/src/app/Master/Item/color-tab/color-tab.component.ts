import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AccountService } from '_services/account.service';
import { MasterService } from '_services/master.service';
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
  asSaveButton: boolean;
  asRemoveButton: boolean;
  masterItemList: any[];
  SizeList: any[];
  itemidx: any;
  allocPrintList: any[];
  notallocPrintList: any[];

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private masterService: MasterService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initilizeForm();
    this.loadArticles();
    this.DefaultSelection();
  }

  initilizeForm() {
    this.accountService.currentUser$.forEach((element) => {
      this.user = element;
      //console.log(this.user.userId);
    });

    var authMenus = this.user.permitMenus;

    if (authMenus != null) {
      if (authMenus.filter((x) => x.autoIdx == 136).length > 0) {
        this.asSaveButton = true;
      }
      if (authMenus.filter((x) => x.autoIdx == 137).length > 0) {
        this.asRemoveButton = true;
      }
    }

    this.assignColorForm = this.fb.group({
      userId: this.user.userId,     
      carticle: [''],     
      size: ['']     
    });   
 
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

  loadArticles(){
    this.masterItemList = [];
    var objOG = {
      ActivityNo: 62
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterItemList = OpGroupList;
      console.log('xxxxxxxxxxxxx');
      console.log(OpGroupList);
    });
  }
  
  
  onSelectArticle(event) {    
    for (const item of event.added) {
      this.loadSizeDetails(item);
    }
  }

  onSelectSize(event) {    
    for (const item of event.added) {
      this.loadAlloPrintDetails(item);
      this.loadNotAllocPrintDetails(item);
    }
  }


  loadAlloPrintDetails(item) {
    this.allocPrintList = [];
    this.itemidx = item;
    var objOG = {
      ActivityNo: 82,
      f01:this.itemidx
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.allocPrintList = OpGroupList;
      console.log(OpGroupList);
    });
  }

  loadNotAllocPrintDetails(item) {
    this.notallocPrintList = [];
    this.itemidx = item;
    var objOG = {
      ActivityNo: 81,
      f01:this.itemidx
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.notallocPrintList = OpGroupList;
      console.log(OpGroupList);
    });
  }

  loadSizeDetails(item) {
    this.SizeList = [];
    this.itemidx = item;
    var objOG = {
      ActivityNo: 80,
      f01:this.itemidx
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.SizeList = OpGroupList;
      console.log(OpGroupList);
    });
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
