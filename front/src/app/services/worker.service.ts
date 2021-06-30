import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { environmentVariables } from "../constants/url-constants";
import { IJob } from "../models/Job";
import { IJobEmployed, JobEmployed } from "../models/JobEmployed";
import { IJobSignedUp, JobSignedUp } from "../models/JobSignedUp";
import { IWorker, Worker } from "../models/Worker";

@Injectable({
    providedIn: 'root'
  })
  export class WorkerService {
    private baseUrl=environmentVariables.JSON_API_URL;
  
    constructor(private http: HttpClient) { }
  
    getWorkerByEmail(email: string): Observable<IWorker>{
      let url=this.baseUrl+`/worker?email=${email}`;
      return this.http.get<IWorker>(url).pipe(
        map(u => u[0])
      );
    }
  
    updateWorker(idWorker: number, user: Worker): Observable<IWorker>{
      let url=this.baseUrl+`/worker/${idWorker}`;
      return this.http.put<IWorker>(url,user) .pipe(
        map(u => u[0])
      );
    }
  
    getAllJobs(): Observable<IJob[]>{
      let url=this.baseUrl+"/job";
      return this.http.get<IJob[]>(url);
    }
  
    getAllJobSigned(): Observable<IJobSignedUp[]>{
      let url=this.baseUrl+"/jobSignedUp";
      return this.http.get<IJobEmployed[]>(url);
    }
  
    getAllJobSignedForWorker( idWorker: number): Observable<IJobSignedUp[]>{
      let url=this.baseUrl+ `/jobSignedUp?workerId=${idWorker}`;
      return this.http.get<IJobSignedUp[]>(url);
    }
  
    postJobSigned( jobSigned: JobSignedUp): Observable<IJobSignedUp>{
      let url=this.baseUrl+"/jobSignedUp";
      return this.http.post<IJobSignedUp>(url,jobSigned);
    }
  
    deletJobSigned(id: number): Observable<IJobSignedUp>{
      let url=this.baseUrl+`/jobSignedUp/${id}`;
      return this.http.delete<IJobSignedUp>(url);
    }
  
    getAllJobsEmployed(): Observable<IJobEmployed[]>{
      let url=this.baseUrl+"/jobEmployed";
      return this.http.get<IJobEmployed[]>(url);
    }
  
    getAllJobEmployedForUser( id: number): Observable<IJobEmployed[]>{
      let url=this.baseUrl+ `/jobEmployed?user=${id}`;
      return this.http.get<IJobEmployed[]>(url);
    }
  
   
    postJobEmployed( job: JobEmployed): Observable<IJobEmployed>{
      let url=this.baseUrl+"/jobEmployed";
      return this.http.post<IJobEmployed>(url,job);
    }
  
    deletJobEmployed(id: number): Observable<IJobEmployed>{
      let url=this.baseUrl+`/jobEmployed/${id}`;
      return this.http.delete<IJobEmployed>(url);
    }
  
  }
  