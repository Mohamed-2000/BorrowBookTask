import { Component, Input, OnInit } from '@angular/core';
import { IBook } from 'src/app/shared/models/Book';

@Component({
  selector: 'app-book-item',
  templateUrl: './book-item.component.html',
  styleUrls: ['./book-item.component.scss']
})
export class bookItemComponent implements OnInit {

  @Input()
  book!: IBook;
  constructor() { }

  ngOnInit(): void {
  }

}
