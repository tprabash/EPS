import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '_services/account.service';
import { AdminService } from '_services/admin.service';
import { LocalService } from '_services/local.service';
import { RegisterService } from '_services/register.service';
import { SysModule } from '../_models/sysModule';
import { User } from '../_models/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  model: any = {};
  sysModules: SysModule[];
  users: any;
  submitted = false;

  constructor(public accountServices: AccountService, private router: Router ,private fb: FormBuilder
      , private registerService: RegisterService, private localService: LocalService) { }

  ngOnInit(): void {
    this.initilizeForm();
    //console.log(this.accountServices.currentUser$);
    this.LoadModules();
  }

  initilizeForm() {
    this.loginForm = this.fb.group ({
      ModuleId: ['', Validators.required ],
      cAgentName: ['', [Validators.required , Validators.maxLength(20)]],
      cPassword: ['',[Validators.required , Validators.maxLength(24)]]
    })
  } 

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  login() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    else {
        //console.log(this.model["ModuleId"]);
      var obj = {
        moduleId: parseInt(this.loginForm.get('ModuleId').value[0]),
        cAgentName: this.loginForm.get('cAgentName').value,
        cPassword: this.loginForm.get('cPassword').value,
      };
      //console.log(obj);
      this.accountServices.login(obj).subscribe(response =>
      {
        //console.log(response);
        this.setCurrentUser();
        this.router.navigateByUrl('/Dashboard');
        //console.log(this.accountServices.decodedToken?.unique_name);
      });   
    }     
  }

  setCurrentUser() {
    const user: User = this.localService.getJsonValue('user');
    // const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountServices.setCurrentUser(user);   
  }

  // getAuthMenuList() {    
  //   this.adminService.getAuthMenuList().subscribe(response => {
  //     const menus = response;
  //     localStorage.setItem('menus', JSON.stringify(menus));
  //   });
  // }
  
  LoadModules() {
    this.registerService.getSysModules().subscribe(modules => {
      this.sysModules = modules;
      //console.log(modules);
    })
  }  

 


}
