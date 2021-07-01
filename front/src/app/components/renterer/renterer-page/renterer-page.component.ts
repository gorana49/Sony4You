import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Renterer } from 'src/app/models/Renterer';
import { RentererService } from 'src/app/services/renterer.service';

@Component({
  selector: 'app-renterer-page',
  templateUrl: './renterer-page.component.html',
  styleUrls: ['./renterer-page.component.css']
})
export class RentererPageComponent implements OnInit {
  renterer: Renterer;
  constructor(private router:Router,
              private rentererService:RentererService) { 
                this.renterer = new Renterer("","","","","","","","");
              }

  ngOnInit(): void {
    var user:Renterer = JSON.parse(localStorage.getItem("user"));
    if(user!= null){
      this.rentererService.getEmployerByUsername(user.username)
      .subscribe(value=>{
        if(value!=undefined){
          this.renterer = value;
          console.log(value)
        }
        else{
          this.renterer = new Renterer("","","","","","","","");
        }
      })
    }
    else{
      this.renterer = new Renterer("","","","","","","","");
    }
  }

  navigateTo(path) {
    this.router.navigate([`./renterer/${path}`]);
  }
}
