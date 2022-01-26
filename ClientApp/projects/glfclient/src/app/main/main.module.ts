import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { SharedModule } from '../shared/shared.module';

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
    DataTablesModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatInputModule,
    MatDialogModule,
    SharedModule
  ]
})
export class MainModule { }
