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
  selector: 'app-print-master',
  templateUrl: './print-master.component.html',
  styleUrls: ['./print-master.component.css']
})
export class PrintMasterComponent implements OnInit {

  masterprintForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  masterCompanyList = [];
  ccDisableButton: boolean = false;

  
  @ViewChild('masterprintGrid' , {static : true})
  public masterprintGrid : IgxGridComponent;

  public col : IgxColumnComponent;
  public oWidth : string;
  public nWidth : string;
  public ogIdx : BigInteger;
  masterPrintList: any[];

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private masterService: MasterService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadPrintList();
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
    this.masterprintForm = this.fb.group({
      autoId: [0],
      createUserId: this.user.userId,
      code: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  public onResize(event){
    this.col = event.column;
    this.oWidth = event.prevWidth;
    this.nWidth = event.newWidth
  }


  loadPrintList(){
    this.masterPrintList = [];
    var objOG = {
      ActivityNo: 31
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterPrintList = OpGroupList;
      console.log(OpGroupList);
    });

  }

  Printsave() {

    if (this.validatecategory()) {
      var SMList = [];

      var categoryData = {
        Code: this.masterprintForm.get('code').value,
        Description: this.masterprintForm.get('description').value.trim(),
        AutoId: this.masterprintForm.get('autoId').value
      };

      var objSM = {
        sCategory: categoryData,
        ActivityNo: 30,
        AgentNo:this.user.userId,
        ModuleNo:this.user.moduleId,
      };

      SMList.push(objSM);
      console.log(SMList);

        this.masterService.SaveMWSMasterData(SMList).subscribe((result) => {
        // console.log(result);
        if (result['result'] == 1) {
          this.toaster.success('save Successfully !!!');
          this.loadPrintList();
          this.clearControls();
        }
        else if (result['result'] == 2) {
          this.toaster.warning('update Successfully !!!');
          this.loadPrintList();
          this.clearControls();
        }
        else if (result['result'] == 3) {
          this.toaster.error('Code already Exists!!!');
          this.loadPrintList();
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
    if (this.masterprintForm.get('code').value != '') {

      if (this.masterprintForm.get('description').value != '') {
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
    this.masterprintForm.get('code').setValue('');
    this.masterprintForm.get('description').setValue('');
    this.masterprintForm.get('autoId').setValue(0);
  }

  onEditCategory(event , cellId){
    this.clearControls();
    console.log("NN");
    const ids = cellId.rowID;
    console.log(ids);
    this.ogIdx = ids;
    const selectedRowData = this.masterprintGrid.data.filter((record) =>{
      return record.f01 == ids;
    });

    this.masterprintForm.get('code').setValue(selectedRowData[0]['f18']);
    this.masterprintForm.get('description').setValue(selectedRowData[0]['f19']);
    this.masterprintForm.get('autoId').setValue(selectedRowData[0]['f01']);

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
      ActivityNo: 33,
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
        ActivityNo: 33,
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
            this.loadPrintList();
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
