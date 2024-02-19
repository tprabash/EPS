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
  selector: 'app-supplier-master',
  templateUrl: './supplier-master.component.html',
  styleUrls: ['./supplier-master.component.css']
})
export class SupplierMasterComponent implements OnInit {

  masterSupplierForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  masterCompanyList = [];
  ccDisableButton: boolean = false;

  
  @ViewChild('masterSupplierGrid' , {static : true})
  public masterSupplierGrid : IgxGridComponent;

  public col : IgxColumnComponent;
  public oWidth : string;
  public nWidth : string;
  public ogIdx : BigInteger;
  masterSupplierList: any[];

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private masterService: MasterService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadSupplierList();
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

    this.masterSupplierForm = this.fb.group({
      autoId: [0],
      createUserId: this.user.userId,
      code: ['', [Validators.required, Validators.maxLength(50)]],
      supname: ['', [Validators.required, Validators.maxLength(50)]],
      address: ['', [Validators.required, Validators.maxLength(50)]],
      contactperson: ['', [Validators.required, Validators.maxLength(50)]],
      creditperiod: ['', [Validators.required, Validators.maxLength(50)]],
      creditlimit: ['', [Validators.required, Validators.maxLength(50)]],
      contactno: ['', [Validators.required, Validators.maxLength(50)]],
      vatno: ['', [Validators.required, Validators.maxLength(50)]],
      svatno: ['', [Validators.required, Validators.maxLength(50)]],
    });
    
   }

   public onResize(event){
    this.col = event.column;
    this.oWidth = event.prevWidth;
    this.nWidth = event.newWidth
  }

   loadSupplierList(){
    this.masterSupplierList = [];
    var objOG = {
      ActivityNo: 41
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterSupplierList = OpGroupList;
      console.log(OpGroupList);
    });

  }

  Suppliersave() {

      var SMList = [];

      var supplierData = {
        Code: this.masterSupplierForm.get('code').value,
        SupName: this.masterSupplierForm.get('supname').value.trim(),
        Address: this.masterSupplierForm.get('address').value,
        ContactPerson: this.masterSupplierForm.get('contactperson').value,
        Creaditeperiod: this.masterSupplierForm.get('creditperiod').value,
        CreaditeLimite: this.masterSupplierForm.get('creditlimit').value,
        ContactNo: this.masterSupplierForm.get('contactno').value,
        VatNo: this.masterSupplierForm.get('vatno').value,
        SvatNo: this.masterSupplierForm.get('svatno').value,
        AutoId: this.masterSupplierForm.get('autoId').value
      };

      var objSM = {
        sSupplier: supplierData,
        ActivityNo: 40,
        AgentNo:this.user.userId,
        ModuleNo:this.user.moduleId,
      };

      SMList.push(objSM);
      console.log(SMList);

        this.masterService.SaveMWSMasterData(SMList).subscribe((result) => {
        // console.log(result);
        if (result['result'] == 1) {
          this.toaster.success('save Successfully !!!');
          this.loadSupplierList();
          this.clearControls();
        }
        else if (result['result'] == 2) {
          this.toaster.warning('update Successfully !!!');
          this.loadSupplierList();
          this.clearControls();
        }
        else if (result['result'] == 3) {
          this.toaster.error('Code already Exists!!!');
          this.loadSupplierList();
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
    this.masterSupplierForm.get('code').setValue('');
    this.masterSupplierForm.get('supname').setValue('');
    this.masterSupplierForm.get('address').setValue('');
    this.masterSupplierForm.get('contactperson').setValue('');
    this.masterSupplierForm.get('creditperiod').setValue(0);
    this.masterSupplierForm.get('creditlimit').setValue(0);
    this.masterSupplierForm.get('contactno').setValue(0);
    this.masterSupplierForm.get('vatno').setValue('');
    this.masterSupplierForm.get('svatno').setValue('');
    this.masterSupplierForm.get('autoId').setValue(0);
  }

  onEditCategory(event , cellId){
    this.clearControls();
    console.log("NN");
    const ids = cellId.rowID;
    console.log(ids);
    this.ogIdx = ids;
    const selectedRowData = this.masterSupplierGrid.data.filter((record) =>{
      return record.f01 == ids;
    });

    this.masterSupplierForm.get('code').setValue(selectedRowData[0]['f18']);
    this.masterSupplierForm.get('supname').setValue(selectedRowData[0]['f19']);
    this.masterSupplierForm.get('address').setValue(selectedRowData[0]['f21']);
    this.masterSupplierForm.get('contactperson').setValue(selectedRowData[0]['f22']);
    this.masterSupplierForm.get('creditperiod').setValue(selectedRowData[0]['f03']);
    this.masterSupplierForm.get('creditlimit').setValue(selectedRowData[0]['f11']);
    this.masterSupplierForm.get('contactno').setValue(selectedRowData[0]['f04']);
    this.masterSupplierForm.get('vatno').setValue(selectedRowData[0]['f23']);
    this.masterSupplierForm.get('svatno').setValue(selectedRowData[0]['f24']);
    this.masterSupplierForm.get('autoId').setValue(selectedRowData[0]['f01']);

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
    ActivityNo: 43,
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
      ActivityNo: 43,
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
          this.loadSupplierList();
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
