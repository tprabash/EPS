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
  selector: 'app-grn',
  templateUrl: './grn.component.html',
  styleUrls: ['./grn.component.css'],
})
export class GrnComponent implements OnInit {
  masterSupplierForm: FormGroup;
  user: User;
  saveButton: boolean = false;
  masterCompanyList = [];
  supplierList = [];
  grnTypeList = [];

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
    }
    this.masterSupplierForm = this.fb.group({
      autoId: [0],
      createUserId: this.user.userId,
      docno: ['', [Validators.required, Validators.maxLength(50)]],
      supname: ['', [Validators.required, Validators.maxLength(50)]],
      supreff: ['', [Validators.required, Validators.maxLength(50)]],
      supplier: ['', [Validators.required, Validators.maxLength(50)]],
      transDate: ['', [Validators.required, Validators.maxLength(50)]],
      docDate: ['', [Validators.required, Validators.maxLength(50)]],
      grntype: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  companysave() {
    if (this.saveButton == true) {
      var Obj = {};
    } else {
      this.toaster.error('Save Permission Denied !!!');
    }
  }

  refreshcompany() {}

  onEditCompany(event, cellId) {}
}
