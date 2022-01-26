import { Component, OnInit, ViewChild } from '@angular/core';
import { ClientsService } from './clients.service';
import { Client } from './client';
import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { SharedTableService } from '../../shared/table/shared-table.service';
import { TableColumn } from '../../shared/table/table-column';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {
  searchInput: string = '';
  clients: Client[]=[];
  clientsTableColumns: TableColumn[]=[];

  constructor(
    private clientService: ClientsService,
    private liveAnnouncer: LiveAnnouncer
  ) {}

  ngOnInit(): void {
    this.initColumns();
    this.clientService.getClients().subscribe((response) => {
      this.clients = response;
    })
  }

  initColumns(): void {
    this.clientsTableColumns = [
      {
        name: 'Name',
        dataKey: 'name'
      },
      {
        name: 'Address',
        dataKey: 'address'
      },
      {
        name: 'Phone',
        dataKey: 'officePhone'
      },
      {
        name: 'Email',
        dataKey: 'officeEmail'
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
