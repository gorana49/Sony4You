import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { LoggedUser } from '../models/LoggedUser';
import { AuthService } from './auth.service';



@Injectable({ providedIn: 'root' })
export class AuthRoleGuard implements CanActivate {
  logged:boolean;
  constructor(private router: Router,
              private authService:AuthService) 
  { 
    this.logged = false;
  }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):boolean  {

    var user:LoggedUser = JSON.parse(localStorage.getItem("logged-user"));
    if(user.loggedIn){
        return true;
    }
    this.router.navigate(['./mainPage']);
    return false;
  }
}