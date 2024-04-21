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
  articleprintsizeList: import("d:/EPS/client/src/app/_models/POAssociation").POAssociation[];

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
      ochidx: [0],
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

  loadArticlePrintSizeList() {
    this.articleprintsizeList = [];
    var objOG = {
      ActivityNo: 7,
      f01:this.orderCreationForm.get('article').value[0],
    };
    console.log(objOG);
    this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
      this.articleprintsizeList = OpGroupList;
      console.log('grnTypeList', OpGroupList);
    });
  }

  singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

  OCsave() {
    var OCData = {
      CustomerId: this.orderCreationForm.get('customer').value[0],
      ArticleId: this.orderCreationForm.get('article').value[0],
      Remaks: this.orderCreationForm.get('remarks').value
    };

    var objSM = {
      sOCHeader: OCData,
      ActivityNo: 3,
      AgentNo: this.user.userId,
      ModuleNo: this.user.moduleId,
    };
    console.log(objSM);

    this.salesOrderService.SavePOAssociationData(objSM).subscribe((result) => {
      console.log(result);
      if (result['result'] == 1) {
        this.orderCreationForm.get('ochidx').setValue(result['refNumId']);
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
    this.loadArticlePrintSizeList();

  }

  closeaddpo(){
    this.LoadAddPoDialog.close();
  }

}
