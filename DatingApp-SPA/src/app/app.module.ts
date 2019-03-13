import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { ListResolve } from './_resolves/list.resolve';
import { MessagesResolve } from './_resolves/messages_resolves';
import { NgModule } from '@angular/core';
import { PhotoEditComponent } from './members/photo-edit/photo-edit.component';
import { PreventUnsavdChange } from './_graud/prevent-unsavd-change.guard';
import {  RouterModule } from '@angular/router';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { AlertifyjsService } from './_Services/alertifyjs.service';
import { ErrorInterceptorProvider } from './_Services/error.Interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { BsDropdownModule, TabsModule, BsDatepickerModule,
   PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { app_route } from './_Services/Router';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { NavComponent } from './Nav/Nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './_Services/auth.service';
import { HomeComponent } from './Home/Home.component';
import { RegisterComponent } from './register/register.component';
import {TimeAgoPipe} from 'time-ago-pipe';
import { ListComponent } from './list/list.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessageComponent } from './message/message.component';
import { AuthGuard } from './_graud/auth.guard';
import { UserService } from './_Services/user.service';
import { JwtModule } from '@auth0/angular-jwt';
import { MemberDetailResolve } from './_resolves/member-detail.resolve';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemeberListResolve } from './_resolves/member-list';
import { MemberEditResolve } from './_resolves/member-edit';

export function token() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    TimeAgoPipe,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    ListComponent,
    MemberListComponent,
    MessageComponent,
    MemberCardComponent,
    MemberDetailComponent,
    MemberEditComponent,
    PhotoEditComponent ,
  MemberMessagesComponent ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
     BsDatepickerModule.forRoot(),
     PaginationModule.forRoot(),
     ButtonsModule.forRoot(),
    TabsModule.forRoot(),
    RouterModule.forRoot(app_route),
    NgxGalleryModule,
    FileUploadModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: token,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']

      }
    })

  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    AlertifyjsService,
    AuthGuard,
    UserService,
    MemberDetailResolve,
    MemberEditResolve,
    MemeberListResolve,
    PreventUnsavdChange,
    ListResolve,
    MessagesResolve ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
