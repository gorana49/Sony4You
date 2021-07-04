import { Component, NgZone, OnInit } from '@angular/core';  
import { LoggedUser } from 'src/app/models/LoggedUser';
import { AuthService } from 'src/app/services/auth.service';
import { MessageDTO } from '../../models/MessageDTO';  
import { ChatService } from '../../services/chat.service';  

@Component({  
  selector: 'app-chat',  
  templateUrl: './chat.component.html',  
  styleUrls: ['./chat.component.css']  
})  
export class ChatComponent implements OnInit{  

  title = 'ClientApp';  
  txtMessage: string = '';
  textMessage1 : string='';  
  username:string;
  receiverUsername:string;
  connectionId:string ;
  uniqueID: string = new Date().getTime().toString();  
  messages = new Array<MessageDTO>();
  users = new Array<LoggedUser>();  
  message = new MessageDTO();  
  constructor(  
    private chatService: ChatService,
    private authService: AuthService,  
    private _ngZone: NgZone  
  ) {  
    this.subscribeToEvents();  
    this.username = JSON.parse(localStorage.getItem("user")).username;
    console.log(this.username);
  }  
  ngOnInit(): void {
    this.authService.getAllUsers()
    .subscribe((value)=>{
      this.users = value;
    },
    err => {
    alert(`Dogodila se greška pri ucitavanju svih logovanih usera.`)
  });
  }
  sendMessage(): void {  
    if (this.txtMessage) {  
      console.log("ts servis" + this.connectionId);
      this.message = new MessageDTO();  
      this.message.clientuniqueid = "";  
      this.message.type = "sent";  
      this.message.message = this.txtMessage;  
      this.message.date = new Date(); 
      this.message.receiverId = "";
      this.message.senderUsername = this.username;
      this.message.receiverUsername = this.receiverUsername; 
      this.messages.push(this.message);  
      this.chatService.sendMessageToRedis(this.message).subscribe((val)=> {});
      this.chatService.sendMessage(this.message);  
      this.txtMessage = '';  

    }  
  }  

  findUsername() :void{
    if(this.textMessage1)
    {
      this.receiverUsername = this.textMessage1;
    }
  }
  private subscribeToEvents(): void {  

    this.chatService.messageReceived.subscribe((message: MessageDTO) => {  
      this._ngZone.run(() => {  
        if (this.username === message.receiverUsername) {  
          message.type = "received";  
          this.messages.push(message);  
        }  
      });  
    });  
  }  
}  