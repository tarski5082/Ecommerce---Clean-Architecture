import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductdetailPage } from './productdetail-page';

describe('ProductdetailPage', () => {
  let component: ProductdetailPage;
  let fixture: ComponentFixture<ProductdetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductdetailPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ProductdetailPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
