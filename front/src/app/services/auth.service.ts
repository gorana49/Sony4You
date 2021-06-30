import { Injectable } from '@angular/core'
import { environmentVariables } from '../constants/url-constants'
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { LoggedUser } from '../models/LoggedUser';


@Injectable({
    providedIn: 'root'
  })
  export class AuthService {

        private URL = environmentVariables.BACK_URL;

        constructor(private http: HttpClient) {}

        checkIfUserValid(loggedUser: LoggedUser): Observable<LoggedUser> {
            let url = this.URL + `Redis/CheckIfUserIsValid`;
            return this.http.post<LoggedUser>(url, loggedUser);
        }

        logInUser(loggedUser : LoggedUser): Observable<LoggedUser>{
            let url = this.URL + `Redis/LogInUser`;
            return this.http.post<LoggedUser>(url, loggedUser);
        }

//         getAllUsers(): Observable<ILoggedUser[]>{
//             let url = this.baseURL + "/loggedUsers";
//             return this.http.get<ILoggedUser[]>(url);
//         }

//         getUserByEmail(email: string): Observable<ILoggedUser> {
//             let url = this.baseURL + `/loggedUsers?email=${email}`;
//             return this.http.get<ILoggedUser>(url);
//         }

//         postRegisterLoggedUser(user: ILoggedUser) : Observable<ILoggedUser> {
//             let url=this.baseURL + `/loggedUsers`;
//             return this.http.post<ILoggedUser>(url,user);
//         }

//         postRegisterEmployer(emp:IEmployer):Observable<IEmployer>{
//             let url=this.baseURL+`/employer`;
//             return this.http.post<IEmployer>(url,emp);
//           }
        
//           postRegisterWorker(worker: IWorker): Observable<IWorker>{
//             let url=this.baseURL+`/worker`;
//             return this.http.post<IWorker>(url, worker);
//           }
 }