import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPageRenteeComponent } from './main-page-rentee.component';

describe('RentererPageComponent', () => {
  let component: MainPageRenteeComponent;
  let fixture: ComponentFixture<MainPageRenteeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainPageRenteeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MainPageRenteeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
