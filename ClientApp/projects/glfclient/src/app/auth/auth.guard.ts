import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { skipWhile, take, tap, map } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanLoad {

  constructor(
    private authService: AuthService,
    private router: Router
  ){}
  
  canLoad(route: Route, segments: UrlSegment[]): 
    Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      
      return this.authService.signedIn$.pipe(
        skipWhile(value => value === null),
        take(1),
        map(authenticated => {
          if (authenticated) {
            return true;
          } else {
            this.router.navigateByUrl("signin");
            return false;
          }
        })
      );
  }
}
