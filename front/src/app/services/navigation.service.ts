import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  private flagSource= new BehaviorSubject<boolean>(false);
  flagCurrent= this.flagSource.asObservable();

  constructor() { }

  changeFlag(value: boolean){
    this.flagSource.next(value);
  }
}
