export class RentererList {
    startTime: string;
    period: string;
    address: string;
    joystick: string;
    notes: string;

    constructor(startTime, period, address, joystick, notes) {
        this.startTime = startTime;
        this.period = period;
        this.address = address;
        this.joystick = joystick;
        this.notes = notes
    }
}