import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  IgxComboComponent,
  IgxGridComponent,
  IgxToastComponent,
} from 'igniteui-angular';

@Component({
  selector: 'app-gate-pass',
  templateUrl: './gate-pass.component.html',
  styleUrls: ['./gate-pass.component.css']
})
export class GatePassComponent implements OnInit {

  invoiceMainForm: FormGroup;
  DocNoSearchForm: FormGroup;
  invoiceSearchForm: FormGroup;
  ItemSearchForm: FormGroup;
  customerList = [];
  invoiceList = [];
  DocNoList = [];
  invoicePOList = [];
  docNoFilters = [];
  docNoColumns = [];
  poFilters = [];
  poColumns = [];
  InvoiceNoList = [];
  itemSearchList = [];

  @ViewChild('invoiceGrid', { static: true }) invoiceGrid: IgxGridComponent;
  @ViewChild('DocNoGrid', { static: true }) DocNoGrid: IgxGridComponent;
  @ViewChild('PoList', { static: true }) PoList: IgxGridComponent;

  constructor(private fb: FormBuilder) {
    this.invoiceMainForm = this.fb.group({
      IHID: [0],
      CMId: [0],
      docno: [''],
      customer: [''],
    });

    this.DocNoSearchForm = this.fb.group({
      docnosearch: [''],
      customer: [''],
      style: [''],
      po: [''],
    });

    this.invoiceSearchForm = this.fb.group({
      docnosearch: [''],
      po: [''],
      articlename: [''],
    });

    this.ItemSearchForm = this.fb.group({
      style: [''],
      po: [''],
      size: [''],
      print: [''],
    });
  }

  ngOnInit(): void {
    // Initialize the data here
  }

  invoiceSave() {
    // Save logic here
  }

  invoiceDelete() {
    // Delete logic here
  }

  refreshCompany() {
    // Refresh company logic here
  }

  filterDocNoList(value: string) {
    // Filtering logic here
  }

  filterInvoiceList(value: string) {
    // Filtering logic here
  }

  singleSelection(event) {
    // Single selection logic here
  }

  onDeletePO(event, cellID) {
    // Delete PO logic here
  }

  onViewDOCNoCheck(event, cellID) {
    // View DOC No Check logic here
  }

  onViewInvoice(event, cellID) {
    // View Invoice logic here
  }

  DocNofilterByDocNo(term) {}

  DocNofilterByCustomer(term) {}

  DocNofilterByStyle(term) {}

  refreshcompany() {}

  DocNofilterByPO(term) {}

  POfilterByDocNo(term) {}

  POName(term) {}

  ItemfilterByStyle(term) {}

  ItemfilterByPO(term) {}

  ItemfilterBySize(term) {}

  ItemfilterByPrint(term) {}

  addItem() {}

}
