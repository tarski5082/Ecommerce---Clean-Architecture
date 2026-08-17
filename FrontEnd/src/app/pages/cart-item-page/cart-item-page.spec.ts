import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartItemPage } from './cart-item-page';

describe('CartItemPage', () => {
  let component: CartItemPage;
  let fixture: ComponentFixture<CartItemPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartItemPage],
    }).compileComponents();

    fixture = TestBed.createComponent(CartItemPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
