import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DivisorPrimosComponent } from './divisor-primos.component';

describe('TableListComponent', () => {
  let component: DivisorPrimosComponent;
  let fixture: ComponentFixture<DivisorPrimosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DivisorPrimosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DivisorPrimosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
