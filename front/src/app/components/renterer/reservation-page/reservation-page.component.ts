import { Component, OnInit } from '@angular/core';
import { NotificationDTO } from 'src/app/models/NotificationDTO';
import { Renterer } from 'src/app/models/Renterer';
import { Reservation } from 'src/app/models/Reservation';
import { Sony } from 'src/app/models/Sony';
import { SonyReservations } from 'src/app/models/SonyReservations';
import { RentererService } from 'src/app/services/renterer.service';
import { ReservationService } from 'src/app/services/reservation.service';
import {NotificationService} from '../../../services/notification.service'

@Component({
  selector: 'app-reservation-page',
  templateUrl: './reservation-page.component.html',
  styleUrls: ['./reservation-page.component.css']
})
export class ReservationPageComponent implements OnInit {

  sonyReservations:SonyReservations[];
  constructor(private rentererService:RentererService,
              private reservationService: ReservationService,
              private notificationService:NotificationService) 
  { 
    this.sonyReservations = [];
  }

  ngOnInit(): void {
    var user:Renterer = JSON.parse(localStorage.getItem("user"));
    if(user!= null){
      this.rentererService.getAllSonysForRenterer(user.username)
      .subscribe(value=>{
        if(value!=undefined){
          value.map(el => this.sonyReservations.push(new SonyReservations(el,[])))
          this.sonyReservations.forEach(sony=> {
            this.reservationService.getAllReservationRequestForSony(sony.sony.serialNumber).subscribe(
              (val) => {
                val.map(e => sony.reservation.push(e))
              })
          })
        }
      })
    }
    console.log(this.sonyReservations)
  }

  btnApproveReservation(res:Reservation) {
    this.reservationService.approveReservationRequest(res).subscribe((val) => {})
    var user = JSON.parse(localStorage.getItem("user")).username;
    var notif:NotificationDTO = new NotificationDTO(user, res.usernameRentee, "Vasa rezervacija je uspesno prosla!");
    this.notificationService.sendNotification(notif);
    this.notificationService.sendNotificationToRedis(notif);
    location.reload();
  };
  
  btnCancelReservation(res:Reservation) {
    this.reservationService.cancelReservation(res).subscribe((val) => {})
    location.reload();
  }
}