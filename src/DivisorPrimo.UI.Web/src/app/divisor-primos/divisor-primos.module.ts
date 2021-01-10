import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DivisorPrimosComponent } from './divisor-primos.component';
import { MaterialModule } from 'app/core/material.module';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';
import { MessageModule } from 'primeng/message';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    DataTablesModule,
    MessageModule
  ],
  declarations: [
    DivisorPrimosComponent
  ]
})
export class DivisorPrimosModule { }
