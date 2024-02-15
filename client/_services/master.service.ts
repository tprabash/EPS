
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Company } from 'src/app/_models/company';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { MWSMaster } from 'src/app/_models/mwsmaster';

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
export class MasterService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // getCompany(moduleId: number) {
  //   return this.http.get<Company[]>(this.baseUrl + 'Master/Company/' + moduleId,httpOptions);
  // }
  getCompany(moduleId: number) {
    return this.http.get<Company[]>(
      this.baseUrl + 'Master/Company/' + moduleId,
      httpOptions
    );
  }

  //#region "Size"

   //#region  MWS Master
   SaveMWSMasterData(wsDt: any) {
    //console.log(wsDt);
    return this.http.post(this.baseUrl + 'Master/SaveMWSMasterData', wsDt, httpOptions);
  }

  GetMWSMasterData(wsDt: any) {
    //console.log(wsDt);
    return this.http.post<MWSMaster[]>(this.baseUrl + 'Master/GetMWSMasterData', wsDt, httpOptions);
  }

  //#endregion  MWS Master
 
}
