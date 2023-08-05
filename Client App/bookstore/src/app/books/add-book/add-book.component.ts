import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { BookService } from 'src/app/services/book.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {

  addBookForm!: FormGroup;
  categories: Array<Category> = [];
  message: string = '';

  constructor(
    private categoryService: CategoryService,
    private bookService: BookService,
    private formBuilder: FormBuilder,
    private router: Router
    ) {

    this.addBookForm = this.formBuilder.group({
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

  }

  onSubmit() {
    if (this.addBookForm.invalid) {
      this.message = "One or more values are invalid.";
      return;
    }

    this.bookService.addBook(this.addBookForm.value).subscribe({
      next: () => {
        this.message = "Book added successfully.";
        this.addBookForm.reset();
        this.router.navigate(['/books']);
      },
      error: (err) => { this.message = err.error.error; },
      complete: () => { }

    });
  }


}
