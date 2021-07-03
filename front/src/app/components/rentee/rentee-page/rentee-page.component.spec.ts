import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenteePageComponent } from './rentee-page.component';

describe('RentererPageComponent', () => {
  let component: RenteePageComponent;
  let fixture: ComponentFixture<RenteePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RenteePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RenteePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
