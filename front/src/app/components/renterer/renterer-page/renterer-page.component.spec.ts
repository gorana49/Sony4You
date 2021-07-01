import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentererPageComponent } from './renterer-page.component';

describe('RentererPageComponent', () => {
  let component: RentererPageComponent;
  let fixture: ComponentFixture<RentererPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentererPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RentererPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
