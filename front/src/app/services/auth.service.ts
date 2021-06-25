import { Injectable } from '@angular/core'
import { environmentVariables } from '../constants/url-constants'
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { ILoggedUser, LoggedUser } from '../models/LoggedUser';
import { Employer, IEmployer } from '../models/Employer';
import { IWorker } from '../models/Worker';

@Injectable({
    providedIn: 'root'
  })
  export class AuthService {
        private baseURL = environmentVariables.JSON_API_URL;

        constructor(private http: HttpClient) {}

        checkIfUserValid(email: string, password: string): Observable<ILoggedUser> {
            let url = this.baseURL + `/loggedUsers?email=${email}&&password=${password}`;
            return this.http.get<ILoggedUser>(url);
        }

        getAllUsers(): Observable<ILoggedUser[]>{
            let url = this.baseURL + "/loggedUsers";
            return this.http.get<ILoggedUser[]>(url);
        }

        getUserByEmail(email: string): Observable<ILoggedUser> {
            let url = this.baseURL + `/loggedUsers?email=${email}`;
            return this.http.get<ILoggedUser>(url);
        }

        postRegisterLoggedUser(user: ILoggedUser) : Observable<ILoggedUser> {
            let url=this.baseURL + `/loggedUsers`;
            return this.http.post<ILoggedUser>(url,user);
        }

        postRegisterEmployer(emp:IEmployer):Observable<IEmployer>{
            let url=this.baseURL+`/employer`;
            return this.http.post<IEmployer>(url,emp);
          }
        
          postRegisterWorker(worker: IWorker): Observable<IWorker>{
            let url=this.baseURL+`/worker`;
            return this.http.post<IWorker>(url, worker);
          }
  }