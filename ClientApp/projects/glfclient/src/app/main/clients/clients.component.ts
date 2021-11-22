import { Component, OnInit, ViewChild } from '@angular/core';
import { ClientsService } from './clients.service';
import { Client } from './client';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {

  pagination: boolean = false; // for when pagination doesn't initialize until the data is loaded
  displayedColumns: string[] = ['name', 'address', 'officeEmail', 'officePhone'];
  dataSource = new MatTableDataSource<Client>();
  searchInput: string = '';
  clients: Client[]=[];

  @ViewChild(MatPaginator, { static: false }) set matPaginator(val: MatPaginator) {
    if (this.pagination) {
      this.dataSource.paginator = val;
    }
  }

  @ViewChild(MatSort, { static: false }) set matSort(val: MatSort) {
    this.dataSource.sort = val;
  }

  constructor(
    private clientService: ClientsService,
    private liveAnnouncer: LiveAnnouncer
  ) {}

  ngOnInit(): void {
    this.clientService.getClients().subscribe((response) => {
      this.dataSource.data=response;
      this.clients=response;
      this.pagination = true;
    })
    this.dataSource.filterPredicate = (data: Client, filter: string) => {
      return data.name.toLowerCase().includes(filter.toLowerCase());
     };
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
    this.dataSource.filter=userInput;
  }
}
