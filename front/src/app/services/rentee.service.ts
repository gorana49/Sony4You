import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environmentVariables } from "../constants/url-constants";
import { Rentee } from "../models/Rentee";
import { Sony } from "../models/Sony";

@Injectable({
    providedIn: 'root'
  })
  export class RenteeService {
  
    private baseUrl=environmentVariables.BACK_URL;
  
    constructor(private http: HttpClient) { }
  
    getRenteeByUsername(username: string): Observable<Rentee>{
      let url=this.baseUrl+`Rentee/GetRentee?username=${username}`;
      return this.http.get<Rentee>(url);
    }
    
    getAvailableSonys(): Observable<Sony[]>{
      let url=this.baseUrl+`Rentee/GetAvailableSonys`;
      return this.http.get<Sony[]>(url);
    }

    getAllRentees(): Observable<Rentee[]>{
        let url=this.baseUrl+`Rentee/GetAllRentees`;
        return this.http.get<Rentee[]>(url);
      }
  
    // createSony(sony: Sony):Observable<Sony>{
    //   let url=this.baseUrl+`/job`;
    //   return this.http.post<Sony>(url,sony);
    // }
}