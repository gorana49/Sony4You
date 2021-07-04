import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environmentVariables } from "../constants/url-constants";
import { Renterer } from "../models/Renterer";
import { Sony } from "../models/Sony";

@Injectable({
    providedIn: 'root'
  })
  export class RentererService {
  
    private baseUrl=environmentVariables.BACK_URL;
  
    constructor(private http: HttpClient) { }
  
    getEmployerByUsername(username: string): Observable<Renterer>{
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
  
    // updateJob(idJob: number, job: Job):Observable<IJob>{
    //   let url=this.baseUrl+`/job/${idJob}`;
    //   return this.http.put<IJob>(url,job);
    // }
  
    // deleteJob( eventId: number):Observable<IJob>{
    //   let url=this.baseUrl+`/job/${eventId}`;
    //   return this.http.delete<IJob>(url);
    // }
  
    // getAllUsers() : Observable<IWorker[]>{
    //   let url=this.baseUrl+"/worker";
    //   return this.http.get<IWorker[]>(url);
    // }
  }
  