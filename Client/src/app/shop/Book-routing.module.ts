import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { BookComponent } from './Book.component';

const routes: Routes = [

  {path: '', component: BookComponent},

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class BookRoutingModule { }
