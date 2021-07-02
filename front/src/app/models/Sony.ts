export class Sony {
    notes: string;
    serialNumber: string;
    type: string;
    price: number;

    constructor(notes, serNo, type, price){
        this.notes=notes;
        this.serialNumber=serNo;
        this.type=type;
        this.price=price;
    }
}