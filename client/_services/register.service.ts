import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DelUserModule } from 'src/app/_models/delUserModule';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { UserLevel } from 'src/app/_models/userLevel';
import { environment } from 'src/environments/environment';
import { LocalService } from './local.service';
import { SysModule } from 'src/app/_models/sysModule';

var usertoken: any;
if (localStorage.length > 0) {
  // usertoken = JSON.parse(localStorage.getItem('token'));
  usertoken = localStorage.getItem('token');
  //console.log(usertoken);
}

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + usertoken,
  }),
};

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private localService: LocalService) {}

  getRegisteredUsres(userId: number) {
    return this.http.get<any>(
      this.baseUrl + 'Agents/regUser/' + userId,
      httpOptions
    );
  }

  getUser(id: number) {
    return this.http.get<Member[]>(this.baseUrl + 'Agents' + id, httpOptions);
  }

  getUserByName(userName: number) {
    return this.http.get<Member[]>(
      this.baseUrl + 'Agents/name/' + userName,
      httpOptions
    );
  }

  getLocation(model: any) {
    return this.http.post<Location[]>(
      this.baseUrl + 'Agents/Location',
      model,
      httpOptions
    );
  }

  // getUserLocation(model: any) {
  //   return this.http.post<UserLocation[]>(this.baseUrl + 'Agents/User/Location' , model , httpOptions);
  // }

  userRegister(model: any) {
    return this.http.post(
      this.baseUrl + 'account/register',
      model,
      httpOptions
    );
  }

  getSysModules() {
    return this.http.get<SysModule[]>(this.baseUrl + 'Agents/Module' );
  }

  getUserLevel() {
    const user: User = this.localService.getJsonValue('user');
    var userId = user.userId;
    // var userId = JSON.parse(localStorage.getItem('user')).userId;
    // console.log(httpOptions);
    return this.http.get<UserLevel[]>(
      this.baseUrl + 'Agents/AgentLevel/' + userId,
      httpOptions
    );
  }

  // changeUserPassword(userName: string , user: Member) {
  //   return this.http.put(this.baseUrl + 'Agents/' + userName , user , httpOptions);
  // }

  changeUserPassword(user: Member) {
    return this.http.post(this.baseUrl + 'Agents/ChPwdUser', user, httpOptions);
  }


  disableUser(user: Member) {
    return this.http.post(this.baseUrl + 'Agents/DeActUser', user, httpOptions);
  }

  deleteUserModule(userModule: DelUserModule) {
    return this.http.post(
      this.baseUrl + 'Agents/UserModDelete',
      userModule,
      httpOptions
    );
  }
}
