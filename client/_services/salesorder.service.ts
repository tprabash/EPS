import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GRN } from 'src/app/_models/GRN';
import { POAssociation } from 'src/app/_models/POAssociation';
import { ProductionOut } from 'src/app/_models/ProductionOut';
import { environment } from 'src/environments/environment';

var usertoken: any;
if (localStorage.length > 0) {
  usertoken = localStorage.getItem('token');
}

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + usertoken
  }),
};

@Injectable({
  providedIn: 'root'
})
export class SalesorderService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  GetGRNData(wsDt: any) {
    return this.http.post<GRN[]>(this.baseUrl + 'SalesOrder/GetGRNData', wsDt, httpOptions);
  }

  SaveGRNData(wsDt: any) {
    return this.http.post(this.baseUrl + 'SalesOrder/SaveGRNData', wsDt, httpOptions);
  }


  GetPOAssociationData(wsDt: any) {
    return this.http.post<POAssociation[]>(this.baseUrl + 'SalesOrder/GetPOAssociationData', wsDt, httpOptions);
  }

  SavePOAssociationData(wsDt: any) {
    return this.http.post(this.baseUrl + 'SalesOrder/SavePOAssociationData', wsDt, httpOptions);
  }

  SaveOCData(wsDt: any) {
    return this.http.post(this.baseUrl + 'SalesOrder/SaveOCData' , wsDt , httpOptions );
  }

  GetProductionOutData(wsDt: any) {
    return this.http.post<ProductionOut[]>(this.baseUrl + 'SalesOrder/GetProductionOutData', wsDt, httpOptions);
  }

  SaveProductionOutData(wsDt: any) {
    return this.http.post(this.baseUrl + 'SalesOrder/SaveProductionOutData' , wsDt , httpOptions );
  }
}