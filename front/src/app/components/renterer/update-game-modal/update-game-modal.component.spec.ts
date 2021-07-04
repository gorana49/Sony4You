import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateGameModalComponent } from './update-game-modal.component';

describe('UpdateGameModalComponent', () => {
  let component: UpdateGameModalComponent;
  let fixture: ComponentFixture<UpdateGameModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateGameModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateGameModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
