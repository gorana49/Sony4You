import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Rentee } from 'src/app/models/Rentee';
import { RenteeService } from 'src/app/services/rentee.service';
import {NotificationService} from 'src/app/services/notification.service'
import { NotificationDTO } from 'src/app/models/NotificationDTO';
@Component({
  templateUrl: './main-page-rentee.component.html',
  styleUrls: ['./main-page-rentee.component.css']
})
export class MainPageRenteeComponent implements OnInit {
  rentee: Rentee;
  allRentees: Rentee[]=[];
  user:Rentee;
  private _ngZone: NgZone;
  notifications = new Array<NotificationDTO>();
  username:string;
  constructor(
              private router:Router,
              private renteeService:RenteeService, 
              private notificationService:NotificationService) { 
                this.rentee = new Rentee("","","","","","");
                this.username = JSON.parse(localStorage.getItem("user")).username;
              }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem("user"));
    if(this.user!= null){
      this.renteeService.getRenteeByUsername(this.user.username)
      .subscribe(value=>{
        if(value!=undefined){
          this.rentee = value;
          console.log(value)
        }
        else{
          this.rentee = new Rentee("","","","","","");
        }
      })
      this.renteeService.getAllRentees()
      .subscribe(value =>{ this.allRentees=value;
      console.log(this.allRentees)})
    }
    else{
      this.rentee = new Rentee("","","","","","");
    }
  }

  navigateTo(path) {
    this.router.navigate([`./renterer/${path}`]);
  }

  sendFriednRequest(rentee: Rentee){
    this.renteeService.sendFriendRequest(this.user.username, rentee.username)
    .subscribe(value=>{
      console.log(value);
      alert(`Uspesno poslat zahetev ${this.user.username} ka ${rentee.username}`)
    })
  }
  private subscribeToEvents(): void {  

    this.notificationService.messageReceived.subscribe((notification: NotificationDTO) => {  
      this._ngZone.run(() => {  
        
        if (this.username === notification.ReceiverUsername) {   
          this.notifications.push(notification);  
        }  
      });  
    });  
  }  
}
