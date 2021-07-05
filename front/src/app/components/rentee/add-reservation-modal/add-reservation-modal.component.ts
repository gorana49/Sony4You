import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Rentee } from 'src/app/models/Rentee';
import { RentererList } from 'src/app/models/RentererList';
import { Reservation } from 'src/app/models/Reservation';
import { Sony } from 'src/app/models/Sony';
import { RentererService } from 'src/app/services/renterer.service';
import { ReservationService } from 'src/app/services/reservation.service';

@Component({
  selector: 'app-add-reservation-modal',
  templateUrl: './add-reservation-modal.component.html',
  styleUrls: ['./add-reservation-modal.component.css']
})
export class AddReservationModalComponent implements OnInit {
  newRenterersList: RentererList;
  @Output() cancelClicked: EventEmitter<any> = new EventEmitter();
  @Input() sonyForReservation: Sony;

  constructor(private reservationService:ReservationService) { 
    this.newRenterersList = new RentererList("","","", "","");
  }

  ngOnInit(): void {
    
  }
  
  cancelModal(): void {
    this.cancelClicked.emit(null);
  }

  handleClick(): void {
    console.log(this.sonyForReservation);
    var rentee:Rentee = JSON.parse(localStorage.getItem("user"));

    this.reservationService.addNewReservationRequest(
      new Reservation(this.sonyForReservation.serialNumber, rentee.username,this.newRenterersList))
      .subscribe(value=>{},
      err=>{
        alert(`Dogodila se greška pri rezervisanju sonija.`)
      });

    this.cancelModal();
  }
 
}
