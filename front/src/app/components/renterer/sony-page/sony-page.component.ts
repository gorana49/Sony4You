import { Component, OnInit } from '@angular/core';
import { Renterer } from 'src/app/models/Renterer';
import { Sony } from 'src/app/models/Sony';
import { Game } from 'src/app/models/Game';
import { RentererService } from 'src/app/services/renterer.service';

@Component({
  selector: 'app-sony-page',
  templateUrl: './sony-page.component.html',
  styleUrls: ['./sony-page.component.css']
})
export class SonyPageComponent implements OnInit {
  allSonys:Sony[];
  allSonyGames:Game[];
  sonySerial: string;
  displayAddGameModal:boolean;
  sonyAddGame:Sony;
  constructor(private rentererService:RentererService) 
  { 
    this.allSonys = [];
    this.allSonyGames = [];
    this.sonySerial = undefined;
    this.displayAddGameModal = false;
  }

  ngOnInit(): void {
    var user:Renterer = JSON.parse(localStorage.getItem("user"));
    if(user!= null){
      this.rentererService.getAllSonysForRenterer(user.username)
      .subscribe(value=>{
        if(value!=undefined){
          value.map(el => this.allSonys.push(el))
        }
      })
    }
  }

  createSony() {
    // var user:Renterer = JSON.parse(localStorage.getItem("user"));
    // const serialNumber: HTMLInputElement = (document.getElementById('sony-serial-number') as HTMLInputElement);
    // const type: HTMLInputElement = (document.getElementById('sony-type') as HTMLInputElement);
    // const notes: HTMLInputElement = (document.getElementById('sony-notes') as HTMLInputElement);
    // const price: HTMLInputElement = (document.getElementById('sony-price') as HTMLInputElement);

    // var sony = new SonyCreate(user.username,notes.value,serialNumber.value,type.value,price.value);

    // this.rentererService.createSony(sony).subscribe(
    //   (value) => {
    //     console.log(value)
    //   }
    // );
  }

  btnShowAddGame(sony:Sony) {
    this.displayAddGameModal = true;
    this.sonyAddGame = sony;
  }

  btnShowGamesClicked(sony:Sony) {
    this.sonySerial=sony.serialNumber;

    this.rentererService.getGamesOnSony(sony.serialNumber)
    .subscribe(value=>{
      this.allSonyGames=value;
      if(value.length === 0){
        alert(`Ovaj sony nema igrica.`)
      }
    },
    err=>{
      alert(`Dogodila se greška pri ucitavanju igrica.`)
    })
  }

  closeSignedUsers(){
    this.allSonyGames=[];
    this.sonySerial = "";
  }

  hideAddGameModal() {
    this.displayAddGameModal = false;
    this.sonyAddGame = null;
  }
}

// allRenterers: Renterer[]=[];
//   rentererSonies: Sony[]=[];
//   rentererUsername: string;
//   rentee: Rentee = {
//     id: undefined,
//     name: '',
//     username: '',
//     email: '',
//     password:'',
//     phoneNumber: '',
//     profilePictureUrl: ''
//   };

//   constructor(/*private store: Store<AppState>,*/
//               private rentererServicve: RentererService) { }
  
//   ngOnInit(): void {
//     this.rentee = JSON.parse(localStorage.getItem("user"));
//     this.rentererServicve.getAllRenterers()
//     .subscribe(value => {
//       this.allRenterers=value;
//       },
//       err => {
//       alert(`Dogodila se greška pri ucitavanju svih izdavaca.`)
//     })
//   }
    


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
