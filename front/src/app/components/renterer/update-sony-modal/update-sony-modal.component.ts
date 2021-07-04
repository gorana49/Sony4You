import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Sony } from 'src/app/models/Sony';
import { RentererService } from 'src/app/services/renterer.service';

@Component({
  selector: 'app-update-sony-modal',
  templateUrl: './update-sony-modal.component.html',
  styleUrls: ['./update-sony-modal.component.css']
})
export class UpdateSonyModalComponent implements OnInit {
  @Output() cancelClicked: EventEmitter<any> = new EventEmitter();
  @Input() updateSony: Sony;

  constructor(private rentererService:RentererService) { 
  }

  ngOnInit(): void {
    
  }
  
  cancelModal(): void {
    this.cancelClicked.emit(null);
  }

  handleClick(): void {
    this.rentererService.updateSony(this.updateSony).subscribe(
      (val) => {}
    )
    this.cancelModal();
  }

}
