import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from 'src/app/models/book';
import { Category } from 'src/app/models/category';
import { BookService } from 'src/app/services/book.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-update-book',
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.css']
})
export class UpdateBookComponent implements OnInit {

  updateBookForm!: FormGroup;
  categories: Array<Category> = [];
  book: Book = {} as Book;
  message: string = '';
  bookId: number = 0;

  constructor(
    private categoryService: CategoryService,
    private bookService: BookService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute
  ) {

    if (this.route.snapshot.params["id"]) {
      this.bookId = this.route.snapshot.params["id"];
    }

    this.updateBookForm = formBuilder.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      price: ['', Validators.required],
      description: ['', Validators.required],
      categoryId: ['', Validators.required]
    });
  }

  ngOnInit(): void {

    this.categoryService.getCategories().subscribe({
      next: (res: any) => {
        this.categories = res;
      },
      error: (err) => { this.message = err.error.error; },
      complete: () => { }
    });

    this.bookService.getBookById(this.bookId).subscribe({
      next: (res: any) => {
        this.book = res;
      },
      error: (err) => { },
      complete: () => { }
    });

    this.bookService.getBookById(this.bookId).subscribe((data: any) => {
      this.book = data;
    });
  }

  onUpdate() {
    if (this.updateBookForm.invalid) {
      this.message = "One or more values are invalid.";
      return;
    }

    this.bookService.updateBook(this.updateBookForm.value).subscribe({
      next: () => {
        this.message = "Book updated successfully.";
        this.updateBookForm.reset();
        this.router.navigate(['/books']);
      },
      error: (err) => { this.message = err.error.error; },
      complete: () => { }
    });
  }

}
