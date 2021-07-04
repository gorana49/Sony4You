import { Component, OnInit } from '@angular/core';
import { Renterer } from 'src/app/models/Renterer';
import { Sony } from 'src/app/models/Sony';
import { SonyCreate } from 'src/app/models/SonyCreate';
import { RentererService } from 'src/app/services/renterer.service';

@Component({
  selector: 'app-sony-page',
  templateUrl: './sony-page.component.html',
  styleUrls: ['./sony-page.component.css']
})
export class SonyPageComponent implements OnInit {
  allSonys:Sony[];
  constructor(private rentererService:RentererService) 
  { 
    this.allSonys = [];
  }

  ngOnInit(): void {
    var user:Renterer = JSON.parse(localStorage.getItem("user"));
    if(user!= null){
      this.rentererService.getAllSonys(user.username)
      .subscribe(value=>{
        if(value!=undefined){
          value.map(el => this.allSonys.push(el))
          console.log(this.allSonys)
        }
      })
    }
  }

  createSony() {
    var user:Renterer = JSON.parse(localStorage.getItem("user"));
    const serialNumber: HTMLInputElement = (document.getElementById('sony-serial-number') as HTMLInputElement);
    const type: HTMLInputElement = (document.getElementById('sony-type') as HTMLInputElement);
    const notes: HTMLInputElement = (document.getElementById('sony-notes') as HTMLInputElement);
    const price: HTMLInputElement = (document.getElementById('sony-price') as HTMLInputElement);

    var sony = new SonyCreate(user.username,notes.value,serialNumber.value,type.value,price.value);

    this.rentererService.createSony(sony).subscribe(
      (value) => {
        console.log(value)
      }
    );
  }

}
