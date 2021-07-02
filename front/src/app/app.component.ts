import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NavigationService } from './services/navigation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'JobApp';
  showNavButtons: boolean;
  constructor(private navigationService: NavigationService,
    private router: Router) {}

ngOnInit(){
  this.navigationService.flagCurrent.subscribe(flag => this.showNavButtons=flag);
}

logoutClicked(){
    this.navigationService.changeFlag(false);
    localStorage.removeItem("user");
    this.router.navigate([`./mainPage`]);
}

pocetnaClicked(){
  // this.user$.subscribe(
  // user=>{
  //   if(user.role==="employer"){
  //     this.router.navigate(['/employer/main'])
  //     }
  //     else{
  //       this.router.navigate(['/worker/main'])
  //     }
  // })
}

profilClicked(){
  // this.user$.subscribe(
  // user=>{
  //   if(user.role==="employer"){
  //     this.router.navigate(['/employer/profil'])
  //   }
  //   else{
  //     this.router.navigate(['/worker/profil'])
  //   }
  //   })
 }
}
