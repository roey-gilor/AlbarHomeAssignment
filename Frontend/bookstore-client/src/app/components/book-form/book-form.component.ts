import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BookService } from '../../services/book.service';
import { BookDTO } from '../../model/book.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent {
  book: BookDTO = {
    bookName: '',
    category: '',
    price: 0,
    unitsInStock: 0
  };

  isLoading = signal<boolean>(false);
  errorMessage = signal<string>('');

  private bookService = inject(BookService);
  private router = inject(Router);

  onSubmit() {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.bookService.createBook(this.book).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.router.navigate(['/books']);
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.errorMessage.set(err.error);
      }
    });
  }

  cancel() {
    this.router.navigate(['/books']);
  }
}