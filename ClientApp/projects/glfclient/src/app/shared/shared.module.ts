import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { InputComponent } from './input/input.component';
import { TableComponent } from './table/table.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { DataPropertyGetterPipe } from './table/data-property-getter-pipe/data-property-getter.pipe';
import { DataTypePipe } from './table/table-pipes/data-type.pipe'



@NgModule({
  declarations: [
    InputComponent,
    TableComponent,
    DataPropertyGetterPipe,
    DataTypePipe
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatInputModule,
    MatDialogModule,
  ],
  exports: [
    InputComponent,
    TableComponent
  ]
})
export class SharedModule { }
