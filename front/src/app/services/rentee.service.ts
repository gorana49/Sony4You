import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environmentVariables } from "../constants/url-constants";
import { Rentee } from "../models/Rentee";
import { Sony } from "../models/Sony";
import { Game } from "../models/Game"
import { CommentDTO } from "../models/CommentDTO";
import {Comment } from "../models/Comment"

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

    addCommentToRenterer(commentDto: CommentDTO): Observable<any>{
      let url=this.baseUrl+`Rentee/AddCommentToRenterer`;
      return this.http.post<any>(url, commentDto);
    }

    deleteComment(date: Date): Observable<any>{
      let url=this.baseUrl+`Rentee/DeleteComment?date=${date}`;
      return this.http.delete<any>(url);
    }
    
    getCommentsForRenterer(rentererUsername: string): Observable<Comment[]>{
      let url=this.baseUrl+`Rentee/GetCommentsForRenterer?UsernameRenterer=${rentererUsername}`;
      return this.http.get<Comment[]>(url);
    }

    sendFriendRequest(sender: string, reciever:string): Observable<any>{
      let url=this.baseUrl+`Rentee/AddRequest?SenderRequestUsername=${sender}&ReceiverRequestUsername=${reciever}`;
      return this.http.post<any>(url,null);
    }
  
   
}