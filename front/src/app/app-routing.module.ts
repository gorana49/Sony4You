import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './components/main-page/main-page.component';
import { MainPageRenteeComponent } from './components/rentee/main-page-rentee/main-page-rentee.component';
import { RenteePageComponent } from './components/rentee/rentee-page/rentee-page.component';
import { SearchRenterersComponent } from './components/rentee/search-renterers/search-renterers.component';
import { ChatPageComponent } from './components/renterer/chat-page/chat-page.component';
import { ForumPageComponent } from './components/renterer/forum-page/forum-page.component';
import { MainPageRentererComponent } from './components/renterer/main-page-renterer/main-page-renterer.component';
import { RentererPageComponent } from './components/renterer/renterer-page/renterer-page.component';
import { ReservationPageComponent } from './components/renterer/reservation-page/reservation-page.component';
import { SonyPageComponent } from './components/renterer/sony-page/sony-page.component';

const routes: Routes = [
  {path: '', redirectTo: '/mainPage', pathMatch: 'full'},
  {path: 'mainPage', component: MainPageComponent},
  {
    path: 'renterer',
    component: RentererPageComponent,
    //canActivate:[AuthRoleGuard],
    children: [
      {path: '', component: MainPageRentererComponent },
      {path: 'profil', component: MainPageComponent},
      {path: 'sony', component: SonyPageComponent},
      {path: 'reservation', component: ReservationPageComponent},
      {path: 'chat', component: ChatPageComponent},
      {path: 'forum', component: ForumPageComponent}
    ],
    //data: { role: 'renterer'}
  },
  {
    path: 'rentee',
    component: RenteePageComponent,
    children: [
      {path: '', component: MainPageRenteeComponent },
      {path: 'profil', component: MainPageRenteeComponent},
      {path: 'izdavaci', component: SearchRenterersComponent}
    ],
  //   canActivate:[AuthRoleGuard],
  //   data: { role: 'worker'}
  },
  {path: '**', redirectTo: 'mainPage', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
