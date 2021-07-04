import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPageRentererComponent } from './main-page-renterer.component';

describe('MainPageRentererComponent', () => {
  let component: MainPageRentererComponent;
  let fixture: ComponentFixture<MainPageRentererComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainPageRentererComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MainPageRentererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
