export class NotificationDTO{
    SenderUsername:string;
    ReceiverUsername:string;
    Message: string;  

    constructor(SenderUsername,ReceiverUsername,message) {
      this.SenderUsername = SenderUsername;
      this.ReceiverUsername = ReceiverUsername;
      this.Message = message;
    }
  
}