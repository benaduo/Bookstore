import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, tap } from "rxjs";

@Injectable()

export class AuthInterceptor implements HttpInterceptor {
  constructor(private router: Router) { }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (sessionStorage.hasOwnProperty('token')) {
      const clonedReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('token'))
      });
      return next.handle(clonedReq)
        .pipe(
          tap(
            () => { },
            (err) => {
              if (err.status === 401) {
                sessionStorage.removeItem('token');
                this.router.navigate(['/user/login']);
              }
            }
          )
        );
    } else {
      return next.handle(req.clone());
    }
  }

}
