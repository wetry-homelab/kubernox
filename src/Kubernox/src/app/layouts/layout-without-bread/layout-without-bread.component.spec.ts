import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutWithoutBreadComponent } from './layout-without-bread.component';

describe('LayoutWithoutBreadComponent', () => {
  let component: LayoutWithoutBreadComponent;
  let fixture: ComponentFixture<LayoutWithoutBreadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LayoutWithoutBreadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutWithoutBreadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
