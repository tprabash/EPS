import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {
  IgxColumnComponent,
  IgxGridComponent,
  IgxComboComponent,
  IComboSelectionChangeEventArgs,
} from 'igniteui-angular';
import { MasterService } from '_services/master.service';
import { AccountService } from '_services/account.service';

@Component({
  selector: 'app-item-master',
  templateUrl: './item-master.component.html',
  styleUrls: ['./item-master.component.css']
})
export class ItemMasterComponent implements OnInit {

  masterItemForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  masterCompanyList = [];
  categoryList = [];
  subcategoryList = [];
  sizeList = [];
  printList = [];

  public col : IgxColumnComponent;
  public oWidth : string;
  public nWidth : string;
  public ogIdx : BigInteger;
  masterCustomerList: any[];
  ccDisableButton: boolean = false;
  masterCategoryList: any[];

  @ViewChild('masteritemGrid' , {static : true})
  public masteritemGrid : IgxGridComponent;
  @ViewChild('category' , {static : true})
  public category : IgxComboComponent;
  @ViewChild('subcategory' , {static : true})
  public subcategory : IgxComboComponent;
  @ViewChild('size' , {static : true})
  public size : IgxComboComponent;
  @ViewChild('print' , {static : true})
  public print : IgxComboComponent;
  masterSubCategoryList: any[];
  mastersizeList: any[];
  masterPrintList: any[];
  masterItemList: any[];

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private masterService: MasterService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadCategoryList();
    this.loadSubCategoryList();
    this.loadSizeList();
    this.loadPrintList();
    this.loadItemList();
  }
  initializeForm() {
    this.accountService.currentUser$.forEach((element) => {
      this.user = element;
    });
    var authMenus = this.user.permitMenus;

    if (authMenus != null) {
      if (authMenus.filter((x) => x.autoIdx == 2201).length > 0) {
        this.saveButton = true;
      }
      //console.log(this.saveButton);
    }
    this.masterItemForm = this.fb.group({
      autoId: [0],
      createUserId: this.user.userId,
      code: ['', [Validators.required, Validators.maxLength(50)]],
      itemname: ['', [Validators.required, Validators.maxLength(50)]],
      barcode: ['', [Validators.required, Validators.maxLength(50)]],
      category: ['', [Validators.required, Validators.maxLength(50)]],
      subcategory: ['', [Validators.required, Validators.maxLength(50)]],
      size: ['', [Validators.required, Validators.maxLength(50)]],
      print: ['', [Validators.required, Validators.maxLength(50)]],
      buyingrate: ['', [Validators.required, Validators.maxLength(50)]],
      sellingrate: ['', [Validators.required, Validators.maxLength(50)]],
      rol: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  public onResize(event){
    this.col = event.column;
    this.oWidth = event.prevWidth;
    this.nWidth = event.newWidth
  }


  loadCategoryList(){
    this.masterCategoryList = [];
    var objOG = {
      ActivityNo: 3
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterCategoryList = OpGroupList;
      console.log(OpGroupList);
    });

  }

  loadSubCategoryList(){
    this.masterSubCategoryList = [];
    var objOG = {
      ActivityNo: 12
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterSubCategoryList = OpGroupList;
      console.log(OpGroupList);
    });

  }

  loadSizeList(){
    this.mastersizeList = [];
    var objOG = {
      ActivityNo: 22
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.mastersizeList = OpGroupList;
      console.log(OpGroupList);
    });

  }

  loadPrintList(){
    this.masterPrintList = [];
    var objOG = {
      ActivityNo: 32
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterPrintList = OpGroupList;
      console.log(OpGroupList);
    });

  }

  loadItemList(){
    this.masterItemList = [];
    var objOG = {
      ActivityNo: 61
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterItemList = OpGroupList;
      console.log(OpGroupList);
    });

  }
  
  
  companysave() {
    if (this.saveButton == true) {
      var Obj = {};
      //console.log(Obj);
    } else {
      this.toaster.error('Save Permission Denied !!!');
    }
  }

  refreshcustomer() {}

  onEditCompany(event, cellId) {}

  onCustomerSearch(event) {
    for (const item of event.added) {
     
    }
  }
  singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

}
