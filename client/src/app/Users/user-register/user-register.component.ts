import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { IComboSelectionChangeEventArgs, IgxColumnComponent, IgxGridComponent } from 'igniteui-angular';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { UserLevel } from 'src/app/_models/userLevel';
import { AccountService } from '_services/account.service';
import { RegisterService } from '_services/register.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css'],
})
export class UserRegisterComponent implements OnInit {
  //@Input() usersFromHomeComponent: any;//get from parent
  //@Output() cancelRegister = new EventEmitter();// sent to parent
  userLevel: UserLevel[];
  registerForm: FormGroup;
  pwChangeForm: FormGroup;
  userSaveButton: boolean = false;
  userReg: boolean = false;
  userDisable: boolean = false;
  moduleReg: boolean = false;
  changePswd: boolean = false;
  pwdSaveButton: boolean = false;
  showPassword = false;
  regUserList: any;
  user: User;
  validationErrors: string[] = [];
  member: Member;

  public col: IgxColumnComponent;
  public pWidth: string;
  public nWidth: string;

  @ViewChild('regUserGrid', { static: true })
  public regUserGrid: IgxGridComponent;

  constructor(
    private registerService: RegisterService,
    private toastr: ToastrService,
    private accountService: AccountService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initilizeForm();
    this.getUserPermmision();
    this.LoadUserLevel();
    this.loadRegisterdUsers();
  }

  toggleShow() {
    this.showPassword = !this.showPassword;
    console.log(this.showPassword);
    // this.registerForm.controls.password.value().type = this.showPassword ? 'text' : 'password';
  }

  initilizeForm() {
    this.accountService.currentUser$.forEach((element) => {
      this.user = element;
    });

    this.registerForm = this.fb.group({
      cAgentName: ['', [Validators.required , Validators.maxLength(20)]],
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
      iCategoryLevel: ['', Validators.required],
      cDescription: [],
      cEmail: [],
    });
    this.registerForm.controls.cPassword.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    });

    this.pwChangeForm = this.fb.group({
      cAgentName: ['', Validators.required],
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
    this.pwChangeForm.controls.cPassword.valueChanges.subscribe(() => {
      this.pwChangeForm.controls.confirmPassword.updateValueAndValidity();
    });
  }

  /// CHECK THE PASSWORD MATCH BY THE CONFIRM PASSWORD
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
        ? null
        : { isMatching: true };
    };
  }

  /// IG COMBO SELECT ONLY ONE VALUE
  public singleSelection(event: IComboSelectionChangeEventArgs) {
    if (event.added.length) {
      event.newSelection = event.added;
    }
  }

  public onResize(event) {
    this.col = event.column;
    this.pWidth = event.prevWidth;
    this.nWidth = event.newWidth;
  }

  loadRegisterdUsers() {
    var userId = this.user.userId;
    this.registerService.getRegisteredUsres(userId).subscribe(result => {
      this.regUserList = result
      // console.log(this.regUserList);
    })
  }

  getUserPermmision() {
    var authMenus = this.user.permitMenus;

    if (authMenus != null) {
      if (authMenus.filter((x) => x.autoIdx == 29).length > 0) {
        this.userReg = true;
      } if (authMenus.filter((x) => x.autoIdx == 180).length > 0) {
        this.userDisable = true;
      } if (authMenus.filter((x) => x.autoIdx == 30).length > 0) {
        this.changePswd = true;
      } if (authMenus.filter((x) => x.autoIdx == 46).length > 0) {
        this.moduleReg = true;
      } if (authMenus.filter((x) => x.autoIdx == 89).length > 0) {
        this.userSaveButton = true;
      } if (authMenus.filter((x) => x.autoIdx == 91).length > 0) {
        this.pwdSaveButton = true;
      }
    }
  }

  register() {
    if (this.userSaveButton == true) {
      //this.registerForm.get('factoryId').setValue(this.registerForm.get('factoryId').value[0]);
      this.registerForm
        .get('iCategoryLevel')
        .setValue(this.registerForm.get('iCategoryLevel').value[0]);

      //console.log(this.registerForm.value);
      this.registerService.userRegister(this.registerForm.value).subscribe(
        () => {
          this.toastr.success('User Registered Successfully !!!');
          this.registerForm.reset();
          this.loadRegisterdUsers();
        },
        (error) => {
          this.validationErrors = error;
          //console.log(error);
          //this.toastr.error(error.error);
        }
      );
    } else {
      this.toastr.error('Save Permission denied !!!');
    }
  }


  //// LOADS PERMITED USER LEVEL
  LoadUserLevel() {
    this.registerService.getUserLevel().subscribe((levels) => {
      this.userLevel = levels;
    });
  }

  /// CANCEL THE USER REGISTRATION
  cancelRegister() {
    this.registerForm.reset();
  }

  deactive(cellValue, cellId) {
    const id = cellId.rowID;

    var obj = {      
      createUserId: this.user.userId,
      idAgents: id,
      bActive: false,
    };
    this.deactiveUser(obj, 'Deactive');
  }

  active(cellValue, cellId) {
    const id = cellId.rowID;

    var obj = {
      createUserId: this.user.userId,
      idAgents: id,
      bActive: true,
    };
    this.deactiveUser(obj, 'Active');
  }

  deactiveUser(obj, status) {
    if(this.userDisable == true) {
    this.registerService.disableUser(obj).subscribe(
      (result) => {
        if (result == 1) {
          this.toastr.success('User ' + status + ' Successfully !!!');
          this.loadRegisterdUsers();
        } else if (result == 2) {
          this.toastr.success('User ' + status + ' Successfully !!!');
          this.loadRegisterdUsers();
        } else if (result == -1) {
          this.toastr.warning("Can't Deactive, User details exists !!!");
        } else {
          this.toastr.error('Contact Admin. Error No:- ' + result.toString());
        }
      },
      (error) => {
        this.validationErrors = error;
      }
    );
    } else {
      this.toastr.error('Disable Permission denied !!!');
    }
  }

  changeUserPassword() {
    if(this.pwdSaveButton == true) {
    var obj = {
      cAgentName: this.pwChangeForm.get("cAgentName").value,
      cPassword: this.pwChangeForm.get("cPassword").value,
      createUserId: this.user.userId
    }  

    this.registerService.changeUserPassword(obj).subscribe(
      () => {
        this.toastr.success('Password changed Successfully !!!');
        this.pwChangeForm.reset();
      },
      (error) => {
        this.validationErrors = error;
      });
    } else {
      this.toastr.error('Save permission denied !!!');
    }
  }

  searchUserPassword() {
    // this.pwChangeForm.get('cAgentName').
    var userName = this.pwChangeForm.get('cAgentName').value;
    this.clearChangPwControls();

    // console.log(userName);
    if (this.pwChangeForm.get('cAgentName').value != "" ) {
      this.registerService.getUserByName(userName).subscribe(
        (member) => {
          // console.log(member["cPassword"]);
          this.pwChangeForm.get('OldPassword').setValue(member['cPassword']);
        },
        (error) => {
          //console.log(error);
          this.validationErrors = error;
        });
    } else {
      this.toastr.warning("User Name is required !!!");      
    }
  }

  clearChangPwControls() {
    this.pwChangeForm.get("OldPassword").setValue("");
    this.pwChangeForm.get("cPassword").setValue("");
    this.pwChangeForm.get("confirmPassword").setValue("");
  }

  resetForm() {
    this.pwChangeForm.reset();
    this.pwChangeForm.get('cAgentName').setValue("");
  }

  clearPassword(event: any) {
    this.pwChangeForm.get('OldPassword').setValue('');
  }

}
