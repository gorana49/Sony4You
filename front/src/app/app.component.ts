import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoggedUser } from './models/LoggedUser';
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
  const loggedUserData: LoggedUser= JSON.parse(localStorage.getItem("user"));
  if(loggedUserData.role==="rentee"){
    this.router.navigate(['./rentee'])
    }
 
}

izdavaciClicked(){
  const loggedUserData: LoggedUser= JSON.parse(localStorage.getItem("user"));
  console.log(loggedUserData);
  if(loggedUserData.role==="rentee"){
    this.router.navigate(['./rentee/izdavaci'])
    }
  }
}
