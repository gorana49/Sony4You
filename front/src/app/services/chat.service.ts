import { EventEmitter, Injectable } from '@angular/core';  
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';  
import { MessageDTO } from '../../app/models/MessageDTO';  
import * as signalR from '@microsoft/signalr';
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ChatService {  
  messageReceived = new EventEmitter<MessageDTO>();  
  connectionEstablished = new EventEmitter<Boolean>();  
  private _hubConnection;
  constructor(private http: HttpClient) {  
    this.beginConnection();
  }  
  
  sendMessageToRedis(message:MessageDTO)
  {
    return this.http.post("https://localhost:5001/api/Message/SendMessage", message);
  }
  
  sendMessage(message: MessageDTO) {  
    this._hubConnection.invoke('NewMessage', message); 
  }  

  beginConnection() {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5000/messenger`, {
        skipNegotiation:true,
        headers: {
          'Access-Control-Allow-Origin':'*',
        },
        transport: signalR.HttpTransportType.WebSockets,
      })
      .configureLogging(signalR.LogLevel.Debug)
      .build();

    const start = async () => {
      try 
      {
        if (this._hubConnection.state === signalR.HubConnectionState.Disconnected) {
          await this._hubConnection.start().catch((err) => console.log(err));
          console.log('SignalR re-Connected for warning-hub.');
        }
        console.log('SignalR still in Connected state. for warning-hub');
        
       // this.getConnectionId(this.senderUsername);
      }
       catch (err) {
        console.log(err);
        setTimeout(start, 5000);
      }
    };
    start();
    

    this._hubConnection.on(
      "MessageReceived",
      (data:any) => {
      console.log('warning hub came with params:');
        this.messageReceived.emit(data);
    }
  );
  }

  // public getConnectionId(senderUsername:string) {
  //   this._hubConnection.invoke('GetConnectionId', senderUsername).then(
  //     (data) => {
  //       console.log("id1 "+data);
  //         this._connectionId = data;
  //       }
  //   ).then(()=> { console.log(this._connectionId)});; 
  // }
}    