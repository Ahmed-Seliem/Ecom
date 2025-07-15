import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ICategory } from '../shared/Models/ICategory';
import { IPagination } from '../shared/Models/IPagination';
import { IProduct } from '../shared/Models/IProduct';
import { ShopService } from './shop-service';
import { ProductParam } from '../shared/Models/ProductParam';

@Component({
  selector: 'app-shop',
  standalone: false,
  templateUrl: './shop.html',
  styleUrl: './shop.scss'
})
export class Shop implements OnInit {
constructor(private shopService:ShopService ){}

  ngOnInit(): void {
    this.getAllProduct()
    this.getAllCategory()
  }
  trackByIndex(index: number): number {
    return index;
  }

//Get Product
product :IProduct[];
totalCount:number;
ProductParam= new ProductParam()
getAllProduct() {
  this.shopService.getProduct(this.ProductParam).subscribe({
    next: (value: IPagination) => {
      this.product = value.data;
      this.totalCount = value.totalCount || 0;
      this.ProductParam.pageNumber = value.pageNumber;
      this.ProductParam.pageSize = value.pageSize;
    },
    error: (err) => {
      console.error('API Error:', err); // Debug API errors
    }
  });
}

OnChangePage(event:any)
{
this.ProductParam.pageNumber=event;
this.getAllProduct()
}

//Get Category
Category: ICategory[];
 getAllCategory()
  {
    this.shopService.getCategory().subscribe({
      next:((value)=>{
        this.Category=value
        console.log(this.Category)
      })

    })
  }

SelectedId(categoryId: number) {
  this.ProductParam.categoryId = categoryId;
  console.log('Selected CategoryId:', this.ProductParam.categoryId); // Debug
  this.getAllProduct();
}

  //sorting by Price
  SortingOption=
[
  {name:'Price', value:'Name'},
  {name:'Price:min-max',value:'PriceAce'},
  {name:'Price:max-min',value:'PriceDce'}
]

SortingByPrice(sort:Event)
{
this.ProductParam.sortSelected= (sort.target as HTMLInputElement).value
this.getAllProduct()
}

//sort by Name

OnSearch(Search: string) {
  this.ProductParam.search = Search || ''; // Fallback to empty string if undefined
  this.getAllProduct();
}

@ViewChild('search') searchInput :ElementRef
@ViewChild('SortSelected') selected :ElementRef

//reset all
ResetValue()
{
  this.ProductParam.search=""
  this.ProductParam.sortSelected=this.SortingOption[0].value;
  this.ProductParam.categoryId = 0; // Clear category filter
  this.searchInput.nativeElement.value='';
  this.selected.nativeElement.selectedIndex=0;
  this.getAllProduct();

}
}
