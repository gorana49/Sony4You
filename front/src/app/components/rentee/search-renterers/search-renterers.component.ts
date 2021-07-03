import { Component, OnInit } from '@angular/core';
//import { select, Store } from '@ngrx/store';
import { filter } from 'rxjs/operators';
import { Rentee } from 'src/app/models/Rentee';
import { Renterer } from 'src/app/models/Renterer';
import { RentererService } from 'src/app/services/renterer.service';
import { CommonModule } from '@angular/common';
import { Sony } from 'src/app/models/Sony';
// import { Event } from 'src/app/models/Event';
// import { User } from 'src/app/models/User';
// import { AppState } from 'src/app/store';
// import { selectAllEvents } from 'src/app/store/selectors/event.selectors';
// import { selectUserInfo } from '../../store/selectors/user-info.selectors'
// import { EventSignedEmplyed } from '../../models/EventSignedEmployed'
// import { UserService } from 'src/app/services/user.service';
// import { AddEventSignedUp } from 'src/app/store/actions/events-signed-up.actions';
// import { UpdateUserInfoAction } from 'src/app/store/actions/user-info.actions';
// import { selectAllEventsSigned } from '../../store/selectors/events-signed-up.selectors'

@Component({
  selector: 'app-search-renterers',
  templateUrl: './search-renterers.component.html',
  styleUrls: ['./search-renterers.component.css']
})
export class SearchRenterersComponent implements OnInit {
  // notSignedEvents: Event[]=[];
  // idsSignedEvents: string[]=[];
  
  allRenterers: Renterer[]=[];
  rentererSonies: Sony[]=[];
  rentee: Rentee = {
    id: undefined,
    name: '',
    username: '',
    email: '',
    password:'',
    phoneNumber: '',
    profilePictureUrl: ''
  };

  constructor(/*private store: Store<AppState>,*/
              private rentererServicve: RentererService) { }
  
  ngOnInit(): void {

    this.rentee = JSON.parse(localStorage.getItem("user"));
    this.rentererServicve.getAllRenterers()
    .subscribe(value => {
      this.allRenterers=value;
      },
      err => {
      alert(`Dogodila se greška pri ucitavanju svih izdavaca.`)
    })
  }
             
  // _inputFilter: string;
  // get inputFilter(){
  //   return this._inputFilter;
  // }
  // set inputFilter(value:string){
  //   this._inputFilter=value;
  //   this.filteredEvents= this.inputFilter ? this.filter(this.inputFilter) : this.allEvents;
  // }

  // userInfo$=this.store.pipe(
  //   select(selectUserInfo),
  //   filter(val => val !== undefined)
  // );

  // events$=this.store.pipe(
  //   select(selectAllEvents),
  //   filter(val => val !== undefined)
  // )

  // eventsSignedUp$=this.store.pipe(
  //   select(selectAllEventsSigned),
  //   filter(val => val !== undefined)
  // );


  // ngOnInit(): void {

  //   this.events$.subscribe(
  //     (events) => events.forEach(u => { this.allEvents.push(u); }))
  //   this.filteredEvents=this.allEvents;
  //   this.userInfo$.subscribe((user: User) => this.user={...user} );

  //   this.eventsSignedUp$.subscribe((events) =>{
  //     events.forEach(eventSigned => this.idsSignedEvents.push(eventSigned.eventId))
    
   
  //   if(this.idsSignedEvents.length!=0){
  //     this.allEvents.forEach((event, indexOf )=>{
  //       this.idsSignedEvents.forEach(idEvent =>{
  //         if(event.id===idEvent)
  //           this.allEvents.splice(indexOf,1);
  //       })
  //     })
  //   }

  //   })
  // }

  // filter(filterBy: string): Event[]{
  //   filterBy=filterBy.toLocaleLowerCase();
  //   return this.allEvents.filter( (korisnik: Event)=>
  //       korisnik.userType.toLocaleLowerCase().indexOf(filterBy)!==-1);
  // }

  // signToEvent(event: Event){
  //   this.idsSignedEvents=[]; 

  //   if(event.userType===this.user.type){
  //     let eventSigned= new EventSignedEmplyed(event.id, this.user.id);
  //     this.store.dispatch(new AddEventSignedUp(eventSigned));
  //     if(this.user.status!="u procesu"){
  //       this.user.status="u procesu"
  //       this.store.dispatch(new UpdateUserInfoAction(this.user));
  //     }
  //   }
  //   else alert('Ne možete se prijaviti na dati događaj jer ovaj događaj zahteva drugu vrstu korisnika. Žao nam je!')
  // }

}
