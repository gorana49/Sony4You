import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environmentVariables } from "../constants/url-constants";
import { Game } from "../models/Game";
import { Renterer } from "../models/Renterer";
import { Sony } from "../models/Sony";
import { SonyCreate } from "../models/SonyCreate";

@Injectable({
    providedIn: 'root'
  })
  export class RentererService {
  
    private baseUrl=environmentVariables.BACK_URL;
  
    constructor(private http: HttpClient) { }
  
    getRentererByUsername(username: string): Observable<Renterer>{
      let url=this.baseUrl+`Renterer/GetRenterer?username=${username}`;
      return this.http.get<Renterer>(url);
    }
    
    getAllSonysForRenterer( username: string): Observable<Sony[]>{
      let url=this.baseUrl +`Renterer/GetMySonys?Username=${username}`;
      return this.http.get<Sony[]>(url);
    }
  
    createSony(sony: Sony):Observable<Sony>{
      let url=this.baseUrl+`Renterer/AddSony`;
      return this.http.post<Sony>(url,sony);
    }

    getAllRenterers(): Observable<Renterer[]>{
      let url=this.baseUrl+`Renterer/GetAllRenterers`;
      return this.http.get<Renterer[]>(url);
    }

    getAllGamesForSony(serial:string) : Observable<Game[]>{
      let url=this.baseUrl+`Renterer/GetGamesOnSony?SerialNumber=${serial}`;
      return this.http.get<Game[]>(url);
    }

    addGameForSony(serialNum:string,newGame: Game){
      let url=this.baseUrl+`Renterer/AddGame?SerialNumber=${serialNum}`;
      return this.http.post(url,newGame);
    }
  
    // updateJob(idJob: number, job: Job):Observable<IJob>{
    //   let url=this.baseUrl+`/job/${idJob}`;
    //   return this.http.put<IJob>(url,job);
    // }
  
    // deleteJob( eventId: number):Observable<IJob>{
    //   let url=this.baseUrl+`/job/${eventId}`;
    //   return this.http.delete<IJob>(url);
    // }
  
 
  }
  