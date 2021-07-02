import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule} from '@angular/forms'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { MainPageComponent } from './components/main-page/main-page.component';
import { RegisterComponent } from './components/register/register.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RentererPageComponent } from './components/renterer/renterer-page/renterer-page.component';
import { SonyPageComponent } from './components/renterer/sony-page/sony-page.component';
import { ReservationPageComponent } from './components/renterer/reservation-page/reservation-page.component';
import { ChatPageComponent } from './components/renterer/chat-page/chat-page.component';
import { ForumPageComponent } from './components/renterer/forum-page/forum-page.component';
import { RenteePageComponent } from './components/rentee/rentee-page/rentee-page.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    MainPageComponent,
    RentererPageComponent,
    SonyPageComponent,
    ReservationPageComponent,
    ChatPageComponent,
    ForumPageComponent,
    RenteePageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule, 
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
