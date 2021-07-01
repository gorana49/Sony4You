import { Component, Input, OnInit } from '@angular/core';;
import { LoggedUser } from 'src/app/models/LoggedUser';
import { Rentee } from 'src/app/models/Rentee';
import { Renterer } from 'src/app/models/Renterer';
import { AuthService } from 'src/app/services/auth.service';

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
    this.selectedSelect = event.target.value;
    if(this.selectedSelect === "renterer")
    {
        this.isRenterer = true;
        this.isRentee = false;
    }
    else
    {
      this.isRenterer = false;
      this.isRentee = true;
    }
    console.log(this.selectedSelect);
  }

  registerUser(email:string, password:string, role:string){
    let loggedUser=new LoggedUser(email, password,false, role)
    this.authService.addNewLoggedUser(loggedUser)
    .subscribe(value => {},
      err => {})
  }

  btnRegistrujClicked(){
    const name: HTMLInputElement = (document.getElementById('input-name') as HTMLInputElement);
    const username: HTMLInputElement = (document.getElementById('input-username') as HTMLInputElement);
    const email: HTMLInputElement = (document.getElementById('input-email') as HTMLInputElement);
    const password: HTMLInputElement = (document.getElementById('input-password') as HTMLInputElement);
    const phoneNumber: HTMLInputElement = (document.getElementById('input-phone') as HTMLInputElement);
  
    if(this.isRentee){
      const provera=this.checkInput(name.value, username.value, email.value,password.value, phoneNumber.value);
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
        this.errorMsg='';
      }
    }
    else if(this.isRenterer){
      const address: HTMLInputElement = (document.getElementById('input-address') as HTMLInputElement);
      const company: HTMLInputElement = (document.getElementById('input-company') as HTMLInputElement);
      //potrebna provera za adresu i kompaniju!
      const provera=this.checkInput(name.value, username.value, email.value,password.value, phoneNumber.value);
      if(!provera){
        this.errorMsg="Unesite sva input polja za registraciju!";
        console.log(this.errorMsg);
      }
      else{
        this.registerUser(email.value, password.value, "renter");
        let renterer= new Renterer(name.value, username.value, email.value, password.value, phoneNumber.value, address.value, company.value, "")
        this.authService.addNewRenterer(renterer)
        .subscribe(value => {
          alert(`Uspešno registrovan izdavač ${renterer.username}!`)
          },
          err => {
          alert(`Dogodila se greška pri registrovanju izdavača, pokušajte ponovo.`)
        })
        name.value='';
        username.value='';
        email.value='';
        password.value='';
        phoneNumber.value='';
        address.value='';
        company.value='';
        this.errorMsg='';
      }
    }
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
}
