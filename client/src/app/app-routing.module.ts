import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserRegisterComponent } from './Users/user-register/user-register.component';
import { CompanyMasterComponent } from './Master/company-master/company-master.component';
import { LocationMasterComponent } from './Master/location-master/location-master.component';
import { CategoryMasterComponent } from './Master/category-master/category-master.component';
import { SubCategoryMasterComponent } from './Master/sub-category-master/sub-category-master.component';
import { SizeMasterComponent } from './Master/size-master/size-master.component';
import { PrintMasterComponent } from './Master/print-master/print-master.component';
import { SupplierMasterComponent } from './Master/supplier-master/supplier-master.component';
import { CustomerMasterComponent } from './Master/customer-master/customer-master.component';
import { GrnComponent } from './purchasing/grn/grn.component';
import { ItemTabComponent } from './Master/Item/item-tab/item-tab.component';

const routes: Routes = [
  {path:'', component: LoginComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'Dashboard', component: DashboardComponent },
      { path: 'Home', component: HomeComponent },
      { path: 'UserRegister', component: UserRegisterComponent },
      { path: 'Dashboard', component: DashboardComponent },
      { path: 'Home', component: HomeComponent},
      { path: 'UserRegister', component: UserRegisterComponent },
      { path: 'company-master', component: CompanyMasterComponent },
      { path: 'location-master', component: LocationMasterComponent},
      { path: 'category-master', component: CategoryMasterComponent },
      { path: 'sub-category-master', component: SubCategoryMasterComponent },
      { path: 'size-master', component: SizeMasterComponent },
      { path: 'print-master', component: PrintMasterComponent },
      { path: 'supplier-master', component: SupplierMasterComponent },
      { path: 'customer-master', component: CustomerMasterComponent },
      { path: 'item-master', component: ItemTabComponent },
      { path: 'grn', component: GrnComponent }
    ]
  },  
  {path:'not-found', component: NotFoundComponent},
  {path:'server-error', component: ServerErrorComponent},
  {path:'errors', component: TestErrorsComponent},  
  {path:'**', component: NotFoundComponent , pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
