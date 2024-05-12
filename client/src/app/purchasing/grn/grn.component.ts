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
  docnosearchForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  mainGRNList = [];
  supplierList: any = [];
  grnTypeList: any = [];
  grnSearchList: any = [];
  GRNDetailsData: any[] = [];
  GRNSearchList: any[] = [];
  itemSearchList: any[] = [];
  details: any;

  @ViewChild('grntype', { static: true })
  public grntype: IgxComboComponent;
  @ViewChild('supplier', { static: true })
  public supplier: IgxComboComponent;
  @ViewChild('grnSearchGrid', { static: true })
  public grnSearchGrid: IgxGridComponent;
  @ViewChild('mainGRNGrid', { static: true })
  public mainGRNGrid: IgxGridComponent;
  @ViewChild('itemSearchGrid', { static: true })
  public itemSearchGrid: IgxGridComponent;
  @ViewChild('GRNSearchGrid', { static: true })
  public GRNSearchGrid: IgxGridComponent;
  GRNDetails: { GRNQty: any; FreeQty: any; UnitRate: any; Value: any };
  requestData: {
    sGRNHeader: {
      GRNTypeId: any;
      SupplierIdx: any;
      SupplierRef: any;
      DocDate: string;
      Transdatetime: string;
      AutoId: any;
    };
    sGRNDetails: { GRNQty: any; FreeQty: any; UnitRate: any; Value: any };
    ActivityNo: number;
    AgentNo: number;
    ModuleNo: number;
  };

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
   
    this.masterSupplierForm = this.fb.group({
      createUserId: this.user.userId,
      docno: [''],
      docnoId: [0],
      supname: ['', [Validators.required, Validators.maxLength(50)]],
      supreff: ['', [Validators.required, Validators.maxLength(50)]],
      supplier: ['', [Validators.required, Validators.maxLength(50)]],
      supplierIdx: [''],
      GRNMasterIdx: [''],
      transDate: [new Date()],
      docDate: [new Date()],
      grntype: ['', [Validators.required, Validators.maxLength(50)]],
    });

    this.GRNSearchForm = this.fb.group({
      docnosearch: [''],
      suppliername: [''],
      articlename: [''],
    });

    this.docnosearchForm = this.fb.group({
      docnosearch: [''],
      // customersearch: [''],
      // articlesearch: [''],
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

  loadItemList() {
    this.itemSearchList = [];
    var objOG = {
      ActivityNo: 4,
    };
    console.log(objOG);
    this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
      this.itemSearchList = OpGroupList;
      console.log('grnTypeList', OpGroupList);
    });
  }

  singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

  GRNsave() {
    var GRNList = [];

    var GRNHeaderData = {
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

    var grnDetails = [];
    var detailsRow = this.mainGRNGrid.data;
    var details;
    detailsRow.forEach((item) => {
      details = {
        ItemMasterIdx: item.f01,
        GRNQty: item.f04,
        FreeQty: item.f05,
        UnitRate: item.f06,
        Value: item.f07,
      };
      grnDetails.push(details);
    });

    var objSM = {
      sGRNHeader: GRNHeaderData,
      sGRNDetails: grnDetails,
      ActivityNo: 1,
      AgentNo: this.user.userId,
      ModuleNo: this.user.moduleId,
    };

    // grnDetails.push(objSM);

    this.salesOrderService.SaveGRNData(objSM).subscribe((result) => {
      // console.log(result);
      if (result['result'] == 1) {
        this.masterSupplierForm.get('docnoId').setValue(result['refNumId']);
        this.masterSupplierForm.get('docno').setValue(result['refNum']);
        this.toaster.success('save Successfully !!!');
      } else if (result['result'] == 2) {
        this.toaster.warning('update Successfully !!!');
      } else if (result['result'] == 3) {
        this.toaster.error('Code already Exists!!!');
      } else {
        this.toaster.warning(
          'Contact Admin. Error No:- ' + result['result'].toString()
        );
      }
    });
  }

  refreshcompany() {}

  onDeleteGRN(event, cellId) {
    var rowId = cellId.rowID;
    this.mainGRNGrid.deleteRow(rowId);
  }

  onViewGRNDetails(event, cellId) {
    const ids = cellId.rowID;
    var OGList = [];
    var objOG = {
      ActivityNo: 8,
      f01: ids,
    };
    OGList.push(objOG);
    console.log('onViewGRNDetails', OGList);

    this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
      console.log('onViewGRNDetails', OpGroupList[0]);
      this.grntype.setSelectedItem(OpGroupList[0]['f03']);
      this.masterSupplierForm.get('docno').setValue(OpGroupList[0]['f16']);
      this.supplier.setSelectedItem(OpGroupList[0]['f02']);
      this.masterSupplierForm.get('supreff').setValue(OpGroupList[0]['f17']);
      this.masterSupplierForm.get('supplierIdx').setValue(OpGroupList[0]['f02']);
      this.masterSupplierForm.get('GRNMasterIdx').setValue(OpGroupList[0]['f04']);
    });
    setTimeout(() => {
      this.loadGRNDetails();
    }, 200);
  }

  loadGRNDetails() {
    var OGList = [];
    
      var objOG = {
        ActivityNo: 9,
        f01: this.masterSupplierForm.get('GRNMasterIdx').value,
      };
    
    OGList.push(objOG);
    console.log('onViewGRNDetails', OGList);

    this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
     this.mainGRNList = OpGroupList;
    });
  }

  filterByDocNo(term) {
    this.grnSearchList = [];
    var objOG = {
      ActivityNo: 5,
      f16: this.GRNSearchForm.get('docnosearch').value,
    };
    if (term != '') {
      this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
        this.GRNSearchList = OpGroupList;
      });
    }
  }

  filterBySupplier(term) {
    this.GRNSearchList = [];
    var objOG = {
      ActivityNo: 6,
      f19: this.GRNSearchForm.get('suppliername').value,
    };
    if (term != '') {
      this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
        this.GRNSearchList = OpGroupList;
      });
    }
  }

  filterByArticle(term) {
    this.GRNSearchList = [];
    var objOG = {
      ActivityNo: 7,
      f20: this.GRNSearchForm.get('articlename').value,
    };
    if (term != '') {
      this.salesOrderService.GetGRNData(objOG).subscribe((OpGroupList) => {
        this.GRNSearchList = OpGroupList;
      });
    }
  }

  addGrn() {
    const selectedRowData = this.itemSearchGrid.selectedRows.map((rowIndex) => {
      return this.itemSearchGrid.data.find((record) => record.f01 === rowIndex);
    });

    this.mainGRNList = selectedRowData;
  }

  filterByOCNo(term) {}
}
