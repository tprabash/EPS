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

@Component({
  selector: 'app-supplier-master',
  templateUrl: './supplier-master.component.html',
  styleUrls: ['./supplier-master.component.css']
})
export class SupplierMasterComponent implements OnInit {

  masterSupplierForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  masterCompanyList = [];

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private masterService: MasterService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
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
      //console.log(this.saveButton);
    }
    this.masterSupplierForm = this.fb.group({
      autoId: [0],
      createUserId: this.user.userId,
      code: ['', [Validators.required, Validators.maxLength(50)]],
      supname: ['', [Validators.required, Validators.maxLength(50)]],
      address: ['', [Validators.required, Validators.maxLength(50)]],
      contactperson: ['', [Validators.required, Validators.maxLength(50)]],
      creditperiod: ['', [Validators.required, Validators.maxLength(50)]],
      creditlimit: ['', [Validators.required, Validators.maxLength(50)]],
      contactno: ['', [Validators.required, Validators.maxLength(50)]],
      vatno: ['', [Validators.required, Validators.maxLength(50)]],
      svatno: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  companysave() {
    if (this.saveButton == true) {
      var Obj = {};
      //console.log(Obj);
    } else {
      this.toaster.error('Save Permission Denied !!!');
    }
  }

  refreshcompany() {}

  onEditCompany(event, cellId) {}

}
