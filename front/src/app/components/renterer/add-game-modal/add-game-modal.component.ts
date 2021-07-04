import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Game } from 'src/app/models/Game';
import { Sony } from 'src/app/models/Sony';
import { RentererService } from 'src/app/services/renterer.service';

@Component({
  selector: 'app-add-game-modal',
  templateUrl: './add-game-modal.component.html',
  styleUrls: ['./add-game-modal.component.css']
})
export class AddGameModalComponent implements OnInit {
  newGame:Game;
  @Output() cancelClicked: EventEmitter<any> = new EventEmitter();
  @Input() sonyAddGame: Sony;
  constructor(private rentererService:RentererService) { 
    this.newGame = new Game("","","","","");
  }

  ngOnInit(): void {
    
  }
  
  cancelModal(): void {
    this.cancelClicked.emit(null);
  }

  handleClick(): void {
    this.rentererService.addGameForSony(this.sonyAddGame.serialNumber,this.newGame);
    console.log(this.newGame)
    this.cancelModal();
  }
 
}
