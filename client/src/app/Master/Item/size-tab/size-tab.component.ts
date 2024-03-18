import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IComboSelectionChangeEventArgs, IgxColumnComponent, IgxComboComponent, IgxGridComponent } from 'igniteui-angular';
import { ToastrService } from 'ngx-toastr';
import { Size } from 'src/app/_models/size';
import { User } from 'src/app/_models/user';
import { AccountService } from '_services/account.service';
import { MasterService } from '_services/master.service';

@Component({
  selector: 'app-size-tab',
  templateUrl: './size-tab.component.html',
  styleUrls: ['./size-tab.component.css']
})
export class SizeTabComponent implements OnInit {
  assignSizeForm: FormGroup;
  articleList: any[];
  pSizeList: Size[];
  npSizeList: Size[];
  asSaveButton: boolean = false;
  asRemoveButton: boolean = false;
  user: User;

  @ViewChild('sarticle', { read: IgxComboComponent })
  public sarticle: IgxComboComponent;

  @ViewChild('npSizeGrid', { static: true })
  public npSizeGrid: IgxGridComponent;
  @ViewChild('pSizeGrid', { static: true })
  public pSizeGrid: IgxGridComponent;

  public col: IgxColumnComponent;
  public pWidth: string;
  public nWidth: string;
  masterItemList: any[];
  allocSizeList: any[];
  itemidx: any;
  notallocSizeList: any[];
  
  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private masterService: MasterService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initilizeForm();
    this.loadArticles();
    this.DefaultSelection();
  }

  initilizeForm() {
    this.accountService.currentUser$.forEach((element) => {
      this.user = element;
      //console.log(this.user.userId);
    });

    var authMenus = this.user.permitMenus;

    if (authMenus != null) {
      if (authMenus.filter((x) => x.autoIdx == 136).length > 0) {
        this.asSaveButton = true;
      }
      if (authMenus.filter((x) => x.autoIdx == 137).length > 0) {
        this.asRemoveButton = true;
      }
    }

    this.assignSizeForm = this.fb.group({
      userId: this.user.userId,     
      sizeCard: [{value: '' , disabled: true }],     
      sarticle: ['']     
    });   
  }

   //// ALOW SINGLE SILECTION ONLY COMBO EVENT
   singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

  public onResize(event) {
    this.col = event.column;
    this.pWidth = event.prevWidth;
    this.nWidth = event.newWidth;
  }


  loadArticles(){
    this.masterItemList = [];
    var objOG = {
      ActivityNo: 62
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.masterItemList = OpGroupList;
      console.log('xxxxxxxxxxxxx');
      console.log(OpGroupList);
    });
  }
  
  
  onSelectArticle(event) {    
    for (const item of event.added) {
      this.loadAllocSizeDetails(item);
      this.loadNotAllocSizeDetails(item);
      console.log("onSelectArticle item", item);
    }
  }

  //// loads both permited and not permited Size list
  loadAllocSizeDetails(item) {
    this.allocSizeList = [];
    this.itemidx = item;
    var objOG = {
      ActivityNo: 71,
      f01:this.itemidx
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.allocSizeList = OpGroupList;
      console.log(OpGroupList);
    });
  }

  loadNotAllocSizeDetails(item) {
    this.notallocSizeList = [];
    this.itemidx = item;
    var objOG = {
      ActivityNo: 70,
      f01:this.itemidx
    };
    console.log(objOG);
    this.masterService.GetMWSMasterData(objOG).subscribe((OpGroupList) => {
      this.notallocSizeList = OpGroupList;
      console.log(OpGroupList);
    });
  }

  saveArticleSize() {

    var OGList = [];
    var machineallocationData = {};
    var objOG = {};
    var selectedRows = this.npSizeGrid.selectedRows;

    selectedRows.forEach((f01) => {
      machineallocationData = {
        ModuleId: f01,
        AutoId: this.assignSizeForm.get('sarticle').value[0]
      };

      console.log(machineallocationData);

      objOG = {
        sItem: machineallocationData,
        ActivityNo: 72,
        AgentNo:this.user.userId,
        ModuleNo:this.user.moduleId
      };
      OGList.push(objOG);
      console.log(OGList)
    });


      this.masterService.SaveMWSMasterData(OGList).subscribe((result) => {
            // console.log(result);
            if (result['result'] == 1) {
              this.toastr.success('save Successfully !!!');
              this.loadAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
              this.loadNotAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
            }
            else if (result['result'] == 2) {
              this.toastr.success('update Successfully !!!');
              this.loadAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
              this.loadNotAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
            }
            else if (result['result'] == 3) {
              this.toastr.success('Code already Exists!!!');
              this.loadAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
              this.loadNotAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
            }
            else {
              this.toastr.warning(
                'Contact Admin. Error No:- ' + result['result'].toString()
              );
            }
          });

      }

  deleteArticleSize() {
    var OGList = [];
    var machineallocationData = {};
    var objOG = {};
    var selectedRows = this.pSizeGrid.selectedRows;

    selectedRows.forEach((f01) => {
      machineallocationData = {
        ModuleId: f01,
        AutoId: this.assignSizeForm.get('sarticle').value[0]
      };

      console.log(machineallocationData);

      objOG = {
        sItem: machineallocationData,
        ActivityNo: 73,
        AgentNo:this.user.userId,
        ModuleNo:this.user.moduleId
      };
      OGList.push(objOG);
      console.log(OGList)
    });


      this.masterService.SaveMWSMasterData(OGList).subscribe((result) => {
            // console.log(result);
            if (result['result'] == 1) {
              this.toastr.error('Deleted Successfully !!!');
              this.loadAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
              this.loadNotAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
            }
            else if (result['result'] == 2) {
              this.toastr.success('update Successfully !!!');
              this.loadAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
              this.loadNotAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
            }
            else if (result['result'] == 3) {
              this.toastr.success('Code already Exists!!!');
              this.loadAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
              this.loadNotAllocSizeDetails(this.assignSizeForm.get('sarticle').value[0]);
            }
            else {
              this.toastr.warning(
                'Contact Admin. Error No:- ' + result['result'].toString()
              );
            }
          });
   
  }

  clearGridDetails(){
    this.npSizeGrid.deselectAllRows();
    this.pSizeGrid.deselectAllRows();
    this.npSizeList = [];
    this.pSizeList = [];
  }

  DefaultSelection(){
  
  }
}
