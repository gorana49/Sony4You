import { Component, Input, OnInit } from '@angular/core';
// import { Employer } from 'src/app/models/Employer';
import { LoggedUser } from 'src/app/models/LoggedUser';
import { Rentee } from 'src/app/models/Rentee';
import { AuthService } from 'src/app/services/auth.service';
// import { Worker } from '../../models/Worker';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  isRenterer: boolean;
  isRentee: boolean;
  errorMsg: string;
  selectedSelect: string;
  constructor(private authService: AuthService)
   { 
    this.isRenterer = true;
    this.isRentee = false;
  }

  ngOnInit(): void {
  }

  radioChange(event) {
    if(event.target.value === "renterer")
    {
        this.isRenterer = true;
        this.isRentee = false;
    }
    else
    {
      this.isRenterer = false;
      this.isRentee = true;
    }
  }

  registerUser(email:string, password:string, role:string){
    let loggedUser=new LoggedUser(email, password,false, role)
    this.authService.addNewLoggedUser(loggedUser)
    .subscribe(value => {
      //console.log(`Uspešno registrovan user ${regkorisnik.email}!`)
      },
      err => {
      //alert(`Dogodila se greška pri registrovanju rezisera, pokušajte ponovo.`)
    })
  }

  checkInput(name, username, email, password, phoneNumber):boolean{
    if((name === '' || name == null || name === undefined)  ||
        (username === '' || username == null || username === undefined) || 
        (password === '' || password == null || password === undefined ) || 
        (email === '' || email == null || email === undefined) ||
        (phoneNumber === '' || phoneNumber ==  null || phoneNumber=== undefined || phoneNumber===" "))
        return false;
    else return true;
  }

  btnRegistrujClicked(){
    const name: HTMLInputElement = (document.getElementById('input-name') as HTMLInputElement);
    const username: HTMLInputElement = (document.getElementById('input-username') as HTMLInputElement);
    const email: HTMLInputElement = (document.getElementById('input-email') as HTMLInputElement);
    const password: HTMLInputElement = (document.getElementById('input-password') as HTMLInputElement);
    const phoneNumber: HTMLInputElement = (document.getElementById('input-phone') as HTMLInputElement);

    const provera=this.checkInput(name.value, username.value, email.value,password.value, phoneNumber.value);
    
    if(this.isRentee){
      if(!provera){
        this.errorMsg="Unesite sva input polja za registraciju!"
        console.log(this.errorMsg);
      }
      else{
        let rentee= new Rentee(name.value, username.value, email.value, password.value, phoneNumber.value, "");
        this.registerUser(email.value, password.value, "rentee");
      
        this.authService.addNewRentee(rentee)
        .subscribe(value => {
          alert(`Uspešno registrovan poslodavac ${rentee.email}!`)
          },
          err => {
          alert(`Dogodila se greška pri registrovanju korinika, pokušajte ponovo.`)
        })
        name.value='';
        username.value='';
        email.value='';
        password.value='';
        phoneNumber.value='';
      }
    }
    else if(this.isRenterer){
      const tip = this.selectedSelect.valueOf;
      console.log(tip)
      //const provera=this.checkInput(ime.value, prezime.value, email.value,password.value, tip);
      if(!provera){
        this.errorMsg="Unesite sva input polja za registraciju!"
      }
      else{
        this.registerUser(email.value, password.value, "worker");
        // let work = new Worker(ime.value, prezime.value, email.value, tip, 0);
      //   this.authService.postRegisterWorker(work)
      //   .subscribe(value => {
      //     alert(`Uspešno registrovan ${tip} ${work.email}!`)
      //     },
      //     err => {
      //     alert(`Dogodila se greška pri registrovanju ${tip}, pokušajte ponovo.`)
      //   })
      //   ime.value='';
      //   prezime.value='';
      //   email.value='';
      //   password.value='';
      }
    }
  }

  selectChangedEvent(event) {
    this.selectedSelect = event.target.value;
    console.log(this.selectedSelect);
  }
}
