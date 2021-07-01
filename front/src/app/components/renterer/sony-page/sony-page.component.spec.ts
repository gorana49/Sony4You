import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SonyPageComponent } from './sony-page.component';

describe('SonyPageComponent', () => {
  let component: SonyPageComponent;
  let fixture: ComponentFixture<SonyPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SonyPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SonyPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
