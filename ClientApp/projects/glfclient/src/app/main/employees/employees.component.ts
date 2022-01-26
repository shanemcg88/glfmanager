import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeesService } from './employees.service';
import { Employee } from './employee';
import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { TableColumn } from '../../shared/table/table-column';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  searchInput: string = '';
  employees: Employee[] = [];
  employeeTableColumns: TableColumn[]=[];

  constructor(
    private employeeService: EmployeesService,
    private liveAnnouncer: LiveAnnouncer
  ) { }

  ngOnInit(): void {
    this.initColumns();
    this.employeeService.getEmployees().subscribe((response) => {
      this.employees = response;
    });
  }

  initColumns(): void {
    this.employeeTableColumns = [
      {
        name: 'First Name',
        dataKey: 'firstName'
      },
      {
        name: 'Last Name',
        dataKey: 'lastName'
      },
      {
        name: 'Email',
        dataKey: 'email'
      },
      {
        name: 'Phone Number',
        dataKey: 'phoneNumber'
      }
    ]
  }
    
    announceSortChange(sortState: Sort) {
     if (sortState.direction) {
       this.liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
     } else {
       this.liveAnnouncer.announce('Sorting cleared');
     }
   }

   searchField(userInput: string) {
    console.log('userinput=', userInput);
  }
}
