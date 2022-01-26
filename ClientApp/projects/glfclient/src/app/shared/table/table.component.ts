import { Component, Input, OnInit, ViewChild, OnChanges, SimpleChange, Output, EventEmitter } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { SharedTableService } from './shared-table.service';
import { BehaviorSubject } from 'rxjs';
import { TableColumn } from './table-column';
@Component({
  selector: 'custom-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  
  public tableDataSource: any;
  public displayedColumns: string[] = [];
  //pagination: boolean = false; // for when pagination doesn't initialize until the data is loaded

  @ViewChild(MatPaginator, {static: false}) matPaginator: MatPaginator | undefined;
  @ViewChild(MatSort, {static: true}) matSort: MatSort | undefined;

  @Input() isPageable = false;
  @Input() isSortable = false;
  @Input() isFilterable = false;
  @Input() tableColumns: TableColumn[] = [];
  @Input() rowActionIcon: string = '';
  @Input() paginationSizes: number[] = [5, 10, 15];
  @Input() defaultPageSize = this.paginationSizes[1];

  @Output() sort: EventEmitter<Sort> = new EventEmitter();
  @Output() rowAction: EventEmitter<any> = new EventEmitter<any>();

  // this property needs to have a setter, to dynamically get changes from parent component
  @Input() set tableData(data: any[]) {
    this.setTableDataSource(data);
  }

  constructor() {}

  ngOnInit(): void {
    const columnNames = this.tableColumns.map((tableColumn: TableColumn) => tableColumn.name);
    if (this.rowActionIcon) {
      this.displayedColumns = [this.rowActionIcon, ...columnNames]
    } else {
      this.displayedColumns = columnNames;
    }  
  }

  setTableDataSource(data: any) {
    this.tableDataSource = new MatTableDataSource<any>(data);
    this.tableDataSource.paginator = this.matPaginator;
    this.tableDataSource.sort = this.matSort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.tableDataSource.filter = filterValue.trim().toLowerCase();
  }

  sortTable(sortParameters: Sort) {
    // defining name of data property, to sort by, instead of column name
    sortParameters.active = this.tableColumns.find(column => column.name === sortParameters.active)?.dataKey as string;
    this.sort.emit(sortParameters);
  }

  emitRowAction(row: any) {
    this.rowAction.emit(row);
  }
}
