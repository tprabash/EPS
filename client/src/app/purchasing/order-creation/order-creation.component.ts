import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {
  IgxColumnComponent,
  IgxGridComponent,
  IgxComboComponent,
  IComboSelectionChangeEventArgs,
  IgxDialogComponent,
} from 'igniteui-angular';
import { MasterService } from '_services/master.service';
import { AccountService } from '_services/account.service';
import { SalesorderService } from '_services/salesorder.service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-order-creation',
  templateUrl: './order-creation.component.html',
  styleUrls: ['./order-creation.component.css'],
})
export class OrderCreationComponent implements OnInit {
  orderCreationForm: FormGroup;
  docnosearchForm: FormGroup;
  addpoForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  customerList: any[] = [];
  articleList: any[] = [];
  OrderCreationSearchList: any[] = [];
  orderCreationList: any[] = [];

  @ViewChild('grntype', { static: true })
  public grntype: IgxComboComponent;
  @ViewChild('supplier', { static: true })
  public supplier: IgxComboComponent;
  @ViewChild('orderCreationGrid', { static: true })
  public orderCreationGrid: IgxGridComponent;
  @ViewChild('OrderCreationSearchGrid', { static: true })
  public OrderCreationSearchGrid: IgxGridComponent;
  @ViewChild('LoadAddPoDialog', { read: IgxDialogComponent })
  public LoadAddPoDialog: IgxDialogComponent;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private salesOrderService: SalesorderService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadCustomerList();
    this.loadArticleList();
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
    this.orderCreationForm = this.fb.group({
      createUserId: this.user.userId,
      docno: [''],
      customer: [''],
      article: [''],
      remarks: [''],
    });

    this.docnosearchForm = this.fb.group({
      docnosearch: [''],
      customersearch: [''],
      articlesearch: [''],
    });

    this.addpoForm = this.fb.group({
      sohid: [''],
      addponame: [''],
      deliverydate: [''],
    });
  }

  loadCustomerList() {
    this.customerList = [];
    var objOG = {
      ActivityNo: 1,
    };
    console.log(objOG);
    this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
      this.customerList = OpGroupList;
      console.log('customerList', OpGroupList);
    });
  }

  loadArticleList() {
    this.articleList = [];
    var objOG = {
      ActivityNo: 2,
    };
    console.log(objOG);
    this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
      this.articleList = OpGroupList;
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
      GRNTypeId: this.orderCreationForm.get('grntype').value[0],
      SupplierIdx: this.orderCreationForm.get('supplier').value[0],
      SupplierRef: this.orderCreationForm.get('supreff').value,
      DocDate: this.datePipe.transform(
        this.orderCreationForm.get('docDate').value,
        'yyyy-MM-dd'
      ),
      Transdatetime: this.datePipe.transform(
        this.orderCreationForm.get('transDate').value,
        'yyyy-MM-dd'
      ),
      AutoId: this.orderCreationForm.get('docnoId').value,
    };

    var GRNDetailsData = {};

    var objSM = {
      sGRNHeader: GRNHeaderData,
      // sGRNDetails: GRNDetailsData,
      ActivityNo: 1,
      AgentNo: this.user.userId,
      ModuleNo: this.user.moduleId,
    };

    GRNList.push(objSM);
    console.log(GRNList);

    this.salesOrderService.SaveGRNData(GRNList).subscribe((result) => {
      // console.log(result);
      if (result['result'] == 1) {
        this.orderCreationForm.get('docnoId').setValue(result['refNumId']);
        this.orderCreationForm.get('docno').setValue(result['refNum']);
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

  filterByDocNo(term){

  }

  filterByCustomer(term){

  }

  filterByArticle(term){

  }

  onEditOrderCreation(event){

  }

  deleteOrderCreation(event){

  }

  refreshcompany() {

  }

  addpo(){

  }

  saverecipestep(){
    
  }

  LoadAddPo(){
    this.LoadAddPoDialog.open();
  }

}
