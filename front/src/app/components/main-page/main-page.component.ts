import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  displayLogIn:boolean;

  constructor() {
    this.displayLogIn = false;
   }

  ngOnInit(): void {
  }

  showLogInModal(): void {
    this.displayLogIn = true;
  }

  hideLogInModal(): void {
    this.displayLogIn = false;
  }

}
