import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environmentVariables } from "../constants/url-constants";
import { Game } from "../models/Game";
import { Renterer } from "../models/Renterer";
import { Sony } from "../models/Sony";

@Injectable({
    providedIn: 'root'
  })
  export class RentererService {
  
    private baseUrl=environmentVariables.BACK_URL;
  
    constructor(private http: HttpClient) { }
  
    getRentererByUsername(username: string): Observable<Renterer>{
      let url=this.baseUrl+`Renterer/GetRenterer?username=${username}`;
      return this.http.get<Renterer>(url);
    }
    
    getAllSonysForRenterer( username: string): Observable<Sony[]>{
      let url=this.baseUrl +`Renterer/GetMySonys?Username=${username}`;
      return this.http.get<Sony[]>(url);
    }
  
    createSony(rentererUsername:string,sony: Sony):Observable<Sony>{
      let url=this.baseUrl+`Renterer/AddSony?rentererUsername=${rentererUsername}`;
      return this.http.post<Sony>(url,sony);
    }

    getAllRenterers(): Observable<Renterer[]>{
      let url=this.baseUrl+`Renterer/GetAllRenterers`;
      return this.http.get<Renterer[]>(url);
    }

    addGameForSony(serialNum:string,newGame: Game){
      let url=this.baseUrl+`Renterer/AddGame?SerialNumber=${serialNum}`;
      console.log(url)
      return this.http.post(url,newGame);
    }

    getGamesOnSony(serialNumber: string): Observable<Game[]>{
      let url=this.baseUrl+`Renterer/GetGamesOnSony?SerialNumber=${serialNumber}`;
      return this.http.get<Game[]>(url);
    }

    deleteGame(game:Game) {
      let url=this.baseUrl+`Renterer/DeleteGame?Name=${game.name}`;
      return this.http.delete(url);
    }

    updateGame(game: Game) {
      let url=this.baseUrl+`Renterer/UpdateGame?Players=${game.players}`;
      return this.http.put(url,game);
    }

    deleteSony(sony: Sony) {
      let url=this.baseUrl+`Renterer/DeleteSony?SerialNumber=${sony.serialNumber}`;
      return this.http.delete(url);
    }

    updateSony(sony: Sony) {
      let url=this.baseUrl+`Renterer/UpdateSony?Price=${sony.price}&SerialNumber=${sony.serialNumber}`;
      return this.http.put(url,null);
    }
  }
  