import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraphCard } from './graph-card';

describe('GraphCard', () => {
  let component: GraphCard;
  let fixture: ComponentFixture<GraphCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GraphCard],
    }).compileComponents();

    fixture = TestBed.createComponent(GraphCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
