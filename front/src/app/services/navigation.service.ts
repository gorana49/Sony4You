import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  private flagSource= new BehaviorSubject<string>("");
  flagCurrent= this.flagSource.asObservable();

  constructor() { }

  changeFlag(value: string){
    this.flagSource.next(value);
  }
}
