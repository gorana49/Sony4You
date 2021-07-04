import { Reservation } from "./Reservation";
import { Sony } from "./Sony";

export class SonyReservations {
    sony: Sony;
    reservation:Reservation[];

    constructor(sony, reservations)
    {
        this.sony = sony;
        this.reservation = reservations;
    }
}