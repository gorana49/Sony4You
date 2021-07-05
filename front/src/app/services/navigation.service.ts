import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LoggedUser } from '../models/LoggedUser';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  private flagSource= new BehaviorSubject<string>("");
  flagCurrent= this.flagSource.asObservable();

  constructor() { 
    var user= JSON.parse(localStorage.getItem("user"));
    if(user!=undefined)
    this.changeFlag(user.role);
  }

  changeFlag(value: string){
    this.flagSource.next(value);
  }
}
