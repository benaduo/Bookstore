import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Book } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getBooks() {
    return this.http.get(this.baseUrl + '/book');
  }


  getBookById(id: number) {
    return this.http.get(this.baseUrl + `/book/${id}`, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      })
    });
  }

  addBook(book: Book) {
    return this.http.post(this.baseUrl + '/book', book, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      })
    });
  }

  updateBook(book: Book) {
    return this.http.put(this.baseUrl + '/book', book, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      })
    });
  }

  deleteBook(id: number) {
    return this.http.delete(this.baseUrl + `/book/${id}`, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      })
    });
  }
}
