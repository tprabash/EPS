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
  selector: 'app-production-out',
  templateUrl: './production-out.component.html',
  styleUrls: ['./production-out.component.css']
})

export class ProductionOutComponent implements OnInit {
  prodOutForm: FormGroup;
  docnosearchForm: FormGroup;
  addpoForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  customerList: any[] ;
  articleList: any[] ;
  OrderCreationSearchList: any[] = [];
  orderCreationList: any[] = [];
  articleprintsizeList:any[];
  gOCHIdx: number = 0;
  gsohIdx: number = 0;

  @ViewChild('grntype', { static: true })
  public grntype: IgxComboComponent;
  @ViewChild('supplier', { static: true })
  public supplier: IgxComboComponent;
  @ViewChild('prodOutGrid', { static: true })
  public prodOutGrid: IgxGridComponent;
  @ViewChild('orderCreationSearchGrid', { static: true })
  public orderCreationSearchGrid: IgxGridComponent;
  @ViewChild('LoadAddPoDialog', { read: IgxDialogComponent })
  public LoadAddPoDialog: IgxDialogComponent;
  @ViewChild('customer', { read: IgxComboComponent })
  public customer: IgxComboComponent;
  @ViewChild('article', { read: IgxComboComponent })
  public article: IgxComboComponent;
  

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
    this.prodOutForm = this.fb.group({
      createUserId: this.user.userId,
      docno: [''],
      customer: [''],
      article: [''],
      remarks: [''],
      proouthid: [0],
      styleid: [''],
    });

    this.docnosearchForm = this.fb.group({
      docnosearch: [''],
      customersearch: [''],
      articlesearch: [''],
    });

    this.addpoForm = this.fb.group({
      sohid: [0],
      addponame: [''],
      deliverydate: [new Date()]
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
      ActivityNo: 1,
    };
    console.log(objOG);
    this.salesOrderService.GetProductionOutData(objOG).subscribe((OpGroupList) => {
      this.articleList = OpGroupList;
      console.log('grnTypeList', OpGroupList);
    });
  }

  loadArticlePrintSizeList() {
    this.articleprintsizeList = [];
    var objOG = {
      ActivityNo: 8,
      f01:this.prodOutForm.get('article').value[0],
    };
    console.log(objOG);
    this.salesOrderService.GetProductionOutData(objOG).subscribe((OpGroupList) => {
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
      CustomerId: this.prodOutForm.get('customer').value[0],
      ArticleId: this.prodOutForm.get('article').value[0],
      Remaks: this.prodOutForm.get('remarks').value
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
        this.prodOutForm.get('ochidx').setValue(result['refNumId']);
        this.prodOutForm.get('docno').setValue(result['refNum']);
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

  prodoutSave(){
    var SalesordersSaveList = [];
    var objOCDetailSave = {};
    var prodoutheader = {
      AutoId:this.prodOutForm.get('proouthid').value || 0,
      ArticleId: this.prodOutForm.get('article').value[0],
      Remarks: this.prodOutForm.get('remarks').value
    };


    var objOCHeaderSave = {
      sProductionoutHeader: prodoutheader,
      ActivityNo: 5,
      AgentNo: this.user.userId,
      ModuleNo: this.user.moduleId
    };

    SalesordersSaveList.push(objOCHeaderSave);

    var selectedRows = this.orderCreationSearchGrid.data;
    console.log(selectedRows);

    
    selectedRows.forEach((items) => {
     var  ocdetailsdata = {
        POHId: items.f02,
        SODId: items.f01,
        OrderQty: items.f04,
        ProductionOutQty: items.f05,
        DamageQty: items.f06
      };

      if (ocdetailsdata.ProductionOutQty !== 0 || ocdetailsdata.ProductionOutQty == null) {
       objOCDetailSave = {
        sProductionoutDetails: ocdetailsdata,
        ActivityNo: 5,
        AgentNo: this.user.userId,
        ModuleNo: this.user.moduleId,
      };

      SalesordersSaveList.push(objOCDetailSave);
    }
    });
    console.log(objOCHeaderSave);
    console.log(SalesordersSaveList);

    this.salesOrderService.SaveProductionOutData(SalesordersSaveList).subscribe((result) => {
      console.log(result);
      if (result['result'] == 1) {
        this.prodOutForm.get('proouthid').setValue(result['refNumId']);
        this.prodOutForm.get('docno').setValue(result['refNum']);
        this.LoadAddPoDialog.close();
        this.toaster.success('save Successfully !!!');
      } else if (result['result'] == 2) {
        this.toaster.warning('update Successfully !!!');
        this.LoadSalesOrderList();
        this.addpoForm.get('addponame').setValue('');
        this.addpoForm.get('deliverydate').setValue('');
        this.addpoForm.get('sohid').setValue(0);
        this.LoadAddPoDialog.close();
      } else if (result['result'] == 3) {
        this.toaster.error('Code already Exists!!!');
        this.LoadSalesOrderList();
        this.addpoForm.get('addponame').setValue('');
        this.addpoForm.get('deliverydate').setValue('');
        this.addpoForm.get('sohid').setValue(0);
        this.LoadAddPoDialog.close();
      } else {
        this.toaster.warning(
          'Contact Admin. Error No:- ' + result['result'].toString()
        );
      }
    });


  }

  LoadSalesOrderList() {
    this.orderCreationList = [];
    var objOG = {
      ActivityNo: 9,
      f04:this.prodOutForm.get('ochidx').value,
    };
    console.log(objOG);
    this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
      this.orderCreationList = OpGroupList;
      console.log('grnTypeList', OpGroupList);
    });
  }



  onEditOrderCreation(event, cellId){
    this.LoadAddPoDialog.open();
    const ids = cellId.rowID;
    this.gOCHIdx = ids;
    var objOG = {
      ActivityNo: 10,
      f04: ids,
    };
    console.log(objOG);
    this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
      console.log(OpGroupList);

      this.addpoForm.get('sohid').setValue(OpGroupList[0]['f04']);
      this.gsohIdx = this.addpoForm.get('sohid').value;
      this.addpoForm.get('addponame').setValue(OpGroupList[0]['f18']);
      this.addpoForm.get('deliverydate').setValue(new Date(OpGroupList[0]['f23']));


      this.articleprintsizeList = [];
      var objOG = {
        ActivityNo: 11,
        f07:this.gsohIdx
      };
      console.log(objOG);
      this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
        this.articleprintsizeList = OpGroupList;
        console.log('grnTypeList', OpGroupList);
      });

    });
  }

  loadArticlePrintSizeListonRecall() {
    this.articleprintsizeList = [];
    var objOG = {
      ActivityNo: 7,
      f07:this.gsohIdx
    };
    console.log(objOG);
    this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
      this.articleprintsizeList = OpGroupList;
      console.log('grnTypeList', OpGroupList);
    });
  }

  deleteOrderCreation(event, cellId){
    var SalesordersSaveList = [];
     const ids = cellId.rowID;
     var objOG = {
      AutoId: ids,
      BuyerDelDate: this.addpoForm.get('deliverydate')?.value ? this.datePipe.transform(this.addpoForm.get('deliverydate')?.value,'yyyy-MM-dd' ).toString(): null
     };

     var objOCHeaderSave = {
      sSalesOrderHeader: objOG,
      ActivityNo: 15,
      AgentNo: this.user.userId,
      ModuleNo: this.user.moduleId
    };

     console.log(objOCHeaderSave);
     SalesordersSaveList.push(objOCHeaderSave);
     this.salesOrderService.SaveOCData(SalesordersSaveList).subscribe((result) => {
      console.log(result);
      if (result['result'] == 1) {      
        this.toaster.error('Deleted Succesfully!!!');
        this.LoadSalesOrderList();
      }else {
        this.toaster.warning(
          'Contact Admin. Error No:- ' + result['result'].toString()
        );
      }  
    });
  }

  refreshcompany() {

  }

  addpo(){

  }

  onViewOCDetails(event, cellId) {
    //Load OC Header Data
    const ids = cellId.rowID;
    this.gOCHIdx = ids;
    var objOG = {
      ActivityNo: 14,
      f04: ids,
    };
    console.log(objOG);
    this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
      console.log(OpGroupList);

      this.prodOutForm.get('remarks').setValue(OpGroupList[0]['f16']);
      this.prodOutForm.get('docno').setValue(OpGroupList[0]['f17']);
      this.prodOutForm.get('ochidx').setValue(OpGroupList[0]['f01']);

      const selectedItemcus = this.customerList.find(
        (item) => item.f01 === OpGroupList[0]['f02']
      );
      if (selectedItemcus) {
        this.customer.setSelectedItem(selectedItemcus);
      }

      const selectedcatItem = this.articleList.find(
        (item) => item.f01 === OpGroupList[0]['f03']
      );
      if (selectedcatItem) {
        this.article.setSelectedItem(selectedcatItem);
      }

    });

    this.LoadSalesOrderListOnRecall();
   
  }


  LoadSalesOrderListOnRecall() {
    this.orderCreationList = [];
    var objOG = {
      ActivityNo: 9,
      f04:this.gOCHIdx ,
    };
    console.log(objOG);
    this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
      this.orderCreationList = OpGroupList;
      console.log('grnTypeList', OpGroupList);
    });
  }


  LoadAddPo(){
    this.LoadAddPoDialog.open();
    this.loadArticlePrintSizeList();

  }

  closeaddpo(){
    this.LoadAddPoDialog.close();
  }


    //Oc Search by Cus
    public filterByCustomer(term) {
      this.OrderCreationSearchList = [];
        var objOG = {
          ActivityNo: 6,
          f19: this.docnosearchForm.get('customersearch').value,
        };
  
        console.log(this.docnosearchForm.get('customersearch').value);

          this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
            this.OrderCreationSearchList = OpGroupList;
          });
        
    }
  
    //Oc Search by OC
    public filterByOCNo(term) {
      this.OrderCreationSearchList = [];
        var objOG = {
          ActivityNo: 5,
          f16: this.docnosearchForm.get('docnosearch').value,
        };
          this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
            this.OrderCreationSearchList = OpGroupList;
          });
    }
  
    //Oc Search by Cus
    public filterByArticle(term) {
      this.OrderCreationSearchList = [];
        var objOG = {
          ActivityNo: 13,
          f21: this.docnosearchForm.get('articlesearch').value,
        };
        console.log(objOG);
          this.salesOrderService.GetPOAssociationData(objOG).subscribe((OpGroupList) => {
            this.OrderCreationSearchList = OpGroupList;
            console.log(OpGroupList);
          });
      
    }
}