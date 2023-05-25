import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookComponent } from './Book.component';
import { SharedModule } from '../shared/shared.module';
import { BookRoutingModule } from './Book-routing.module';
import { bookItemComponent } from './Book-item/book-item.component';



@NgModule({
  declarations: [
    BookComponent,
    bookItemComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    BookRoutingModule
  ]
})
export class ShopModule { }
