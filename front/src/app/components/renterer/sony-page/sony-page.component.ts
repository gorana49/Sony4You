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
  displayUpdateGameModal:boolean;
  displayUpdateSony:boolean;
  sonyAddGame:Sony;
  updateGame:Game;
  newSony:Sony;
  updateSony:Sony;
  constructor(private rentererService:RentererService) 
  { 
    this.allSonys = [];
    this.allSonyGames = [];
    this.sonySerial = undefined;
    this.displayAddGameModal = false;
    this.displayUpdateGameModal = false;
    this.newSony = new Sony("","","",0)
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
    var user:Renterer = JSON.parse(localStorage.getItem("user"));
    console.log(this.newSony)
    if(user!= null){
      this.rentererService.createSony(user.username,this.newSony)
      .subscribe(value=>{
      })}
    location.reload();
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

  hideUpdateGameModal() {
    this.displayUpdateGameModal = false;
    this.updateGame = null;
  }

  btnDeleteGame(game:Game) {
    this.rentererService.deleteGame(game)
    .subscribe(value=>{
      this.allSonyGames=[];
    })

    this.rentererService.getGamesOnSony(this.sonySerial)
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

  btnUpdateGame(game:Game) {
    this.displayUpdateGameModal = true;
    this.updateGame = game;
  }

  btnIzmeniSony(sony:Sony) {
    this.displayUpdateSony = true;
    this.updateSony = sony;
  }

  hideUpdateSonyModal() {
    this.displayUpdateSony = false;
    this.updateSony = null;
  }

  btnObrisiSony(sony:Sony) {
    this.rentererService.deleteSony(sony)
    .subscribe(value=>{

    })
    location.reload();
  }
}
