import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IBook } from 'src/app/shared/models/Book';
import { BookService } from '../Book.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss']
})
export class bookDetailsComponent implements OnInit {
  book!: IBook;
  //bookId!: string;
  constructor(private bookService:BookService, private activateRoute:ActivatedRoute) { }

  ngOnInit(): void {
    // this.loadbook();
  }

  // loadbook () {
  //     // this.shopService.getbook(+this.activateRoute.snapshot.paramMap.get('id')).subscribe ==> udemy code
  //     //console.log(+this.activateRoute.snapshot.params['id']);
  //     //this.bookId = this.activateRoute.snapshot.params.id;
  //     //var convertIdToString = Number(this.activateRoute.snapshot.paramMap.get('id'))
  //     //console.log(Number(this.bookId));

  //     // this.activateRoute.paramMap.subscribe(params => {
  //     //   if (this.bookId) {
  //     //   this.bookId = params.get('id');

  //     //   }
  //     // });


  //     this.activateRoute.params.subscribe(a=> {
  //       this.bookService.getbook(a['id']).subscribe( book =>
  //         this.book = book
  //       )
  //     }, error => {
  //       console.log(error);

  //     });
  //     // this.shopService.getbook(+this.bookId).subscribe(book =>
  //     // {
  //     //   if(book){
  //     //     this.book = book;

  //     //   }
  //     // }, error => {
  //     //   console.log(error);

  //     // });
  // }

}
