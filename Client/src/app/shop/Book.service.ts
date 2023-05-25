import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import {delay, map} from 'rxjs/operators';
import { BookParams } from '../shared/models/BookParams';
import { IBook } from '../shared/models/Book';
// services need to be decorated with injectable decorator
@Injectable({
  providedIn: 'root'
})
export class BookService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http:HttpClient) { }

  getBooks(bookParams :BookParams) {

    let params = new HttpParams();


    if (bookParams.search) {
      params = params.append("search", bookParams.search)
    }


    params = params.append('pageIndex', bookParams.currentPage.toString());
    params = params.append('pageSize', bookParams.pageSize.toString());

    //observing response ==> give us http reponse instead of the body of the response ==> {observe: 'response', params}
    return this.http.get<IPagination>(this.baseUrl + 'books', {observe: 'response', params})
      .pipe(

        map(response => {
          return response.body;
        })
      )

  }





}
