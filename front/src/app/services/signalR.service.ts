import { Message } from './../models/message';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})

export class signalRService {

  public hubConnection: signalR.HubConnection;
  private readonly baseUrl = 'https://localhost:5001';
  constructor(private http:HttpClient) {

    this.startConnection();
   }
 
  public startConnection =()=>{
      this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl+"/messenger")
      .build()

      this.hubConnection
      .start()
      .then(()=> console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: '+err))

      
  }

  public sendMessage(mess:Message): void {
    this.hubConnection
      .invoke('sendToAll', mess)
      .catch(err => console.error(err));
  }

  

}
