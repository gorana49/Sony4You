export interface ILoggedUser {
    username: string;
    password: string;
    role: string;
    loggedIn: boolean;
}

export class LoggedUser implements ILoggedUser{
    username: string;
    password: string;
    role: string;
    loggedIn: boolean;

    constructor(username, password, role, loggedIn) {
        this.username=username;
        this.password=password;
        this.role=role;
        this.loggedIn = loggedIn;
    }
  
}