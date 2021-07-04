export class SonyCreate {
    rentererUsername:string;
    notes: string;
    serialNumber: string;
    type: string;
    price: number;

    constructor(rentUser,notes, serNo, type, price){
        this.rentererUsername = rentUser;
        this.notes=notes;
        this.serialNumber=serNo;
        this.type=type;
        this.price=price;
    }
}