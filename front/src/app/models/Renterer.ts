export class Renterer{
    id:string;
    name: string;
    username: string;
    email: string;
    password: string;
    phoneNumber: string;
    address: string;
    companyName: string;
    profilePictureUrl: string;

    constructor(name, username, email, pass, phone, addr, company, picUrl) {
       this.name=name;
       this.username=username;
       this.email=email;
       this.password=pass;
       this.phoneNumber=phone;
       this.address=addr;
       this.companyName=company;
       this.profilePictureUrl=picUrl;
    }
  
}