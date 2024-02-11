import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter, map, mergeMap } from 'rxjs/operators';
import { AccountService } from '_services/account.service';
import { LocalService } from '_services/local.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit { 
  title = 'MTrack';
  users: any;
  visible: boolean = false;

  constructor(private http: HttpClient,public accountServices: AccountService,private localService: LocalService) { }
 
  ngOnInit() {
   //this.getUsers();
   this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = this.localService.getJsonValue('user');
    // const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountServices.setCurrentUser(user);
    // const menu: any = JSON.parse(localStorage.getItem('menus'));
    // console.log(menu);
  }


    
  

  // getUsers() {
  //   this.http.get('https://localhost:5001/api/Agents').subscribe(response => {
  //     this.users = response;
  //   }, error =>{
  //     console.log(error);
  //   });
  // }
}
