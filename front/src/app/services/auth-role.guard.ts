import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';



@Injectable({ providedIn: 'root' })
export class AuthRoleGuard implements CanActivate {
  logged:boolean=false;
  
  constructor(private router: Router) { }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):boolean  {

    // this.store.select(isLoggedIn).subscribe( 
    //   value => this.logged=value
    // )
    if(this.logged){
        return true;
    }
    this.router.navigate(['./mainPage']);
    return false;
  }
}