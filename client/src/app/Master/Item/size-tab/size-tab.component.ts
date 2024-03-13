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

  loadArticles() {

  }

  onSelectArticle(event) {    
    this.clearGridDetails();
    this.assignSizeForm.get("sizeCard").setValue("");
    for (const item of event.added) {
      var selectedRow = this.articleList.filter(x => x.autoId == item);

      if(selectedRow.length > 0) {
        this.assignSizeForm.get("sizeCard").setValue(selectedRow[0]["sizeCard"]);
      }
      this.loadSizeDetails(item);
    }
  }

  //// loads both permited and not permited Size list
  loadSizeDetails(articleId) {
   
  }

  saveArticleSize() {
    
  }

  deleteArticleSize() {
   
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
