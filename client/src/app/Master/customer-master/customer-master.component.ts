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
  selector: 'app-customer-master',
  templateUrl: './customer-master.component.html',
  styleUrls: ['./customer-master.component.css']
})
export class CustomerMasterComponent implements OnInit {

  masterCustomerForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  masterCompanyList = [];

  @ViewChild('masterCustomerGrid' , {static : true})
  public masterCustomerGrid : IgxGridComponent;

  public col : IgxColumnComponent;
  public oWidth : string;
  public nWidth : string;
  public ogIdx : BigInteger;
  masterCustomerList: any[];
  ccDisableButton: boolean = false;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private masterService: MasterService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadCustomerList();
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
    this.masterCustomerForm = this.fb.group({
      autoId: [0],
      createUserId: this.user.userId,
      code: ['', [Validators.required, Validators.maxLength(50)]],
      custname: ['', [Validators.required, Validators.maxLength(50)]],
      address: ['', [Validators.required, Validators.maxLength(50)]],
      totalcrebalamount: ['', [Validators.required, Validators.maxLength(50)]],
      contactperson: ['', [Validators.required, Validators.maxLength(50)]],
      vatno: ['', [Validators.required, Validators.maxLength(50)]],
      svatno: ['', [Validators.required, Validators.maxLength(50)]]
    });
  }

  public onResize(event){
    this.col = event.column;
    this.oWidth = event.prevWidth;
    this.nWidth = event.newWidth
  }


  loadCustomerList(){
    this.masterCustomerList = [];
    var objOG = {
      ActivityNo: 51
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterCustomerList = OpGroupList;
      console.log(OpGroupList);
    });

  }

  Customersave() {

    var SMList = [];

    var supplierData = {
      Code: this.masterCustomerForm.get('code').value,
      CustName: this.masterCustomerForm.get('custname').value.trim(),
      Address: this.masterCustomerForm.get('address').value,
      ContactPerson: this.masterCustomerForm.get('contactperson').value,
      TotalcrebalAmount: this.masterCustomerForm.get('totalcrebalamount').value,
      VatNo: this.masterCustomerForm.get('vatno').value,
      SvatNo: this.masterCustomerForm.get('svatno').value,
      AutoId: this.masterCustomerForm.get('autoId').value
    };

    var objSM = {
      sCustomer: supplierData,
      ActivityNo: 50,
      AgentNo:this.user.userId,
      ModuleNo:this.user.moduleId,
    };

    SMList.push(objSM);
    console.log(SMList);

      this.masterService.SaveMWSMasterData(SMList).subscribe((result) => {
      // console.log(result);
      if (result['result'] == 1) {
        this.toaster.success('save Successfully !!!');
        this.loadCustomerList();
        this.clearControls();
      }
      else if (result['result'] == 2) {
        this.toaster.warning('update Successfully !!!');
        this.loadCustomerList();
        this.clearControls();
      }
      else if (result['result'] == 3) {
        this.toaster.error('Code already Exists!!!');
        this.loadCustomerList();
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
  this.masterCustomerForm.get('code').setValue('');
  this.masterCustomerForm.get('custname').setValue('');
  this.masterCustomerForm.get('address').setValue('');
  this.masterCustomerForm.get('contactperson').setValue('');
  this.masterCustomerForm.get('totalcrebalamount').setValue(0);
  this.masterCustomerForm.get('vatno').setValue('');
  this.masterCustomerForm.get('svatno').setValue('');
  this.masterCustomerForm.get('autoId').setValue(0);
}

onEditCategory(event , cellId){
  this.clearControls();
  console.log("NN");
  const ids = cellId.rowID;
  console.log(ids);
  this.ogIdx = ids;
  const selectedRowData = this.masterCustomerGrid.data.filter((record) =>{
    return record.f01 == ids;
  });

  this.masterCustomerForm.get('code').setValue(selectedRowData[0]['f18']);
  this.masterCustomerForm.get('custname').setValue(selectedRowData[0]['f19']);
  this.masterCustomerForm.get('address').setValue(selectedRowData[0]['f21']);
  this.masterCustomerForm.get('contactperson').setValue(selectedRowData[0]['f22']);
  this.masterCustomerForm.get('totalcrebalamount').setValue(selectedRowData[0]['f11']);
  this.masterCustomerForm.get('vatno').setValue(selectedRowData[0]['f23']);
  this.masterCustomerForm.get('svatno').setValue(selectedRowData[0]['f24']);
  this.masterCustomerForm.get('autoId').setValue(selectedRowData[0]['f01']);

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
      sSupplier: CatData,
    ActivityNo: 53,
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
      sSupplier: CatData,
      ActivityNo: 53,
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
          this.loadCustomerList();
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
