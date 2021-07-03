import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Rentee } from 'src/app/models/Rentee';
import { RenteeService } from 'src/app/services/rentee.service';

@Component({
  templateUrl: './main-page-rentee.component.html',
  styleUrls: ['./main-page-rentee.component.css']
})
export class MainPageRenteeComponent implements OnInit {
  rentee: Rentee;
  constructor(
              private router:Router,
              private renteeService:RenteeService) { 
                this.rentee = new Rentee("","","","","","");
              }

  ngOnInit(): void {
    var user:Rentee = JSON.parse(localStorage.getItem("user"));
    if(user!= null){
      this.renteeService.getRenteeByUsername(user.username)
      .subscribe(value=>{
        if(value!=undefined){
          this.rentee = value;
          console.log(value)
        }
        else{
          this.rentee = new Rentee("","","","","","");
        }
      })
    }
    else{
      this.rentee = new Rentee("","","","","","");
    }
  }

  navigateTo(path) {
    this.router.navigate([`./renterer/${path}`]);
  }
}
