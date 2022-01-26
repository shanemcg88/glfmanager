import { Component, OnInit } from '@angular/core';
import { TableColumn } from '../../shared/table/table-column';
import { Job } from './job';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {

  searchInput: string = '';
  jobs: Job[]=[];
  jobTableColumns: TableColumn[]=[];

  constructor() { }

  ngOnInit(): void {
  }

  searchField(userInput: string) {
  }

}
