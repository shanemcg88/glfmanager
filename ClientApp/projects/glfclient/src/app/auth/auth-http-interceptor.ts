import { Injectable } from "@angular/core";
import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpEventType
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { tap } from "rxjs/operators";

@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor{
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const modifiedReq = req.clone({
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
