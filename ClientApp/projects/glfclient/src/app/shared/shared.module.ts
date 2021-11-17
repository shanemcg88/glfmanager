import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { InputComponent } from './input/input.component';
import { TableComponent } from './table/table.component';


@NgModule({
  declarations: [
    InputComponent,
    TableComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    InputComponent
  ]
})
export class SharedModule { }
