export class Sony {
    serialNumber:string;
    notes:string;
    type:string;
    price:number;

    constructor(ser:string, type:string, notes:string, price:number) 
    {
        this.serialNumber = ser;
        this.type = type;
        this.notes = notes;
        this.price = price;
    }
}