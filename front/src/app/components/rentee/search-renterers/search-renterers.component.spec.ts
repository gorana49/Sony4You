import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchRenterersComponent } from './search-renterers.component';

describe('PretragaOglasiComponent', () => {
  let component: SearchRenterersComponent;
  let fixture: ComponentFixture<SearchRenterersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchRenterersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchRenterersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
