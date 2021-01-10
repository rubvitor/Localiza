import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { DivisorPrimosModule } from '../../divisor-primos/divisor-primos.module';
import { MaterialModule } from 'app/core/material.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    DivisorPrimosModule
  ],
  declarations: [
    UserProfileComponent
  ]
})

export class AdminLayoutModule {}
