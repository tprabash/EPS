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
      code: [''],
      itemname:[''],
      barcode: [''],
      category: [''],
      subcategory: [''],
      // size: ['', [Validators.required, Validators.maxLength(50)]],
      // print: ['', [Validators.required, Validators.maxLength(50)]],
      buyingrate: [''],
      sellingrate: [''],
      rol: [''],
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
  
  
  Itemsave() {

    var SMList = [];
    var supplierData = {
      Code: this.masterItemForm.get('code').value,
      ItemName: this.masterItemForm.get('itemname').value,
      Barcode: this.masterItemForm.get('barcode').value,
      // CategoryId: 1,
      // SubCategoryId: 1,
      CategoryId: this.masterItemForm.get('category').value[0],
      SubCategoryId: this.masterItemForm.get('subcategory').value[0],
      // SizeId: this.masterItemForm.get('vatno').value,
      // PrintId: this.masterItemForm.get('svatno').value,
      buyingRate: this.masterItemForm.get('buyingrate').value,
      SellingRate: this.masterItemForm.get('sellingrate').value,
      ROL: this.masterItemForm.get('rol').value,
      AutoId: this.masterItemForm.get('autoId').value
    };


    console.log("supplierData",supplierData);
    var objSM = {
      sItem: supplierData,
      ActivityNo: 60,
      AgentNo:this.user.userId,
      ModuleNo:this.user.moduleId
    };

    SMList.push(objSM);
    
    console.log(SMList);

      this.masterService.SaveMWSMasterData(SMList).subscribe((result) => {
      // console.log(result);
      if (result['result'] == 1) {
        this.toaster.success('save Successfully !!!');
        this.loadItemList();
        this.clearControls();
      }
      else if (result['result'] == 2) {
        this.toaster.warning('update Successfully !!!');
        this.loadItemList();
        this.clearControls();
      }
      else if (result['result'] == 3) {
        this.toaster.error('Code already Exists!!!');
        this.loadItemList();
        this.clearControls();
      }
      else {
        this.toaster.warning(
          'Contact Admin. Error No:- ' + result['result'].toString()
        );
      }
    });
  
}

clearControls(){
  this.masterItemForm.get('code').setValue('');
  this.masterItemForm.get('custname').setValue('');
  this.masterItemForm.get('address').setValue('');
  this.masterItemForm.get('contactperson').setValue('');
  this.masterItemForm.get('totalcrebalamount').setValue(0);
  this.masterItemForm.get('vatno').setValue('');
  this.masterItemForm.get('svatno').setValue('');
  this.masterItemForm.get('autoId').setValue(0);
}

onEditCategory(event , cellId){
  this.clearControls();
  console.log("NN");
  const ids = cellId.rowID;
  console.log(ids);
  this.ogIdx = ids;
  const selectedRowData = this.masteritemGrid.data.filter((record) =>{
    return record.f01 == ids;
  });

  this.masterItemForm.get('code').setValue(selectedRowData[0]['f18']);
  this.masterItemForm.get('custname').setValue(selectedRowData[0]['f19']);
  this.masterItemForm.get('address').setValue(selectedRowData[0]['f21']);
  this.masterItemForm.get('contactperson').setValue(selectedRowData[0]['f22']);
  this.masterItemForm.get('totalcrebalamount').setValue(selectedRowData[0]['f11']);
  this.masterItemForm.get('vatno').setValue(selectedRowData[0]['f23']);
  this.masterItemForm.get('svatno').setValue(selectedRowData[0]['f24']);
  this.masterItemForm.get('autoId').setValue(selectedRowData[0]['f01']);

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
