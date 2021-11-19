import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';

import { MainRoutingModule } from './main-routing.module';
import { HomeComponent } from './home/home.component';
import { ClientsComponent } from './clients/clients.component';
import { JobsComponent } from './jobs/jobs.component';
import { EmployeesComponent } from './employees/employees.component';


@NgModule({
  declarations: [
    HomeComponent,
    ClientsComponent,
    JobsComponent,
    EmployeesComponent,
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    HttpClientModule,
    DataTablesModule
  ]
})
export class MainModule { }
