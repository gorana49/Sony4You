import { Component, Input, OnInit } from '@angular/core';
// import { Employer } from 'src/app/models/Employer';
// import { LoggedUser } from 'src/app/models/LoggedUser';
// import { AuthService } from 'src/app/services/auth.service';
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
  constructor(//private authService: AuthService
    ) { 
    this.isRenterer = true;
    this.isRentee = false;
  }

  ngOnInit(): void {
  }

  radioChange(event) {
    if(event.target.value === "employer")
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
    // let regkorisnik=new LoggedUser(email, password, role)
    // this.authService.postRegisterLoggedUser(regkorisnik)
    // .subscribe(value => {
    //   //console.log(`Uspešno registrovan user ${regkorisnik.email}!`)
    //   },
    //   err => {
    //   alert(`Dogodila se greška pri registrovanju rezisera, pokušajte ponovo.`)
    // })
  }

  checkInput(ime, prezime,email,password, sertifikat):boolean{
    if((ime === '' || ime == null || ime === undefined)  ||
        (prezime === '' || prezime == null || prezime === undefined) || 
        (password === '' || password == null || password === undefined ) || 
        (email === '' || email == null || email === undefined) ||
        (sertifikat === '' || sertifikat ==  null || sertifikat=== undefined || sertifikat===" "))
        return false;
    else return true;
  }

  btnRegistrujClicked(){
    const ime: HTMLInputElement = (document.getElementById('input-ime') as HTMLInputElement);
    const prezime: HTMLInputElement = (document.getElementById('input-prezime') as HTMLInputElement);
    const email: HTMLInputElement = (document.getElementById('input-email') as HTMLInputElement);
    const password: HTMLInputElement = (document.getElementById('input-password') as HTMLInputElement);
    
    if(this.isRenterer){
      const company: HTMLInputElement = (document.getElementById('input-company') as HTMLInputElement);
      const provera=this.checkInput(ime.value, prezime.value, email.value,password.value, company.value);
      if(!provera){
        this.errorMsg="Unesite sva input polja za registraciju!"
      }
      else{
        this.registerUser(email.value, password.value, "employer");
      
        // let emp=new Employer(ime.value,prezime.value, email.value,company.value);
        // this.authService.postRegisterEmployer(emp)
        // .subscribe(value => {
        //   alert(`Uspešno registrovan poslodavac ${emp.email}!`)
        //   },
        //   err => {
        //   alert(`Dogodila se greška pri registrovanju poslodavca, pokušajte ponovo.`)
        // })
        // ime.value='';
        // prezime.value='';
        // email.value='';
        // password.value='';
        // company.value='';
      }
    }
    else if(this.isRentee){
      const tip = this.selectedSelect;
      console.log(tip)
      const provera=this.checkInput(ime.value, prezime.value, email.value,password.value, tip);
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
  }
}
