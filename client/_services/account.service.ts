import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { environment } from 'src/environments/environment';
import { LocalService } from './local.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient, private localService: LocalService) { }

  login(model: any){
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if(user) {
          // console.log(user);
          this.localService.storagesetJsonValue('user', user);

          localStorage.setItem('token', user.token);
          // localStorage.setItem('user', JSON.stringify(user));
          // var userdt = this.localService.getJsonValue('user');
        
          this.currentUserSource.next(user);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          //console.log(this.decodedToken);
          //this.currentUser = user.user;
        }
        //return user;
      })
    );
  }  

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout() {
    // localStorage.removeItem('user');
    // localStorage.removeItem('token');
    localStorage.clear();
    this.currentUserSource.next(null);
  }

  loggedIn() {
    var token = localStorage.getItem('token');
    // decodedToken = this.jwtHelper.decodeToken(myRawToken);
    // expirationDate = this.jwtHelper.getTokenExpirationDate(myRawToken);
    //console.log(this.jwtHelper.isTokenExpired(token));
    return !this.jwtHelper.isTokenExpired(token);
  }

  // refreshToken(model: any) {
  //   return this.http.post<any>(this.baseUrl + 'account/refresh', model)
  //       .pipe(map((user) => {
  //         this.localService.storagesetJsonValue('user', user);
  //         localStorage.setItem('token', user.token);
  //         this.currentUserSource.next(user);
  //         this.decodedToken = this.jwtHelper.decodeToken(user.token);
  //       }));
  //     }


  
}
