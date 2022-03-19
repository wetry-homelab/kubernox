import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HostListComponent } from './components/host-list/host-list.component';
import { RouterModule, Routes } from '@angular/router';
import { MatTableModule } from '@angular/material/table';


const routes: Routes = [
  { path: '', component: HostListComponent }
];

@NgModule({
  declarations: [
    HostListComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    RouterModule.forChild(routes)
  ]
})
export class DatacenterModule { }
