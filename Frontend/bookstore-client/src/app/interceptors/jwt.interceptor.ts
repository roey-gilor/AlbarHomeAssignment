import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service'

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  // Injecting custom service to get token
  const authService = inject(AuthService);
  const token = authService.getToken();

  // If token exists, duplicate origin request and adding Bearer to header
  if (token) {
    const clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });

    // sending on the duplicated request with token
    return next(clonedRequest);
  }

  // If token doesnt exists, sending the request as is 
  return next(req);
};