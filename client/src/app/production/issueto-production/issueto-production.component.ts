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
  selector: 'app-issueto-production',
  templateUrl: './issueto-production.component.html',
  styleUrls: ['./issueto-production.component.css'],
})
export class IssuetoProductionComponent implements OnInit {
  user: User;
  saveButton: boolean;
  issueToProMainForm: FormGroup;
  DocNoSearchForm: FormGroup;
  POsearchForm: FormGroup;
  ItemSearchForm: FormGroup;
  POList: any = [];
  styleList: any = [];
  issueToProductionList: any = [];
  itemSearchList: any = [];
  DocNoList: any = [];
  PONoList: any = [];
  selectedRowData: any = [];

  @ViewChild('DocNoGrid', { static: true })
  public DocNoGrid: IgxGridComponent;
  @ViewChild('PONoGrid', { static: true })
  public PONoGrid: IgxGridComponent;
  @ViewChild('itemSearchGrid', { static: true })
  public itemSearchGrid: IgxGridComponent;
  @ViewChild('issueToProductionGrid', { static: true })
  public issueToProductionGrid: IgxGridComponent;
  @ViewChild('style', { static: true })
  public style: IgxComboComponent;
  styleId: any;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private masterService: MasterService,
    private salesOrderService: SalesorderService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadItemList();
  }
  initializeForm() {
    this.accountService.currentUser$.forEach((element) => {
      this.user = element;
    });

    this.issueToProMainForm = this.fb.group({
      createUserId: this.user.userId,
      MIHID: [''],
      StyleID: [''],
      docno: [''],
      style: [''],
      po: [''],
      customer: [''],
      remarks: [''],
      OCHID: [''],
    });

    this.DocNoSearchForm = this.fb.group({
      docnosearch: [''],
      style: [''],
      po: [''],
      customer: [''],
    });

    this.POsearchForm = this.fb.group({
      docnosearch: [''],
      suppliername: [''],
      articlename: [''],
    });

    this.ItemSearchForm = this.fb.group({
      category: [''],
      subcategory: [''],
      itemname: [''],
    });
  }

  singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

  styleSlection(event) {
    for (const item of event.added) {
      this.styleId = item;
    }
  }

  loadItemList() {
    this.styleList = [];
    var objOG = {
      ActivityNo: 62,
    };

    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.styleList = OpGroupList;
    });
  }

  refreshcompany() {
    this.issueToProMainForm.reset();
    this.DocNoSearchForm.reset();
    this.POsearchForm.reset();
    this.ItemSearchForm.reset();
    this.POList = [];
    this.styleList = [];
    this.issueToProductionList = [];
    this.itemSearchList = [];
    this.DocNoList = [];
    this.PONoList = [];
    this.selectedRowData = [];
  }

  DocNofilterByDocNo(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 10,
      f16: this.DocNoSearchForm.get('docnosearch').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.DocNoList = OpGroupList;
      });
  }

  DocNofilterBystyle(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 12,
      f20: this.DocNoSearchForm.get('style').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.DocNoList = OpGroupList;
      });
  }

  DocNofilterByPO(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 13,
      f21: this.DocNoSearchForm.get('docnosearch').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.DocNoList = OpGroupList;
      });
  }

  DocNofilterByCustomer(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 11,
      f19: this.DocNoSearchForm.get('customer').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.DocNoList = OpGroupList;
      });
  }

  onViewDOCNoCheck(event: any, cellID: any) {
    const selectedRowID = cellID.rowID;

    const selectedRow = this.DocNoGrid.data.find(
      (item) => item.f01 === selectedRowID
    );

    console.log('onViewDOCNoCheck selectedRow', selectedRow);

    this.issueToProMainForm.get('docno').setValue(selectedRow['f16']);
    this.issueToProMainForm.get('po').setValue(selectedRow['f21']);
    this.style.setSelectedItem(selectedRow['f03']);
    this.issueToProMainForm.get('customer').setValue(selectedRow['f19']);
    this.issueToProMainForm.get('remarks').setValue(selectedRow['f17']);
    this.issueToProMainForm.get('MIHID').setValue(selectedRow['f01']);

    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 14,
      f01: selectedRowID,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.issueToProductionList = OpGroupList;
      });
  }

  onViewDOCNoDetails(event: any, cellID: any) {
    const selectedRowID = cellID.rowID;
    const selectedRow = this.PONoGrid.data.find(
      (item) => item.f02 === selectedRowID
    );
    this.issueToProMainForm.get('po').setValue(selectedRow['f21']);
    this.issueToProMainForm.get('customer').setValue(selectedRow['f19']);
    this.issueToProMainForm.get('OCHID').setValue(selectedRow['f04']);
    this.issueToProMainForm.get('MIHID').setValue(selectedRow['f02']);
  }

  POfilterByDocNo(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 1,
      f20: this.POsearchForm.get('docnosearch').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.PONoList = OpGroupList;
      });
  }

  POsupplierName(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 2,
      f21: this.POsearchForm.get('suppliername').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.PONoList = OpGroupList;
      });
  }

  CustomerSupplierName(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 3,
      f19: this.POsearchForm.get('articlename').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.PONoList = OpGroupList;
      });
  }

  ItemfilterByCategory(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 4,
      f22: this.ItemSearchForm.get('category').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.itemSearchList = OpGroupList;
      });
  }

  ItemfilterBySubCategory(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 5,
      f23: this.ItemSearchForm.get('subcategory').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.itemSearchList = OpGroupList;
      });
  }

  ItemfilterByItemName(term) {
    var OGList = [];
    this.itemSearchList = [];

    var objOG = {
      ActivityNo: 6,
      f24: this.ItemSearchForm.get('itemname').value,
    };

    OGList.push(objOG);

    this.salesOrderService
      .GetIssueToProduction(objOG)
      .subscribe((OpGroupList) => {
        this.itemSearchList = OpGroupList;
      });
  }

  addItem() {
    const selectedRowData = this.itemSearchGrid.selectedRows.map((rowIndex) => {
      return this.itemSearchGrid.data.find((record) => record.f06 === rowIndex);
    });

    this.issueToProductionList = selectedRowData;
  }

  onDeletePO(event, cellId) {
    var rowId = cellId.rowID;
    this.issueToProductionGrid.deleteRow(rowId);
    var MIHID = this.issueToProMainForm.get('MIHID').value;

    var details;
    var IssueToProductionDetails = [];

    details = {
      MIHId: MIHID,
      ItemId: rowId,
    };
    IssueToProductionDetails.push(details);

    var objSM = {
      sIssueToProductionDetails: IssueToProductionDetails,
      ActivityNo: 9,
    };

    this.salesOrderService.SaveIssueToProduction(objSM).subscribe((result) => {
      if (result['result'] == 1) {
        this.toaster.success('Deleted Successfully !!!');
      } else {
        this.toaster.warning(
          'Contact Admin. Error No:- ' + result['result'].toString()
        );
      }
    });
  }

  issueToProductionSave() {
    var IssueToProductionHeader = [];

    var header = {
      SOHId: this.issueToProMainForm.get('MIHID').value,
      StyleId: this.styleId,
      Remarks: this.issueToProMainForm.get('remarks').value,
    };

    IssueToProductionHeader.push(header);

    var IssueToProductionDetails = [];
    var detailsRow = this.issueToProductionGrid.data;
    var details;

    detailsRow.forEach((item) => {
      details = {
        ItemId: item.f06,
        UOM: item.f24,
        SIHQty: item.f14,
        IssueQty: item.f15,
      };
      IssueToProductionDetails.push(details);
    });

    var objSM = {
      sIssueToProductionHeader: IssueToProductionHeader[0],
      sIssueToProductionDetails: IssueToProductionDetails,
      ActivityNo: 7,
      ModuleNo: this.user.moduleId,
    };

    this.salesOrderService.SaveIssueToProduction(objSM).subscribe((result) => {
      if (result['result'] == 1) {
        this.issueToProMainForm.get('MIHID').setValue(result['refNumId']);
        this.issueToProMainForm.get('docno').setValue(result['refNum']);
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

  issueToProductionDelete(){
    var MIHID = this.issueToProMainForm.get('MIHID').value;

    var IssueToProductionHeader = [];

    var header = {
      MIHId: MIHID,
    };

    IssueToProductionHeader.push(header);

    var details;
    var IssueToProductionDetails = [];

    details = {
      MIHId: MIHID,
    };
    IssueToProductionDetails.push(details);

    var objSM = {
      sIssueToProductionHeader: IssueToProductionHeader[0],
      sIssueToProductionDetails: IssueToProductionDetails,
      ActivityNo: 8,
    };

    this.salesOrderService.SaveIssueToProduction(objSM).subscribe((result) => {
      if (result['result'] == 1) {
        this.issueToProMainForm.reset();
        this.toaster.success('Deleted Successfully !!!');
      } else {
        this.toaster.warning(
          'Contact Admin. Error No:- ' + result['result'].toString()
        );
      }
    });
  }
}
