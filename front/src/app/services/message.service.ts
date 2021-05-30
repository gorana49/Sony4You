import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Message } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private readonly baseUrl = 'https://localhost:5001'
  constructor(private http:HttpClient) { }

  getIdsStudentsInChatWith(studentId:number) {
    return this.http.get<number[]>(URL + "/api/messages/chat-ids/student/" + studentId);
  }

  // getStudentsInChatWith(studentId:number) {
  //   return this.http.get<Student[]>(URL + "/api/messages/chats/student/" + studentId);
  // }

  startChat(conversation:any) {
    return this.http.post(URL + "/api/messages/add-conversation/temp", conversation);
  }

  getMessagePortion(params:any) {
    return this.http.get<Message[]>(URL + "/api/messages/receive", {'params': params})
  }

  deleteMessage(biggerId:number, smallerId:number) {
     this.http.delete(URL+"/api/messages/deleteConversation/user/"+biggerId+"/"+smallerId).subscribe(()=>{});
  }

  getConversationTimeLeft(senderId:number, receiverId:number) {
    return this.http.get<number>(URL + "/api/messages/time-left/sender/" + senderId + "/receiver/" + receiverId);
  }
}
