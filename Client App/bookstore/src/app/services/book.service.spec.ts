import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { BookService } from './book.service';
import { Book } from '../models/book';

describe('BookService', () => {
  let service: BookService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, HttpClientTestingModule],
      providers: [BookService]
    });
    service = TestBed.inject(BookService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should retrieve books from the server', () => {
    const mockBooks: Book[] = [
      { id: 1, name: 'Ananse in the land of idiots', price: 70, description: 'A book on the corny adventures of Kwaku Ananse', categoryId: 1 },
      { id: 2, name: 'A long walk to freedom', price: 50, description: 'As written by Ghana\'s first president', categoryId: 2 }
    ];

    service.getBooks().subscribe({
      next: books => {
        expect(books).toBeTruthy();
        expect(books).toEqual(mockBooks);
      }
    });

    const request = httpMock.expectOne(service.baseUrl + '/book');
    expect(request.request.method).toBe('GET');

    request.flush(mockBooks);
  })

  it('should retrieve a book by id from the server', () => {
    const mockBook: Book = {
      id: 1,
      name: 'Ananse in the land of idiots',
      price: 70,
      description: 'A book on the corny adventures of Kwaku Ananse',
      categoryId: 1
    }

    const bookId = 1;

    service.getBookById(bookId).subscribe({
      next: book => {
        expect(book).toBeTruthy();
        expect(book).toEqual(mockBook);
      }
    });

    const request = httpMock.expectOne(service.baseUrl + `/book/${bookId}`);
    expect(request.request.method).toBe('GET');
    request.flush(mockBook);
  });


  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
