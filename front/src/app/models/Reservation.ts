import { RentererList } from "./RentererList";

export class Reservation {
    serialNumber: string;
    usernameRentee: string;
    renterList:RentererList;

    constructor(ser, user, rentList) {
        this.serialNumber = ser;
        this.usernameRentee = user;
        this.renterList = rentList;
    }
}