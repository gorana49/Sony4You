import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Game } from 'src/app/models/Game';
import { RentererService } from 'src/app/services/renterer.service';

@Component({
  selector: 'app-update-game-modal',
  templateUrl: './update-game-modal.component.html',
  styleUrls: ['./update-game-modal.component.css']
})
export class UpdateGameModalComponent implements OnInit {
  @Output() cancelClicked: EventEmitter<any> = new EventEmitter();
  @Input() updateGame: Game;

  constructor(private rentererService:RentererService) { 
    
  }

  ngOnInit(): void {
    
  }
  
  cancelModal(): void {
    this.cancelClicked.emit(null);
  }

  handleClick(): void {
    this.rentererService.updateGame(this.updateGame).subscribe(
      (val) => {}
    )
    this.cancelModal();
  }

}
