export class LoggedUser{
    id:string;
    username: string;
    password: string;
    loggedIn: boolean;
    role: string;

    constructor(username, pass, lg, role) {
        this.username=username;
        this.password=pass;
        this.loggedIn=lg;
        this.role=role;
    }
  
}