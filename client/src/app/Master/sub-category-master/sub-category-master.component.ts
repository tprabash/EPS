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
  selector: 'app-sub-category-master',
  templateUrl: './sub-category-master.component.html',
  styleUrls: ['./sub-category-master.component.css']
})
export class SubCategoryMasterComponent implements OnInit {

  masterSubCatForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  masterCompanyList = [];
  ccDisableButton: boolean = false;

  
  @ViewChild('masterSubCategoryGrid' , {static : true})
  public masterSubCategoryGrid : IgxGridComponent;

  public col : IgxColumnComponent;
  public oWidth : string;
  public nWidth : string;
  public ogIdx : BigInteger;
  masterSubCategoryList: import("d:/EPS/client/src/app/_models/mwsmaster").MWSMaster[];

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private masterService: MasterService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadSubCategoryList();
  }
  initializeForm() {
    this.accountService.currentUser$.forEach((element) => {
      this.user = element;
    });
    var authMenus = this.user.permitMenus;

    if (authMenus != null) {
      if (authMenus.filter((x) => x.autoIdx == 130).length > 0) {
        this.saveButton = true;
      }if (authMenus.filter((x) => x.autoIdx == 130).length > 0) {
        this.ccDisableButton = true;
      }
      //console.log(this.saveButton);
    }
    this.masterSubCatForm = this.fb.group({
      autoId: [0],
      createUserId: this.user.userId,
      code: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  loadSubCategoryList(){
    this.masterSubCategoryList = [];
    var objOG = {
      ActivityNo: 11
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterSubCategoryList = OpGroupList;
      console.log(OpGroupList);
    });

  }

  SubCatSave() {

    if (this.validatecategory()) {
      var SMList = [];

      var categoryData = {
        Code: this.masterSubCatForm.get('code').value,
        Description: this.masterSubCatForm.get('description').value.trim(),
        AutoId: this.masterSubCatForm.get('autoId').value
      };

      var objSM = {
        sCategory: categoryData,
        ActivityNo: 10,
        AgentNo:this.user.userId,
        ModuleNo:this.user.moduleId,
      };

      SMList.push(objSM);
      console.log(SMList);

        this.masterService.SaveMWSMasterData(SMList).subscribe((result) => {
        // console.log(result);
        if (result['result'] == 1) {
          this.toaster.success('save Successfully !!!');
          this.loadSubCategoryList();
          this.clearControls();
        }
        else if (result['result'] == 2) {
          this.toaster.warning('update Successfully !!!');
          this.loadSubCategoryList();
          this.clearControls();
        }
        else if (result['result'] == 3) {
          this.toaster.error('Code already Exists!!!');
          this.loadSubCategoryList();
          this.clearControls();
        }
        else {
          this.toaster.warning(
            'Contact Admin. Error No:- ' + result['result'].toString()
          );
        }
      });
    }
  }

  validatecategory() {
    if (this.masterSubCatForm.get('code').value != '') {

      if (this.masterSubCatForm.get('description').value != '') {
        return true;
      } else {
        this.toaster.info('Plz Enter Description!!!');
        return false;
      }
    } else {
      this.toaster.info('Plz Enter Code !!!');
      return false;
    }
  }

  clearControls(){
    this.masterSubCatForm.get('code').setValue('');
    this.masterSubCatForm.get('description').setValue('');
    this.masterSubCatForm.get('autoId').setValue(0);
  }

  onEditCategory(event , cellId){
    this.clearControls();
    console.log("NN");
    const ids = cellId.rowID;
    console.log(ids);
    this.ogIdx = ids;
    const selectedRowData = this.masterSubCategoryGrid.data.filter((record) =>{
      return record.f01 == ids;
    });

    this.masterSubCatForm.get('code').setValue(selectedRowData[0]['f18']);
    this.masterSubCatForm.get('description').setValue(selectedRowData[0]['f19']);
    this.masterSubCatForm.get('autoId').setValue(selectedRowData[0]['f01']);

}

  public onResize(event){
    this.col = event.column;
    this.oWidth = event.prevWidth;
    this.nWidth = event.newWidth
  }

  //deactive click even
  Deactive(cellValue, cellId) {

    var RCTList = [];
    const id = cellId.rowID;

    var CatData = {
    AutoId: id,
    bActive: false
    };

    var objOG = {
    sCategory: CatData,
    ActivityNo: 13,
    AgentNo:this.user.userId
    };
    RCTList.push(objOG);

    this.deactiveRecepieType(RCTList, 'Deactive');
    }

    //active click event
    Active(cellValue, cellId) {
    if(this.saveButton == true) {
    var RCTList = [];
    const id = cellId.rowID;
    var CatData = {
      AutoId: id,
      bActive: true
    };
    var objMMA = {
      sCategory: CatData,
      ActivityNo: 13,
      AgentNo:this.user.userId
    };
    RCTList.push(objMMA);
    console.log(objMMA);
    this.deactiveRecepieType(RCTList, 'Active');
    } else {
    this.toaster.error('Active permission denied !!!');
    }
    }

    //active/deactive method
    deactiveRecepieType(RCTList, status) {
    if(this.ccDisableButton == true) {
      this.masterService.SaveMWSMasterData(RCTList).subscribe((result) => {
        if (result['result'] == 2) {
          this.toaster.success('Category ' + status + ' Successfully !!!');
          this.loadSubCategoryList();
        } else {
          this.toaster.warning(
            'Contact Admin. Error No:- ' + result['result'].toString()
          );
        }
      });
    } else {
      this.toaster.error('Disable permission denied !!!');
    }
    }

}
