export class Game{
    id:number;
    name:string;
    players:number;
    yearOfProduction:number;
    description:string;
    type:string;

    constructor( name, players, year, desc, type)
    {
        this.name = name;
        this.players = players;
        this.yearOfProduction = year;
        this.description = desc;
        this.type;
    }

}