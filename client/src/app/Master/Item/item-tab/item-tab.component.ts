import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AccountService } from '_services/account.service';

@Component({
  selector: 'app-item-tab',
  templateUrl: './item-tab.component.html',
  styleUrls: ['./item-tab.component.css']
})
export class ItemTabComponent implements OnInit {

  user: User;
  article: boolean = false;
  articleColor: boolean = false;
  articleSize: boolean = false;

  constructor(public accountServices: AccountService) { }

  ngOnInit(): void {
    this.accountServices.currentUser$.forEach((element) => {
      this.user = element;
    });
    this.checkMenuPermission();
  }

  checkMenuPermission() {
    var menus = this.user.permitMenus;
    // console.log(menus);
    
    var formMenus = menus.filter((x) => x.mType == 'F'); 
    if (formMenus.filter(x => x.autoIdx == 64 ).length > 0)
        this.article = true;
    if (formMenus.filter(x => x.autoIdx == 78 ).length > 0)
        this.articleColor = true;
    if (formMenus.filter(x => x.autoIdx == 79).length > 0)
        this.articleSize = true;
  }

}
