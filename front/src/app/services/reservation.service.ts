import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environmentVariables } from "../constants/url-constants";
import { Reservation } from "../models/Reservation";

@Injectable({
    providedIn: 'root'
  })
  export class ReservationService {
  
    private baseUrl=environmentVariables.BACK_URL;
  
    constructor(private http: HttpClient) { }
  
    addNewReservationRequest(reservation: Reservation){
      let url=this.baseUrl+`Reservation/AddNewReservationRequest`;
      return this.http.post(url, reservation);
    }
    
    cancelReservation(reservation: Reservation){
      let url=this.baseUrl +`Reservation/CancelReservation`;
      return this.http.post(url,reservation);
    }
  
    approveReservationRequest(reservation:Reservation){
      let url=this.baseUrl+`Reservation/ApproveReservationRequest`;
      return this.http.post(url,reservation);
    }

    getAllReservationRequestForSony(serial:string): Observable<Reservation[]>{
      let url=this.baseUrl+`Reservation/GetAllReservationRequestForSony?serialNumber=${serial}`;
      return this.http.get<Reservation[]>(url);
    }
  }
  