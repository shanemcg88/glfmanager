import { Injectable } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
@Injectable()
export class SharedTableService {
  private columnSource = new BehaviorSubject(['']);
  displayedColumns$ = this.columnSource.asObservable();

  private dataSource = new BehaviorSubject(MatTableDataSource)
  displaySource$ = this.dataSource.asObservable();

  constructor() { }

  getColumns() {
    // return this.displayedColumns;
    return null;
  }
  setColumns(columns: string[]=[]) {
    this.columnSource.next(columns);
  }

  getSource() {
    return this.dataSource;
  }

  setSource(source: any) {
    this.dataSource.next(source);
  }
}
