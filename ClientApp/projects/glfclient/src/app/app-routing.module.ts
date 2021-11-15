import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './main/home/home.component';

import { AuthGuard } from './auth/auth.guard';
import { SignInPageGuard } from './auth/sign-in-page.guard';

const routes: Routes = [
  {
    path: 'main',
    canLoad: [AuthGuard], // not sure if using a guard is necessary as the interceptor takes care of this
    loadChildren: () => import('./main/main.module').then(mod => mod.MainModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
