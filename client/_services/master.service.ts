
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Company } from 'src/app/_models/company';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';

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

 
}
