import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RouterTestingModule } from '@angular/router/testing';

import { BooksListComponent } from './books-list.component';
import { of } from 'rxjs';
import { Book } from 'src/app/models/book';
import { Category } from 'src/app/models/category';
import { BookService } from 'src/app/services/book.service';
import { CategoryService } from 'src/app/services/category.service';

describe('BooksListComponent', () => {
  let component: BooksListComponent;
  let fixture: ComponentFixture<BooksListComponent>;
  let bookService: jasmine.SpyObj<BookService>;
  let categoryService: jasmine.SpyObj<CategoryService>;

  beforeEach(async () => {
    const bookSpy = jasmine.createSpyObj('BookService', ['getBooks', 'deleteBook']);
    const categorySpy = jasmine.createSpyObj('CategoryService', ['getCategories']);

    await TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [BooksListComponent],
      providers: [
        { provide: BookService, useValue: bookSpy },
        { provide: CategoryService, useValue: categorySpy },
      ],
    }).compileComponents();

    bookService = TestBed.inject(BookService) as jasmine.SpyObj<BookService>;
    categoryService = TestBed.inject(CategoryService) as jasmine.SpyObj<CategoryService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BooksListComponent);
    component = fixture.componentInstance;
  });

  it('should map category names to books', () => {

    const categories: Category[] = [
      { id: 1, name: 'Fiction', description: 'Books on fictional characters' },
      { id: 2, name: 'Science', description: 'Books on science and nature' },
    ];

    const books: Book[] = [
      { id: 1, name: 'The gods are not to blame', price: 19.99, description: 'An Ola Rotimi literature', categoryId: 1 },
      { id: 2, name: 'In the chest of a woman', price: 24.99, description: 'A play by Efo Kojo Mawugbe', categoryId: 2 },
    ];

    bookService.getBooks.and.returnValue(of(books));
    categoryService.getCategories.and.returnValue(of(categories));

    fixture.detectChanges();

    expect(component.books[0].categoryName).toBe('Fiction');
    expect(component.books[1].categoryName).toBe('Science');
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
