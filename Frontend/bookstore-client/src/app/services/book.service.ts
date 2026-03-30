import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BookDTO } from '../model/book.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7112/api/books';

  // 1. Retrieving all books
  getAllBooks(): Observable<BookDTO[]> {
    return this.http.get<BookDTO[]>(this.apiUrl);
  }

  // 2. Getting book by name
  getBookByName(name: string): Observable<BookDTO> {
    return this.http.get<BookDTO>(`${this.apiUrl}/search/name/${name}`);
  }

  // 3. Getting book by category
  searchByCategory(category: string): Observable<BookDTO[]> {
    return this.http.get<BookDTO[]>(`${this.apiUrl}/search/category/${category}`);
  }

  // 4. Create new book
  createBook(book: BookDTO): Observable<BookDTO> {
    return this.http.post<BookDTO>(`${this.apiUrl}/Create`, book);
  }

  // 5. Update existing book
  updateBook(id: number, book: BookDTO): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, book);
  }
}