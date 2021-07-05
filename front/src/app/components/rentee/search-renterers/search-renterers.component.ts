import { Component, OnInit } from '@angular/core';
//import { select, Store } from '@ngrx/store';

import { Rentee } from 'src/app/models/Rentee';
import { Renterer } from 'src/app/models/Renterer';
import { RentererService } from 'src/app/services/renterer.service';
import { Sony } from 'src/app/models/Sony';
import { Game } from 'src/app/models/Game';
import { RenteeService } from 'src/app/services/rentee.service';
import { Comment } from "src/app/models/Comment";
import { CommentDTO } from 'src/app/models/CommentDTO';
import { RentererComments } from 'src/app/models/RentererComments';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-renterers',
  templateUrl: './search-renterers.component.html',
  styleUrls: ['./search-renterers.component.css']
})
export class SearchRenterersComponent implements OnInit {
  
  allRenterers: Renterer[]=[];
  allRenterersComments: RentererComments[]=[];
  rentererSonies: Sony[]=[];
  gamesForSony: Game[]=[];
  commentsForRenterer: Comment[]=[];
  sonyForReservation: Sony;
  rentererUsername: string;
  showModal: boolean=false;
  noGamesForSony: boolean=false;
  displayAddReservationModal: boolean=false;
  rentee: Rentee = {
    id: undefined,
    name: '',
    username: '',
    email: '',
    password:'',
    phoneNumber: '',
    profilePictureUrl: ''
  };

  constructor(private rentererService: RentererService,
              private renteeService: RenteeService,
              private _router: Router) { }
  
  ngOnInit(): void {
    this.showModal=false;
    this.displayAddReservationModal=false;
    this.rentee = JSON.parse(localStorage.getItem("user"));
    this.allRenterersComments=[];
    this.rentererService.getAllRenterers()
    .subscribe(value => {
      this.allRenterers=value;
      this.allRenterers.forEach(renterf=>{
        this.renteeService.getCommentsForRenterer(renterf.username)
        .subscribe(comments => {
          this.commentsForRenterer=comments;
          this.allRenterersComments.push(new RentererComments(renterf,this.commentsForRenterer));
        })
      })
    })
    console.log(this.allRenterersComments);
  }
    
  btnShowSoniesClicked(renterer: Renterer){
    this.rentererUsername=renterer.username;

    this.rentererService.getAllSonysForRenterer(renterer.username)
    .subscribe(value=>{
      this.rentererSonies=value;
      if(value.length === 0){
        this.rentererSonies=[];
        alert(`Nema slobodnih sonija.`)
      }
    },
    err=>{
      alert(`Dogodila se greška pri ucitavanju sonija.`)
    })
  }

  closeSignedUsers(){
    this.rentererSonies=[];
  }

  btnShowGamesClicked(r: Renterer, sony: Sony){
    this.showModal=true;
    this.rentererService.getGamesOnSony(sony.serialNumber)
    .subscribe(value=>{
      this.gamesForSony=value;
      if(value.length === 0){
        this.gamesForSony=[];
        this.noGamesForSony=true;
      }
    })

  }

  cancelModal(){
    this.showModal=false;
  }

  commentClicked(renterer: Renterer){
    const comment: HTMLInputElement = (document.getElementById('input-comment') as HTMLInputElement);
    var komentar= new Comment(new Date(), comment.value, this.rentee.username);
    var komentarDTO= new CommentDTO(renterer.username, this.rentee.username, komentar );
    
    if(comment.value!=""){
      this.renteeService.addCommentToRenterer(komentarDTO)
      .subscribe(value=>{
        window.location.reload()
      },
      err=>{
        alert(`Dogodila se greška pri dodavanju komentara.`)
      })
    }
  }

  deleteCommentClicked(comment :Comment){
    console.log(comment.date)
    this.renteeService.deleteComment(comment.date)
      .subscribe(value=>{
        window.location.reload()
      },
      err=>{
        alert(`Dogodila se greška pri brisanju komentara.`)
      })
  }

  showAddReservationModal(sony: Sony){
    this.sonyForReservation=sony;
    this.displayAddReservationModal=true;
  }
  
  hideAddReservationModal(){
    this.displayAddReservationModal=false;
  }
  
}
