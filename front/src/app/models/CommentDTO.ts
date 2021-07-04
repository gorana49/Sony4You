import { Comment } from "../models/Comment";

export class CommentDTO{
    username: string; //renterer's username
    usernameRentee: string;
    comment:Comment;

    constructor( renterersUsername, renteeUsername, comment)
    {
        this.username = renterersUsername;
        this.usernameRentee = renteeUsername;
        this.comment = comment;
    }

}
