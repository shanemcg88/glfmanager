import { Injectable } from "@angular/core";
import { CookieService } from "ngx-cookie-service";
import { Router } from "@angular/router";
import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpEventType,
    HttpHeaders
} from '@angular/common/http';

import { Observable, of, throwError } from 'rxjs';
import { tap, catchError } from "rxjs/operators";

@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor{
    constructor(
        private cookieService: CookieService,
        private router: Router
    ){}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        var bearer = 'Bearer ' + this.cookieService.get('Bearer');
        const httpOptions = new HttpHeaders({
          'Content-Type':  'application/json',
          Authorization: bearer,
        })
        const modifiedReq = req.clone({
            headers: httpOptions,
            withCredentials: true
        });
        //return next.handle(modifiedReq)
        return next.handle(modifiedReq).pipe(
            // tap(value => {
            //     console.log('auth-http-interceptor value.type', value.type);
            //     if (value.type === HttpEventType.Response){
            //         console.log('response event type=', value);
            //     }
            // }),
            // catchError(err => {
            //     console.log('catcherror ran', err);
            //     if (err.status === 401) {
            //         this.router.navigateByUrl("signin");
            //         return throwError("Unauthorized")
            //     }
            //     return throwError(err);
            //   })
        )
    }

}
