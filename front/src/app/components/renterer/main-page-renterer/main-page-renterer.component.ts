import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Renterer } from 'src/app/models/Renterer';
import { RentererService } from 'src/app/services/renterer.service';

@Component({
  selector: 'app-main-page-renterer',
  templateUrl: './main-page-renterer.component.html',
  styleUrls: ['./main-page-renterer.component.css']
})
export class MainPageRentererComponent implements OnInit {
  renterer: Renterer;
  constructor(
              private router:Router,
              private rentererService:RentererService) { 
                this.renterer = new Renterer("","","","","","","","");
              }

  ngOnInit(): void {
    var user:Renterer = JSON.parse(localStorage.getItem("user"));
    if(user!= null){
      this.rentererService.getRentererByUsername(user.username)
      .subscribe(value=>{
        if(value!=undefined){
          this.renterer = value;
          console.log(value)
        }
      })
    }
  }

  navigateTo(path) {
    this.router.navigate([`./renterer/${path}`]);
  }
}
