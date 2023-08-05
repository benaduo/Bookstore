import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Book } from 'src/app/models/book';
import { Category } from 'src/app/models/category';
import { BookService } from 'src/app/services/book.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})
export class BooksListComponent implements OnInit {

  categories: Array<Category> = [];
  books: Array<Book> = [];
  message: string = '';

  constructor(
    private bookService: BookService,
    private categoryService: CategoryService,
    private router: Router) { }

  ngOnInit(): void {

    this.bookService.getBooks().subscribe({
      next: (res: any) => {
        this.books = res;
      },
      error: (err) => { },
      complete: () => { }
    });

    this.categoryService.getCategories().subscribe({
      next: (res: any) => {
        this.categories = res;
        this.mapCategoryToBook();

      },
      error: (err) => { this.message = err.error.error; },
      complete: () => { }
    });
  }

  mapCategoryToBook() {
    if (this.books.length === 0 || this.categories.length === 0) return;

    for (const book of this.books) {
      const category = this.categories.find((c) => c.id === book.categoryId);
      if (category) {
        book.categoryName = category.name;
      }
    }
  }

  updateBook(id: number) {
    this.router.navigate(['/books/update', id]);
  }

  deleteBook(id: number) {
    this.bookService.deleteBook(id).subscribe({
      next: () => {
        this.message = "Book deleted successfully.";
        this.ngOnInit();
      },
      error: (err) => { this.message = err.error.error; },
      complete: () => { }
    });
  }

  logout(){
    sessionStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }
}
