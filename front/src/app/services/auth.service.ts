import { Injectable } from '@angular/core';
import { HttpClient} from "@angular/common/http";
import { Observable } from 'rxjs';
import { ILoggedUser } from '../models/LoggedUser';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
   private baseURL: string = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }

//LoggedUser if user exist or null if not
checkIfUserValid(user: ILoggedUser): Observable<ILoggedUser> {
    let url = this.baseURL + `redis/CheckIfUserIsValid`;
    return this.http.post<ILoggedUser>(url, user);
}
//crete new LoggedUser
registerLoggedUser(user: ILoggedUser) : Observable<ILoggedUser> {
    let url=this.baseURL + `redis/AddNewLoggedUser`;
    return this.http.post<ILoggedUser>(url,user);
}
//logIn user
logInUser(user: ILoggedUser) : Observable<ILoggedUser> {
    let url=this.baseURL + `redis/LogInUser`;
    return this.http.post<ILoggedUser>(url,user);
}
//logOut user
logOutUser(user: ILoggedUser) {
    let url=this.baseURL + `redis/LogOutUser`;
    return this.http.post<ILoggedUser>(url,user);
}
//check is user logIn
checkIsUserLogIn(user: ILoggedUser): Observable<ILoggedUser> {
    let url=this.baseURL + `redis/LogOutUser`;
    return this.http.post<ILoggedUser>(url,user);
}



// getAllUsers(): Observable<ILoggedUser[]>{
//     let url = this.baseURL + "/loggedUsers";
//     return this.http.get<ILoggedUser[]>(url);
// }

// getUserByEmail(email: string): Observable<ILoggedUser> {
//     let url = this.baseURL + `/loggedUsers?email=${email}`;
//     return this.http.get<ILoggedUser>(url);
// }



// postRegisterEmployer(emp:IEmployer):Observable<IEmployer>{
//     let url=this.baseURL+`/employer`;
//     return this.http.post<IEmployer>(url,emp);
//   }

//   postRegisterWorker(worker: IWorker): Observable<IWorker>{
//     let url=this.baseURL+`/worker`;
//     return this.http.post<IWorker>(url, worker);
//   }
}
