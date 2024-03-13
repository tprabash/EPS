import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SharedModule } from './_modules/shared.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MainsidebarComponent } from './mainsidebar/mainsidebar.component';
import { FooterComponent } from './footer/footer.component';
import { UserRegisterComponent } from './Users/user-register/user-register.component';
import { DatePipe } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReportViewerModule } from 'ngx-ssrs-reportviewer';
import { BoldReportViewerModule } from '@boldreports/angular-reporting-components';
import { NgYasYearPickerModule } from 'ngy-year-picker';
import { IgxExcelExporterService, IgxCsvExporterService, IgxIconModule, IgxDatePickerModule, IgxComboModule, IgxInputGroupModule, IgxCheckboxModule, IgxRadioModule, IgxGridModule, IgxTabsModule, IgxMaskModule, IgxActionStripModule, IgxAvatarModule, IgxTooltipModule, IgxDialogModule, IgxCardModule, IgxDividerModule, IgxHierarchicalGridModule, IgxTimePickerModule, IgxExpansionPanelModule } from 'igniteui-angular';
import { CompanyMasterComponent } from './Master/company-master/company-master.component';
import { LocationMasterComponent } from './Master/location-master/location-master.component';
import { CategoryMasterComponent } from './Master/category-master/category-master.component';
import { SubCategoryMasterComponent } from './Master/sub-category-master/sub-category-master.component';
import { SizeMasterComponent } from './Master/size-master/size-master.component';
import { PrintMasterComponent } from './Master/print-master/print-master.component';
import { SupplierMasterComponent } from './Master/supplier-master/supplier-master.component';
import { CustomerMasterComponent } from './Master/customer-master/customer-master.component';
import { ItemMasterComponent } from './Master/Item/item-master/item-master.component';
import { GrnComponent } from './purchasing/grn/grn.component';
import { ItemTabComponent } from './Master/Item/item-tab/item-tab.component';
import { ColorTabComponent } from './Master/Item/color-tab/color-tab.component';
import { SizeTabComponent } from './Master/Item/size-tab/size-tab.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    LoginComponent,     
    DashboardComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MainsidebarComponent,
    FooterComponent,    
    UserRegisterComponent, 
    CompanyMasterComponent, 
    LocationMasterComponent, 
    CategoryMasterComponent, 
    SubCategoryMasterComponent, 
    SizeMasterComponent, 
    PrintMasterComponent, 
    SupplierMasterComponent, 
    CustomerMasterComponent, 
    ItemMasterComponent, GrnComponent, ItemTabComponent, ColorTabComponent, SizeTabComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    IgxIconModule,
    BrowserAnimationsModule,
    SharedModule,
    CollapseModule.forRoot(), 
    IgxDatePickerModule,
    IgxComboModule,
    IgxInputGroupModule,
    IgxCheckboxModule,
    IgxRadioModule,
    IgxGridModule,
    IgxTabsModule,
    IgxMaskModule,
    IgxActionStripModule,
    IgxAvatarModule,
    IgxTooltipModule,
    IgxDialogModule,
    IgxCardModule,
	  IgxDividerModule,
    ReportViewerModule,
    BoldReportViewerModule,
    IgxHierarchicalGridModule,
    NgYasYearPickerModule,
    BrowserModule,
    IgxTimePickerModule,
    IgxExpansionPanelModule
  ],
  providers: [
    DatePipe,
    ,IgxExcelExporterService, IgxCsvExporterService
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
