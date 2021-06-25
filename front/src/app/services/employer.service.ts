import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environmentVariables } from "../constants/url-constants";
import { IEmployer } from "../models/Employer";
import { IJob, Job } from "../models/Job";
import { IWorker } from "../models/Worker";

@Injectable({
    providedIn: 'root'
  })
  export class EmployerService {
  
    private baseUrl=environmentVariables.JSON_API_URL;
  
    constructor(private http: HttpClient) { }
  
    getEmployerByEmail(email: string): Observable<IEmployer>{
      let url=this.baseUrl+`/employer?email=${email}`;
      return this.http.get<IEmployer>(url);
    }
    
    getEventsByEmployerId( id: number): Observable<IJob[]>{
      let url=this.baseUrl+`/job?employerId=${id}`;
      return this.http.get<IJob[]>(url);
    }
  
    postEvent(job: Job):Observable<IJob>{
      let url=this.baseUrl+`/job`;
      return this.http.post<IJob>(url,job);
    }
  
    updateJob(idJob: number, job: Job):Observable<IJob>{
      let url=this.baseUrl+`/job/${idJob}`;
      return this.http.put<IJob>(url,job);
    }
  
    deleteJob( eventId: number):Observable<IJob>{
      let url=this.baseUrl+`/job/${eventId}`;
      return this.http.delete<IJob>(url);
    }
  
    getAllUsers() : Observable<IWorker[]>{
      let url=this.baseUrl+"/worker";
      return this.http.get<IWorker[]>(url);
    }
  }
  