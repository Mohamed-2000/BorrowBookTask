import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [NavBarComponent, TestErrorComponent, ServerErrorComponent, NotFoundComponent],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
    })
  ],
  // export NavBarComponent so we can use it in another module
  exports: [NavBarComponent],
})
export class CoreModule { }
