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
import { SalesorderService } from '_services/salesorder.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-grn',
  templateUrl: './grn.component.html',
  styleUrls: ['./grn.component.css'],
})
export class GrnComponent implements OnInit {
  masterSupplierForm: FormGroup;
  GRNSearchForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  mainGRNList = [];
  supplierList: any = [];
  grnTypeList: any = [];
  grnSearchList: any = [];
  GRNDetailsData: any[] = [];

  @ViewChild('grntype', { static: true })
  public grntype: IgxComboComponent;
  @ViewChild('supplier', { static: true })
  public supplier: IgxComboComponent;
  @ViewChild('grnSearchGrid', { static: true })
  public grnSearchGrid: IgxGridComponent;
  @ViewChild('mainGRNGrid', { static: true })
  public mainGRNGrid: IgxGridComponent;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private salesOrderService: SalesorderService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadCategoryList();
    this.loadSupplierList();
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
    }
    this.masterSupplierForm = this.fb.group({
      createUserId: this.user.userId,
      docno: [''],
      docnoId: [0],
      supname: ['', [Validators.required, Validators.maxLength(50)]],
      supreff: ['', [Validators.required, Validators.maxLength(50)]],
      supplier: ['', [Validators.required, Validators.maxLength(50)]],
      transDate: ['', [Validators.required, Validators.maxLength(50)]],
      docDate: ['', [Validators.required, Validators.maxLength(50)]],
      grntype: ['', [Validators.required, Validators.maxLength(50)]],
    });

    this.GRNSearchForm = this.fb.group({
      docnosearch: [''],
      customername: [''],
      articlename: [''],
    });
  }

  loadCategoryList() {
    this.grnTypeList = [];
    var objOG = {
      ActivityNo: 2,
    };
    console.log(objOG);
    this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
      this.grnTypeList = OpGroupList;
      console.log('grnTypeList', OpGroupList);
    });
  }

  loadSupplierList() {
    this.supplierList = [];
    var objOG = {
      ActivityNo: 3,
    };
    console.log(objOG);
    this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
      this.supplierList = OpGroupList;
      console.log('grnTypeList', OpGroupList);
    });
  }

  singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

  // GRNsave() {
  //   var GRNList = [];

  //   var GRNHeaderData = {
  //     GRNTypeId: this.masterSupplierForm.get('grntype').value[0],
  //     SupplierIdx: this.masterSupplierForm.get('supplier').value[0],
  //     SupplierRef: this.masterSupplierForm.get('supreff').value,
  //     DocDate: this.datePipe.transform(
  //       this.masterSupplierForm.get('docDate').value,
  //       'yyyy-MM-dd'
  //     ),
  //     Transdatetime: this.datePipe.transform(
  //       this.masterSupplierForm.get('transDate').value,
  //       'yyyy-MM-dd'
  //     ),
  //     AutoId: this.masterSupplierForm.get('docnoId').value,
  //   };
    
  //   this.GRNDetailsData = [];
  //   this.mainGRNGrid.data.forEach((items) => {
  //       var GRNDetails= {
  //         GRNQty: items.f09,
  //         FreeQty: items.f10,
  //         UnitRate: items.f11,
  //         Value: items.f12,
  //       };

  //       this.GRNDetailsData.push(GRNDetails);
  //   });

  //   var objSM = {
  //     sGRNHeader: GRNHeaderData,
  //     sGRNDetails: this.GRNDetailsData,
  //     ActivityNo: 1,
  //     AgentNo: this.user.userId,
  //     ModuleNo: this.user.moduleId,
  //   };

  //   GRNList.push(objSM);
  //   console.log(GRNList);

  //   this.salesOrderService.SaveGRNData(GRNList).subscribe((result) => {
  //     // console.log(result);
  //     if (result['result'] == 1) {
  //       this.masterSupplierForm.get('docnoId').setValue(result['refNumId']);
  //       this.masterSupplierForm.get('docno').setValue(result['refNum']);
  //       this.toaster.success('save Successfully !!!');
  //     } else if (result['result'] == 2) {
  //       this.toaster.warning('update Successfully !!!');
  //     } else if (result['result'] == 3) {
  //       this.toaster.error('Code already Exists!!!');
  //     } else {
  //       this.toaster.warning(
  //         'Contact Admin. Error No:- ' + result['result'].toString()
  //       );
  //     }
  //   });
  // }

  // GRNsave() {
  //   const GRNHeaderData = {
  //     GRNTypeId: this.masterSupplierForm.get('grntype').value[0],
  //     SupplierIdx: this.masterSupplierForm.get('supplier').value[0],
  //     SupplierRef: this.masterSupplierForm.get('supreff').value,
  //     DocDate: this.datePipe.transform(
  //       this.masterSupplierForm.get('docDate').value,
  //       'yyyy-MM-dd'
  //     ),
  //     Transdatetime: this.datePipe.transform(
  //       this.masterSupplierForm.get('transDate').value,
  //       'yyyy-MM-dd'
  //     ),
  //     AutoId: this.masterSupplierForm.get('docnoId').value,
  //   };
  
  //   this.GRNDetailsData = [];
  //   this.mainGRNGrid.data.forEach((items) => {
  //       var GRNDetails= {
  //         GRNQty: items.f09,
  //         FreeQty: items.f10,
  //         UnitRate: items.f11,
  //         Value: items.f12,
  //       };

  //       this.GRNDetailsData.push(GRNDetails);
  //   });
  
  //   const objSM = {
  //     sGRNHeader: GRNHeaderData,
  //     sGRNDetails: this.GRNDetailsData, 
  //     ActivityNo: 1,
  //     AgentNo: this.user.userId,
  //     ModuleNo: this.user.moduleId,
  //   };
  
  
  //   this.salesOrderService.SaveGRNData(objSM).subscribe(
  //     (result) => {
  //       if (result['result'] == 1) {
  //         this.masterSupplierForm.get('docnoId').setValue(result['refNumId']);
  //         this.masterSupplierForm.get('docno').setValue(result['refNum']);
  //         this.toaster.success('Save Successful!');
  //       } else if (result['result'] == 2) {
  //         this.toaster.warning('Update Successful!');
  //       } else if (result['result'] == 3) {
  //         this.toaster.error('Code already exists!');
  //       } else {
  //         this.toaster.warning('Contact Admin. Error No:- ' + result['result'].toString());
  //       }
  //     },
  //     (error) => {
  //       console.error('Error occurred:', error);
  //       // Handle error, show appropriate message to the user
  //       // For example:
  //       this.toaster.error('An error occurred while saving GRN data.');
  //     }
  //   );
  // }

  GRNsave() {

    const GRNHeaderData =[];

    const GRNHeader = {
      GRNTypeId: this.masterSupplierForm.get('grntype').value[0],
      SupplierIdx: this.masterSupplierForm.get('supplier').value[0],
      SupplierRef: this.masterSupplierForm.get('supreff').value,
      DocDate: this.datePipe.transform(
        this.masterSupplierForm.get('docDate').value,
        'yyyy-MM-dd'
      ),
      Transdatetime: this.datePipe.transform(
        this.masterSupplierForm.get('transDate').value,
        'yyyy-MM-dd'
      ),
      AutoId: this.masterSupplierForm.get('docnoId').value,
      
    };
    GRNHeaderData.push(GRNHeader);
  
    // Initialize GRNDetailsData array
    const GRNDetailsData = [];
  
    // Iterate through mainGRNGrid data and populate GRNDetailsData array
    this.mainGRNGrid.data.forEach((items) => {
      const GRNDetails = {
        GRNQty: items.f09,
        FreeQty: items.f10,
        UnitRate: items.f11,
        Value: items.f12,
      };
  
      GRNDetailsData.push(GRNDetails);
    });
  
    // Prepare data object to be sent in the request
    const requestData = {
      sGRNHeader: GRNHeaderData,
      sGRNDetails: GRNDetailsData,
      ActivityNo: 1,
      AgentNo: this.user.userId,
      ModuleNo: this.user.moduleId,
    };
  
    // Send POST request to the server
    this.salesOrderService.SaveGRNData(requestData).subscribe(
      (result) => {
        if (result['result'] == 1) {
          this.masterSupplierForm.get('docnoId').setValue(result['refNumId']);
          this.masterSupplierForm.get('docno').setValue(result['refNum']);
          this.toaster.success('Save Successful!');
        } else if (result['result'] == 2) {
          this.toaster.warning('Update Successful!');
        } else if (result['result'] == 3) {
          this.toaster.error('Code already exists!');
        } else {
          this.toaster.warning('Contact Admin. Error No:- ' + result['result'].toString());
        }
      },
      (error) => {
        console.error('Error occurred:', error);
        // Handle error, show appropriate message to the user
        // For example:
        this.toaster.error('An error occurred while saving GRN data.');
      }
    );
  }
  

  refreshcompany() {}

  onEditCompany(event, cellId) {}

  filterByDocNo(term) {
    this.grnSearchList = [];
    var objOG = {
      ActivityNo: 5,
      f16: this.GRNSearchForm.get('docnosearch').value,
    };
    if (term != '') {
      this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
        this.grnSearchList = OpGroupList;
        console.log(this.grnSearchList);
      });
    }
  }

  filterByCustomer(term) {
    this.grnSearchList = [];
    var objOG = {
      ActivityNo: 6,
      f16: this.GRNSearchForm.get('customername').value,
    };
    if (term != '') {
      this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
        this.grnSearchList = OpGroupList;
        console.log(this.grnSearchList);
      });
    }
  }

  filterByArticle(term) {
    this.grnSearchList = [];
    var objOG = {
      ActivityNo: 7,
      f16: this.GRNSearchForm.get('articlename').value,
    };
    if (term != '') {
      this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
        this.grnSearchList = OpGroupList;
        console.log(this.grnSearchList);
      });
    }
  }

  addGrn() {
    const selectedRowData = this.grnSearchGrid.selectedRows.map(rowIndex => {
      return this.grnSearchGrid.data.find(record => record.f01 === rowIndex);
    });

    this.mainGRNList = selectedRowData;
  }

}
