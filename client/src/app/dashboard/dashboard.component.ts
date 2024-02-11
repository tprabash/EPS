import { Component, OnInit } from '@angular/core';
import { FormBuilder , FormGroup , Validators } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  dashboardForm : FormGroup;
  NewOrders: number = 0;
  dashboardList = [];

   constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.loadDashboarDetails();
  }


  loadDashboarDetails() {
    var objDt = {
      f09: 12,
      f20: 0
    };

  }
}
