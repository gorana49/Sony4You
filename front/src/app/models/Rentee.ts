export class Rentee{
    id:string;
    name: string;
    username: string;
    email: string;
    password: string;
    phoneNumber: string;
    profilePictureUrl: string;

    constructor(name, username, email, pass, phone, picUrl) {
       this.name=name;
       this.username=username;
       this.email=email;
       this.password=pass;
       this.phoneNumber=phone;
       this.profilePictureUrl=picUrl;
    }
  
}