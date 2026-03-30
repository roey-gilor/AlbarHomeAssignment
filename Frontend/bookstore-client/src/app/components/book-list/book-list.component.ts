import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BookService } from '../../services/book.service';
import { BookDTO } from '../../model/book.model';
import { AuthService } from '../../services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent implements OnInit {
  // Signal to manage visual components state
  books = signal<BookDTO[]>([]);
  isLoading = signal<boolean>(false);
  errorMessage = signal<string>('');

  // Variable to track the id of book we want to update
  editingBookId = signal<number | null>(null);

  // variables for search
  searchNameTerm: string = '';
  searchCategoryTerm: string = '';

  private authService = inject(AuthService);
  private bookService = inject(BookService);
  private router = inject(Router);

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks() {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.bookService.getAllBooks().subscribe({
      next: (data) => {
        this.books.set(data);
        this.isLoading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.errorMessage.set(err.error.message);
      }
    });
  }

  // Navigate to new book page
  addNewBook() {
    this.router.navigate(['/books/new']);
  }

  // Save the ID of the selected book
  startEdit(bookId: number | undefined) {
    if (bookId) {
      this.editingBookId.set(bookId);
    }
  }

  // Reset the choice and Reloads the books to cancel unsaved changes 
  // (ngModel changes the object on real time)
  cancelEdit() {
    this.editingBookId.set(null);
    this.loadBooks();
  }

  saveEdit(book: BookDTO) {
    this.isLoading.set(true);
    this.bookService.updateBook(book.bookId!, book).subscribe({
      next: () => {
        this.editingBookId.set(null);
        this.loadBooks();
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.errorMessage.set(err.error.message);
      }
    });
  }

  searchByName() {
    if (!this.searchNameTerm.trim()) return;

    this.isLoading.set(true);
    this.errorMessage.set('');
    this.searchCategoryTerm = ''; // Reset category search

    this.bookService.getBookByName(this.searchNameTerm).subscribe({
      next: (data) => {
        this.books.set(data ? [data] : []);
        this.isLoading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.errorMessage.set(err.error.message);
      }
    });
  }

  searchByCategory() {
    if (!this.searchCategoryTerm.trim()) return;

    this.isLoading.set(true);
    this.errorMessage.set('');
    this.searchNameTerm = ''; // Reset name search

    this.bookService.searchByCategory(this.searchCategoryTerm).subscribe({
      next: (data) => {
        this.books.set(data || []);
        this.isLoading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.errorMessage.set(err.error.message);
      }
    });
  }

  clearSearch() {
    // Reset all searches
    this.searchNameTerm = '';
    this.searchCategoryTerm = '';
    this.loadBooks();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}