import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

import { IBook } from '../shared/models/Book';
import { BookParams } from '../shared/models/BookParams';
import { BookService } from './Book.service';

@Component({
  selector: 'app-shop',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {
  @ViewChild('search', { static: true })
  searchTerm!: ElementRef;
  books: IBook[] = [];

  totalCount = 0;
  bookParams = new BookParams();
  sortOptions = [
    {name : 'Alphabetical', value : 'name'},
    {name : 'Price: Low To High', value : 'priceAsc'},
    {name : 'Price: High To Low', value : 'priceDesc'}, //value spelling is important because api is accept parameters spelled like this
  ]
  constructor(private bookService:BookService) { }

  ngOnInit(): void {
    this.getbooks();

  }

  getbooks() {
    this.bookService.getBooks(this.bookParams)
      .subscribe(response => {
      if (response) {
        this.books = response.data;
        this.bookParams.currentPage = response.currentPage;
        this.bookParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      }
    }, error => {
      console.log(error);

    });
  }




  onPageChanged(event :any) {
    if (this.bookParams.currentPage !== event) {
      this.bookParams.currentPage = event;
      this.getbooks();
    }
  }

  onSearch() {
    this.bookParams.search = this.searchTerm?.nativeElement.value;
    this.bookParams.currentPage = 1;
    this.getbooks();
  }

  onReset() {

    this.searchTerm.nativeElement = '';
    this.bookParams = new BookParams(); //initialize all our values with their default values
    this.getbooks();
  }
}
