import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MainPageComponent } from './components/main-page/main-page.component';
import { AuthRoleGuard } from './services/auth-role.guard';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    MainPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers:[
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
    AuthRoleGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }