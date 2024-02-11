
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IComboSelectionChangeEventArgs } from 'igniteui-angular';
import { AccountService } from '_services/account.service';
import { LocalService } from '_services/local.service';
import { RegisterService } from '_services/register.service';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';
import { UserLocation } from '../_models/userLocation';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mainsidebar',
  templateUrl: './mainsidebar.component.html',
  styleUrls: ['./mainsidebar.component.css'],
})
export class MainsidebarComponent implements OnInit {
  pwChangeForm: FormGroup;
  adminGroup: boolean = false;
  masterGroup: boolean = false;
  salesGroup: boolean = false;
  productionGroup: boolean = false;
  menuList: boolean = false;
  userReg: boolean = false;
  userPermit: boolean = false;
  codeDefi: boolean = false;
  serialNoDt: boolean = false;
  mstrSize: boolean = false;
  mstrColor: boolean = false;
  units: boolean = false;
  process: boolean = false;
  prodDef: boolean = false;
  costingGroup: boolean = false;
  fluteType: boolean = false;
  salesAgent: boolean = false;
  currency: boolean = false;
  countries: boolean = false;
  paymentTerms: boolean = false;
  rejReason: boolean = false;
  product: boolean = false;
  brand: boolean = false;
  bank: boolean = false;
  materialType: boolean = false;
  category: boolean = false;
  addressType: boolean = false;
  customer: boolean = false;
  flexField: boolean = false;
  article: boolean = false;
  storeSite: boolean = false;
  salesOrder: boolean = false;
  jobCard: boolean = false;
  fpo: boolean = false;
  fpoIn: boolean = false;
  fpoOut: boolean = false;
  qualityCon: boolean = false;
  dispatch: boolean = false;
  costing: boolean = false;
  costAttach: boolean = false;
  finance: boolean = false;
  exchRate: boolean = false;
  tax: boolean = false;
  min: boolean = false;
  invoice: boolean = false;
  approveCenter: boolean = false;
  receipt: boolean = false;
  receiptAllocation: boolean = false;
  dashboard: boolean = false;
  report: boolean = false;
  reportList: boolean = false;
  errorLog: boolean = false;
  customerType: boolean = false;
  creditNote: boolean = false;
  invoiceType: boolean = false;
  paymentMode: boolean = false;
  mrNote: boolean = false;
  purchasingGroup: boolean = false;
  specialInstruction: boolean = false;
  masterCompany: boolean = false;
  CartonBoxType: boolean = false;
  SupplierType: boolean = false;
  salesOrderStatus: boolean = false;
  SalesOrderDefault: boolean = false;
  crtntrnasfer: boolean = false;
  purchasingOrder: boolean = false;
  mrIndent: boolean = false;
  userIndent: boolean = false;
  adhocIndent: boolean = false;
  indentGroup: boolean = false;
  ports: boolean = false;
  supplier: boolean = false;
  creditNoteAllocation: boolean = false;
  shipmentMode: boolean = false;
  accountType: boolean = false;
  inventoryGroup: boolean = false;
  grn: boolean = false;
  isCollapsed = false;
  userLoc: UserLocation[];
  user: User;
  public selectedNoValueKey: number[];
  cartonMenuStatus: boolean = false;
  ptrackMenuStatus: boolean = false;
  mtrackMenuStatus: boolean = false;
  mwsMenuStatus: boolean = false;
  workstudyGroup: boolean = false;
  operationgroup: boolean = false;
  sectionmaster: boolean = false;
  CartonType: boolean = false;
  debitNote: boolean = false;
  machinetype: boolean = false;
  GRNType: boolean = false;
  operation: boolean = false;
  factorywisesection: boolean = false;
  operationassigntofactory: boolean = false;
  operationassigntofactorysection: boolean = false;
  operationbreakdown: boolean = false;
  validationErrors: string[] = [];
  dashboard1: boolean = false;
  factorylayout: boolean = false;
  SystemTitle: string; y
  CPDProduction: boolean = false;
  transportGroup: boolean = false;
  forwarder: boolean = false;
  POType: boolean = false;
  DispatchR: boolean = false;
  Basis: boolean = false;
  AdditionalC: boolean = false;
  DeliveryT: boolean = false;
  FROrdAssociation: boolean = false;
  stylesmv: boolean = false;
  paymentinvoice: boolean = false;
  fixedAssetGroup: boolean = false;
  sampleGroup: boolean = true;
  sampleProdIn: boolean = true;
  sampleProdOut: boolean = true;

  vehicletype: boolean = false;
  vehiclecategory: boolean = false;
  bookingtype: boolean = false;
  transporttype: boolean = false;
  paymenttype: boolean = false;
  route: boolean = false;
  routefactoryallocation: boolean = false;
  vehicleother: boolean = false;
  transpoter: boolean = false;
  transpoterfactoryallocation: boolean = false;
  ratematrix: boolean = false;
  approvalmatrix: boolean = false;
  bookingrequest: boolean = false;
  vehicleregister: boolean = false;
  payableInvoice: boolean = false;
  integration: boolean = false;
  packagemapping: boolean = false;
  stockAdj: boolean = false;
  faMasterTab: boolean = false;
  model: boolean = false;
  sales: boolean = false;
  buyerPurchaseOrder: boolean = false;
  fixedAssetDetails: boolean = true;
  dashboard2: boolean = false;
  blockbooking: boolean = false;
  operationAssign: boolean = false;
  ordercreation: boolean = false;
  lostTime: boolean = false;
  departmentwisesectionmaster: boolean = false;
  merchantdizer: boolean = false;
  buyingofficemaster: boolean = false;
  dyetype: boolean = false;
  fabcomp: boolean = false;
  recipe: boolean = false;
  gender: boolean = false;
  gmttype: boolean = false;
  machinemaster: boolean = false;
  machinetoprocess: boolean = false;
  machtypes: boolean = false;
  processtype: boolean = false;
  receipetype: boolean = false;
  recipeutl: boolean = false;
  sampleissue: boolean = false;
  sampletype: boolean = false;
  speoprtype: boolean = false;
  subsalescategory: boolean = false;
  useby: boolean = false;
  washstd: boolean = false;
  costsheet: boolean = false;
  machineBreak: boolean = false;
  folderandattachment: boolean = false;
  mbrand: boolean = false;
  foot: boolean = false;
  type: boolean = false;
  size: boolean = false;
  productionissue:boolean = false;
  washtype: boolean = false;
  processtochemical : boolean = false;
  @ViewChild('navMenu', { static: false }) navMenu: ElementRef<HTMLElement>;

  constructor(
    public accountServices: AccountService,
    private router: Router,
    private localService: LocalService,
    private fb: FormBuilder,
    private registerService: RegisterService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.accountServices.currentUser$.forEach((element) => {
      this.user = element;
    });

    if (this.user.moduleId == 2) {
      this.ptrackMenuStatus = true;
      this.SystemTitle = 'EPS'
    } else if (this.user.moduleId == 3) {
      this.mwsMenuStatus = true;
      this.SystemTitle = 'EPS'
    } else if (this.user.moduleId == 4) {
      this.mtrackMenuStatus = true;
      this.SystemTitle = 'SMF - EE'
    } else {
      this.cartonMenuStatus = true;
      this.SystemTitle = 'EPS'
    }
    //console.log(this.cartonMenuStatus);
    this.loadUserLocation();
    this.checkMenuPermission();
    this.initilizeForm();
  }

  initilizeForm() {
    this.pwChangeForm = this.fb.group({
      cAgentName: [this.user.userName, Validators.required],
      OldPassword: [],

      cPassword: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(10),
        ],
      ],
      confirmPassword: [
        '',
        [Validators.required, this.matchValues('cPassword')],
      ],
    });
  }

  menuClose() {
    this.navMenu.nativeElement.click();
  }

  /// CHECK THE PASSWORD MATCH BY THE CONFIRM PASSWORD
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
        ? null
        : { isMatching: true };
    };
  }

  public singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

  loadUserLocation() {
    console.log(this.user);
    this.userLoc = this.user.locations;
    var selectRow = this.userLoc.filter((x) => x.isDefault == true);
    console.log(selectRow);
    selectRow.forEach((element) => {
      this.user['locationId'] = element.locationId;
      // this.user.locations = [];
      // localStorage.setItem('user', JSON.stringify(this.user));
      this.selectedNoValueKey = [element.locationId];
    });

  }

  selectLocation(event) {
    // var user: User = JSON.parse(localStorage.getItem('user'));
    var user: User = this.user;
    for (const item of event.added) {
      /// loads locations
      user['locationId'] = item;
      this.localService.storagesetJsonValue('user', user);
      // localStorage.setItem('user', JSON.stringify(user));
      //console.log(user);
    }
    this.router.navigate(['/']);
  }

  logout() {
    this.accountServices.logout();
    this.accountServices.decodedToken = null;
    this.router.navigateByUrl('/');
  }

  checkMenuPermission() {
    var menus = this.user.permitMenus;
    console.log(menus);
    var formMenus = menus.filter((x) => x.mType == 'F');

    if (formMenus.filter((x) => x.groupName == 'Dashboard').length > 0) {
      this.dashboard = true;

      if (formMenus.filter((x) => x.autoIdx == 1184).length > 0)
        this.approveCenter = true;
      if (formMenus.filter((x) => x.autoIdx == 2196).length > 0)
        this.errorLog = true;
      if (formMenus.filter((x) => x.autoIdx == 2228 || x.autoIdx == 2313).length > 0)
        this.dashboard1 = true;
      if (formMenus.filter((x) => x.autoIdx == 2228 || x.autoIdx == 2361).length > 0)
        this.dashboard2 = true;

    }/// USER PERMITET ADMIN GROUP
    if (formMenus.filter((x) => x.groupName == 'Admin').length > 0) {
      this.adminGroup = true;

      //// SUB MENU OF ADMIN GROUP
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.userPermit = true;
      if (formMenus.filter((x) => x.autoIdx == 2183).length > 0)
        this.reportList = true;
      if (formMenus.filter((x) => x.autoIdx == 29).length > 0)
        this.userReg = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.menuList = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.codeDefi = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.serialNoDt = true;
    }
    if (formMenus.filter((x) => x.groupName == 'Master').length > 0) {
      this.masterGroup = true;

      //// SUB MENU OF MASTER GROUP
      if (formMenus.filter(x => x.autoIdx == 3 || x.autoIdx == 43 || x.autoIdx == 65).length > 0)
        this.mstrSize = true;
      if (formMenus.filter((x) => x.autoIdx == 2 || x.autoIdx == 42 || x.autoIdx == 66).length > 0)
        this.mstrColor = true;
      if (formMenus.filter((x) => x.autoIdx == 36 || x.autoIdx == 47).length > 0)
        this.units = true;
      if (formMenus.filter((x) => x.autoIdx == 37 || x.autoIdx == 175).length > 0)
        this.storeSite = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.process = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.prodDef = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.costingGroup = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.fluteType = true;
      if (formMenus.filter((x) => x.autoIdx == 52).length > 0)
        this.salesAgent = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.currency = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.countries = true;
      if (formMenus.filter((x) => x.autoIdx == 35).length > 0)
        this.paymentTerms = true;
      if (formMenus.filter((x) => x.autoIdx == 56).length > 0)
        this.rejReason = true;
      if (formMenus.filter((x) => x.autoIdx == 57 || x.autoIdx == 67 || x.autoIdx == 68 || x.autoIdx == 69).length > 0)
        this.product = true;
      if (formMenus.filter((x) => x.autoIdx == 58 || x.autoIdx == 70).length > 0)
        this.brand = true;
      if (formMenus.filter((x) => x.autoIdx == 59).length > 0)
        this.materialType = true;
      if (formMenus.filter((x) => x.autoIdx == 60).length > 0)
        this.category = true;
      if (formMenus.filter((x) => x.autoIdx == 61).length > 0)
        this.addressType = true;
      if (formMenus.filter((x) => x.autoIdx == 62 || x.autoIdx == 71 || x.autoIdx == 72 || x.autoIdx == 73
        || x.autoIdx == 74 || x.autoIdx == 75 || x.autoIdx == 76).length > 0)
        this.customer = true;
      if (formMenus.filter((x) => x.autoIdx == 63 || x.autoIdx == 77).length > 0)
        this.flexField = true;
      if (formMenus.filter((x) => x.autoIdx == 64 || x.autoIdx == 78 || x.autoIdx == 79).length > 0)
        this.article = true;
      if (formMenus.filter((x) => x.autoIdx == 80).length > 0)
        this.customerType = true;
      if (formMenus.filter((x) => x.autoIdx == 2189).length > 0)
        this.invoiceType = true;
      if (formMenus.filter((x) => x.autoIdx == 2190).length > 0)
        this.paymentMode = true;
      if (formMenus.filter((x) => x.autoIdx == 2200).length > 0)
        this.masterCompany = true;
      if (formMenus.filter((x) => x.autoIdx == 2202).length > 0)
        this.CartonBoxType = true;
      if (formMenus.filter((x) => x.autoIdx == 2214).length > 0)
        this.specialInstruction = true;
      if (formMenus.filter((x) => x.autoIdx == 2214).length > 0)
        this.ports = true;
      if (formMenus.filter((x) => x.autoIdx == 2237).length > 0)
        this.supplier = true;
      if (formMenus.filter((x) => x.autoIdx == 2243).length > 0)
        this.SupplierType = true;
      if (formMenus.filter((x) => x.autoIdx == 2245).length > 0)
        this.accountType = true;
      if (formMenus.filter((x) => x.autoIdx == 2247).length > 0)
        this.GRNType = true;
      if (formMenus.filter((x) => x.autoIdx == 2249).length > 0)
        this.shipmentMode = true;
      if (formMenus.filter((x) => x.autoIdx == 2288).length > 0)
        this.sectionmaster = true;
      if (formMenus.filter((x) => x.autoIdx == 2249).length > 0)
        this.forwarder = true;
      if (formMenus.filter((x) => x.autoIdx == 2252).length > 0)
        this.POType = true;
      if (formMenus.filter((x) => x.autoIdx == 2254).length > 0)
        this.DispatchR = true;
      if (formMenus.filter((x) => x.autoIdx == 2256).length > 0)
        this.Basis = true;
      if (formMenus.filter((x) => x.autoIdx == 2258).length > 0)
        this.AdditionalC = true;
      if (formMenus.filter((x) => x.autoIdx == 2260).length > 0)
        this.DeliveryT = true;
      if (formMenus.filter((x) => x.autoIdx == 58).length > 0)
        this.model = true;
      if (formMenus.filter((x) => x.autoIdx == 2373).length > 0)
        this.merchantdizer = true;
      if (formMenus.filter((x) => x.autoIdx == 2374).length > 0)
        this.buyingofficemaster = true;
      if (formMenus.filter((x) => x.autoIdx == 2375).length > 0)
        this.dyetype = true;
      if (formMenus.filter((x) => x.autoIdx == 2378).length > 0)
        this.fabcomp = true;
      if (formMenus.filter((x) => x.autoIdx == 2379).length > 0)
        this.gender = true;
      if (formMenus.filter((x) => x.autoIdx == 2380).length > 0)
        this.gmttype = true;
      if (formMenus.filter((x) => x.autoIdx == 2381).length > 0)
        this.machinemaster = true;
      if (formMenus.filter((x) => x.autoIdx == 2382).length > 0)
        this.machinetoprocess = true;
      if (formMenus.filter((x) => x.autoIdx == 2383).length > 0)
        this.machtypes = true;
      if (formMenus.filter((x) => x.autoIdx == 2384).length > 0)
        this.processtype = true;
      if (formMenus.filter((x) => x.autoIdx == 2385).length > 0)
        this.receipetype = true;
      if (formMenus.filter((x) => x.autoIdx == 2386).length > 0)
        this.recipeutl = true;
      if (formMenus.filter((x) => x.autoIdx == 2387).length > 0)
        this.sampleissue = true;
      if (formMenus.filter((x) => x.autoIdx == 2388).length > 0)
        this.speoprtype = true;
      if (formMenus.filter((x) => x.autoIdx == 2389).length > 0)
        this.subsalescategory = true;
      if (formMenus.filter((x) => x.autoIdx == 2390).length > 0)
        this.useby = true;
      if (formMenus.filter((x) => x.autoIdx == 2391).length > 0)
        this.washstd = true;
        if (formMenus.filter((x) => x.autoIdx == 2393).length > 0)
        this.washtype = true;
        if (formMenus.filter((x) => x.autoIdx == 2392).length > 0)
        this.processtochemical = true;
        if (formMenus.filter((x) => x.autoIdx == 2394).length > 0)
        this.sampletype = true;      
    }
    if (formMenus.filter((x) => x.groupName == 'Orders').length > 0) {
      this.salesGroup = true;

      //// SUB MENU OF Order GROUP
      if (formMenus.filter((x) => x.autoIdx == 34).length > 0)
        this.salesOrder = true;
      if (formMenus.filter((x) => x.autoIdx == 87).length > 0)
        this.costAttach = true;
      if (formMenus.filter((x) => x.autoIdx == 2199).length > 0)
        this.salesOrderStatus = true;
      if (formMenus.filter((x) => x.autoIdx == 2209).length > 0)
        this.SalesOrderDefault = true;
      if (formMenus.filter((x) => x.autoIdx == 2283).length > 0)
        this.blockbooking = true;
      if (formMenus.filter((x) => x.autoIdx == 2372 ).length > 0)
        this.ordercreation = true;

    }
    if (formMenus.filter((x) => x.groupName == 'Purchasing').length > 0) {
      this.purchasingGroup = true;

      //// SUB MENU OF Order GROUP
      if (formMenus.filter((x) => x.autoIdx == 2211).length > 0)
        this.mrNote = true;
      if (formMenus.filter((x) => x.autoIdx == 2218).length > 0)
        this.purchasingOrder = true;
    }
    if (formMenus.filter((x) => x.groupName == 'Inventory').length > 0) {
      this.inventoryGroup = true;

      if (formMenus.filter((x) => x.autoIdx == 2232).length > 0)
        this.grn = true;
      if (formMenus.filter((x) => x.autoIdx == 2276).length > 0)
        this.stockAdj = true;
    }
    if (formMenus.filter((x) => x.groupName == 'Indent').length > 0) {
      this.indentGroup = true;

      //// SUB MENU OF Indent GROUP
      if (formMenus.filter((x) => x.autoIdx == 2220).length > 0)
        this.mrIndent = true;
      if (formMenus.filter((x) => x.autoIdx == 2221).length > 0)
        this.userIndent = true;
      if (formMenus.filter((x) => x.autoIdx == 2222).length > 0)
        this.adhocIndent = true;
    }

    if (formMenus.filter((x) => x.groupName == 'Production').length > 0) {
      this.productionGroup = true;

      if (formMenus.filter((x) => x.autoIdx == 80).length > 0)
        this.jobCard = true;
      if (formMenus.filter((x) => x.autoIdx == 81).length > 0)
        this.fpo = true;
      if (formMenus.filter((x) => x.autoIdx == 82).length > 0)
        this.fpoIn = true;
      if (formMenus.filter((x) => x.autoIdx == 83).length > 0)
        this.fpoOut = true;
      if (formMenus.filter((x) => x.autoIdx == 84).length > 0)
        this.qualityCon = true;
      if (formMenus.filter((x) => x.autoIdx == 85).length > 0)
        this.dispatch = true;
      if (formMenus.filter((x) => x.autoIdx == 2185).length > 0)
        this.min = true;
      if (formMenus.filter((x) => x.autoIdx == 2281).length > 0)
        this.crtntrnasfer = true;
        if (formMenus.filter((x) => x.autoIdx == 83).length > 0)
        this.productionissue = true;

      //MPlus 
      if (formMenus.filter((x) => x.autoIdx == 2312).length > 0)
        this.CPDProduction = true;
      if (formMenus.filter((x) => x.autoIdx == 2359).length > 0)
        this.FROrdAssociation = true;
      if (formMenus.filter((x) => x.autoIdx == 80).length > 0)
        this.recipe = true;

    }
    if (formMenus.filter((x) => x.groupName == 'Costing').length > 0) {

      if (formMenus.filter((x) => x.autoIdx == 86).length > 0)
        this.costing = true;
    }
    if (formMenus.filter((x) => x.groupName == 'Costing').length > 0) {

      if (formMenus.filter((x) => x.autoIdx == 86).length > 0)
        this.costsheet = true;
    }
    if (formMenus.filter((x) => x.groupName == 'Report').length > 0) {

      if (formMenus.filter((x) => x.autoIdx == 2182).length > 0)
        this.report = true;
    }
    if (formMenus.filter((x) => x.groupName == 'Finance').length > 0) {
      this.finance = true;

      if (formMenus.filter((x) => x.autoIdx == 167).length > 0)
        this.exchRate = true;
      if (formMenus.filter((x) => x.autoIdx == 169).length > 0)
        this.tax = true;
      if (formMenus.filter((x) => x.autoIdx == 172).length > 0)
        this.bank = true;
      if (formMenus.filter((x) => x.autoIdx == 174).length > 0)
        this.invoice = true;
      if (formMenus.filter((x) => x.autoIdx == 2179).length > 0)
        this.receipt = true;
      if (formMenus.filter((x) => x.autoIdx == 2204).length > 0)
        this.receiptAllocation = true;
      if (formMenus.filter((x) => x.autoIdx == 2216).length > 0)
        this.creditNote = true;
      if (formMenus.filter((x) => x.autoIdx == 2216).length > 0)
        this.creditNoteAllocation = true;
      if (formMenus.filter((x) => x.autoIdx == 2262).length > 0)
        this.debitNote = true;
      if (formMenus.filter((x) => x.autoIdx == 2351).length > 0)
        this.paymentinvoice = true;
    }

    if (formMenus.filter((x) => x.groupName == 'Workstudy').length > 0) {
      this.workstudyGroup = true;

      //// SUB MENU OF WORKSTUDY

      if (formMenus.filter((x) => x.autoIdx == 2285).length > 0)
        this.operationgroup = true;
      if (formMenus.filter((x) => x.autoIdx == 2291).length > 0)
        this.machinetype = true
      if (formMenus.filter((x) => x.autoIdx == 2294).length > 0)
        this.operation = true;
      if (formMenus.filter((x) => x.autoIdx == 2297).length > 0)
        this.departmentwisesectionmaster = true;
      if (formMenus.filter((x) => x.autoIdx == 2297).length > 0)
        this.factorywisesection = true;
      if (formMenus.filter((x) => x.autoIdx == 2300).length > 0)
        this.operationassigntofactory = true;
      if (formMenus.filter((x) => x.autoIdx == 2303).length > 0)
        this.operationassigntofactorysection = true;
      if (formMenus.filter((x) => x.autoIdx == 2306).length > 0)
        this.operationbreakdown = true;
      if (formMenus.filter((x) => x.autoIdx == 2312).length > 0)
        this.factorylayout = true;
      if (formMenus.filter((x) => x.autoIdx == 2360).length > 0)
        this.stylesmv = true;
      if (formMenus.filter((x) => x.autoIdx == 2364).length > 0)
        this.operationAssign = true;
      if (formMenus.filter((x) => x.autoIdx == 2362).length > 0)
        this.lostTime = true;
      if (formMenus.filter((x) => x.autoIdx == 2312).length > 0)
        this.folderandattachment = true;
      if (formMenus.filter((x) => x.autoIdx == 2312).length > 0)
        this.foot = true;
      if (formMenus.filter((x) => x.autoIdx == 2312).length > 0)
        this.mbrand = true;
      if (formMenus.filter((x) => x.autoIdx == 2312).length > 0)
        this.type = true;
      if (formMenus.filter((x) => x.autoIdx == 2312).length > 0)
        this.size = true;
    }

    if (formMenus.filter((x) => x.groupName == 'Transport').length > 0) {
      this.transportGroup = true;
      if (formMenus.filter((x) => x.autoIdx == 2315).length > 0)
        this.vehicletype = true;
      if (formMenus.filter((x) => x.autoIdx == 2316).length > 0)
        this.vehiclecategory = true;
      if (formMenus.filter((x) => x.autoIdx == 2317).length > 0)
        this.bookingtype = true;
      if (formMenus.filter((x) => x.autoIdx == 2318).length > 0)
        this.transporttype = true;
      if (formMenus.filter((x) => x.autoIdx == 2319).length > 0)
        this.paymenttype = true;
      if (formMenus.filter((x) => x.autoIdx == 2320).length > 0)
        this.route = true;
      if (formMenus.filter((x) => x.autoIdx == 2321).length > 0)
        this.routefactoryallocation = true;
      if (formMenus.filter((x) => x.autoIdx == 2322).length > 0)
        this.vehicleother = true;
      if (formMenus.filter((x) => x.autoIdx == 2323).length > 0)
        this.transpoter = true;
      if (formMenus.filter((x) => x.autoIdx == 2324).length > 0)
        this.transpoterfactoryallocation = true;
      if (formMenus.filter((x) => x.autoIdx == 2325).length > 0)
        this.ratematrix = true;
      if (formMenus.filter((x) => x.autoIdx == 2326).length > 0)
        this.approvalmatrix = true;
      if (formMenus.filter((x) => x.autoIdx == 2327).length > 0)
        this.bookingrequest = true;
      if (formMenus.filter((x) => x.autoIdx == 2346).length > 0)
        this.vehicleregister = true;
    }

    if (formMenus.filter((x) => x.groupName == 'Integration').length > 0) {
      this.integration = true;
      if (formMenus.filter((x) => x.autoIdx == 2264).length > 0)
        this.packagemapping = true;
    }

    //Fixed Asset 
    if (formMenus.filter((x) => x.groupName == 'FA').length > 0) {
      this.fixedAssetGroup = true;
      if (formMenus.filter((x) => x.autoIdx == 15002).length > 0)
        this.faMasterTab = true;
      if (formMenus.filter((x) => x.autoIdx == 15003).length > 0)
        this.fixedAssetDetails = true;
      if (formMenus.filter((x) => x.autoIdx == 2366).length > 0)
        this.machineBreak = true;
    }

    if (formMenus.filter((x) => x.groupName == 'Sales').length > 0) {
      this.sales = true;
      if (formMenus.filter((x) => x.autoIdx == 5078).length > 0)
        this.buyerPurchaseOrder = true;
    }

    if (formMenus.filter((x) => x.groupName == 'FA').length > 0) {
      this.sampleGroup = true;
      if (formMenus.filter((x) => x.autoIdx == 2366).length > 0)
        this.sampleProdIn = true;
      if (formMenus.filter((x) => x.autoIdx == 2366).length > 0)
        this.sampleProdOut = true;
    }
  }

  clearChangPwControls() {
    this.pwChangeForm.get("OldPassword").setValue("");
    this.pwChangeForm.get("cPassword").setValue("");
    this.pwChangeForm.get("confirmPassword").setValue("");
  }

  changeUserPassword() {

    var obj = {
      cAgentName: this.pwChangeForm.get("cAgentName").value,
      cPassword: this.pwChangeForm.get("cPassword").value,
      createUserId: this.user.userId
    }

    this.registerService.changeUserPassword(obj).subscribe(
      () => {
        //this.toastr.success('Password changed Successfully !!!');
        //this.pwChangeForm.reset();
        this.logout();
      },
      (error) => {
        this.validationErrors = error;
      });
  }
}
