import { Component, OnInit, ViewChild } from '@angular/core';
import { TableColumn } from '../../shared/table/table-column';
import { Job } from './job';
import { JobsService } from './jobs.service';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatSort, Sort } from '@angular/material/sort';


@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {

  searchInput: string = '';
  jobs: Job[]=[];
  dailyJobTableColumns: TableColumn[]=[];
  employeeString: string = '';

  constructor(
    private jobService: JobsService,
    private liveAnnouncer: LiveAnnouncer
  ) { }

  ngOnInit(): void {
    this.initColumns();
    this.jobService.getDailyJobs().subscribe((response: any) => {
      // Creating a string of employees for the Workers column
      var emps = response[0].employeeList

      for (let i = 0; i <= emps.length-1; i++) {
        if (i === emps.length-1 || emps.length <=1 )
          this.employeeString += emps[i]['firstName'] + ' ' + emps[i]['lastName'];
        else 
          this.employeeString += emps[i]['firstName'] + ' ' + emps[i]['lastName'] + ', ';
      }

      // Replacing so the 'dataKey' can pick up the string instead of an Array
      response[0].employeeList = this.employeeString;
      this.jobs = response;
    })

    
  }


  initColumns(): void {
    this.dailyJobTableColumns = [
      {
        name: 'Company',
        dataKey: 'companyName'
      },
      {
        name: 'Site Address',
        dataKey: 'address'
      },
      {
        name: 'Contact',
        dataKey: 'contact'
      },
      {
        name: 'Phone Number',
        dataKey: 'phoneNumber'
      },
      {
        name: 'Date',
        dataKey: 'dateOfJob'
      },
      {
        name: 'Workers',
        dataKey: 'employeeList'
      }
    ];
  }

  searchField(userInput: string) {
  }

}
