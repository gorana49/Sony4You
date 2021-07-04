import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateSonyModalComponent } from './update-sony-modal.component';

describe('UpdateSonyModalComponent', () => {
  let component: UpdateSonyModalComponent;
  let fixture: ComponentFixture<UpdateSonyModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateSonyModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateSonyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
