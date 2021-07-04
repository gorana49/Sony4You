import { Renterer } from "./Renterer";
import {Comment} from "../models/Comment"

export class RentererComments{
    renterer: Renterer;
    commentArray: Comment[];

    constructor( renterer, commentArray: Comment[])
    {
        this.renterer=renterer;
        this.commentArray=commentArray;
    }

}