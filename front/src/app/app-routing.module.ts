import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './components/main-page/main-page.component';
import { ChatPageComponent } from './components/renterer/chat-page/chat-page.component';
import { ForumPageComponent } from './components/renterer/forum-page/forum-page.component';
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
      {path: '', component: SonyPageComponent },
      {path: 'sony', component: SonyPageComponent},
      {path: 'reservation', component: ReservationPageComponent},
      {path: 'caht', component: ChatPageComponent},
      {path: 'forum', component: ForumPageComponent}
    ],
    //data: { role: 'renterer'}
  },
  // {
  //   path: 'worker',
  //   component: WorkerComponent,
  //   children: [
  //     {path: '', component: WorkerProfileComponent },
  //     {path: 'profil', component: WorkerProfileComponent},
  //     {path: 'main', component: SearchJobsComponent}
  //   ],
  //   canActivate:[AuthRoleGuard],
  //   data: { role: 'worker'}
  // },
  {path: '**', redirectTo: 'mainPage', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
