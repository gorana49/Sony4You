export class MessageDTO{
    clientuniqueid:string;
    senderUsername:string;
    receiverId:string;
    receiverUsername:string;
    type:string;  
    message: string;  
    date:Date;  

    // constructor(clientuniqueid, type, message, ReceiverUsername, date) {
    //   this.clientuniqueid = clientuniqueid;
    //   this.type = type;
    //   this.message = message;
    //   this.date = date;
    // }
  
}