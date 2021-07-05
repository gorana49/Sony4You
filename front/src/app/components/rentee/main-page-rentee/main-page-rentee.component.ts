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
