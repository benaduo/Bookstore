import { HttpClientModule } from '@angular/common/http';

import { TestBed } from '@angular/core/testing';

import { CategoryService } from './category.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Category } from '../models/category';

describe('CategoryService', () => {
  let service: CategoryService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientModule, HttpClientTestingModule]
    });
    service = TestBed.inject(CategoryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should return categories from the server', () => {
    const mockCategories: Category[] = [
      { id: 1, name: 'Sci-Fi', description: 'Books on Science Fiction'},
      { id: 2, name: 'Regligion',  description: 'Books on Religion'}
    ];

    service.getCategories().subscribe({
      next: categories => {
        expect(categories).toBeTruthy();
        expect(categories).toEqual(mockCategories);
      }
    });

    const request = httpMock.expectOne(service.baseUrl + '/category');
    expect(request.request.method).toBe('GET');

    request.flush(mockCategories);

  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
