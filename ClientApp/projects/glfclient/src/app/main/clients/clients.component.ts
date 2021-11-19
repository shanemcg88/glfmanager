import { Component, OnInit } from '@angular/core';
import { DataTableDirective, DataTablesModule } from 'angular-datatables';
import { ClientsService } from './clients.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private clientService: ClientsService) { }

  ngOnInit(): void {
    this.clientService.getClients().subscribe((response) => {console.log('clientresponse', response)})
  }

}
