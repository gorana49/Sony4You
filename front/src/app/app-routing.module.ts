import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent } from './components/main-page/main-page.component';

const routes: Routes = [
  {path: '', redirectTo: '/mainPage', pathMatch: 'full'},
  {path: 'mainPage', component: MainPageComponent},
  // {
  //   path: 'renterer',
  //   component: EmployerComponent,
  //   canActivate:[AuthRoleGuard],
  //   children: [
  //     {path: '', component: EmployerProfileComponent },
  //     {path: 'profil', component: EmployerProfileComponent},
  //     {path: 'main', component: SearchWorkersComponent}
  //   ],
  //   data: { role: 'renterer'}
  // },
  // {
  //   path: 'rentee',
  //   component: WorkerComponent,
  //   children: [
  //     {path: '', component: WorkerProfileComponent },
  //     {path: 'profil', component: WorkerProfileComponent},
  //     {path: 'main', component: SearchJobsComponent}
  //   ],
  //   canActivate:[AuthRoleGuard],
  //   data: { role: 'rentee'}
  // },
  {path: '**', redirectTo: 'mainPage', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
