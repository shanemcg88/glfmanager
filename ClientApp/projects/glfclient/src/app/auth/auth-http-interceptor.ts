import { Injectable } from "@angular/core";
import { CookieService } from "ngx-cookie-service";
import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpEventType,
    HttpHeaders
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { tap } from "rxjs/operators";

@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor{
    constructor(private cookieService: CookieService){}
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
        console.log('REQ:', req);
        return next.handle(modifiedReq).pipe(
            tap(value => {
                if (value.type === HttpEventType.Response){
                    console.log('response event type=', value);
                }
            })
        )
    }

}
