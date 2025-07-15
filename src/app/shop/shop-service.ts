import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICategory } from '../shared/Models/ICategory';
import { IPagination } from '../shared/Models/IPagination';
import { IProduct } from '../shared/Models/IProduct';
import { ProductParam } from '../shared/Models/ProductParam';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl='https://localhost:44361/api/';
  Product: IProduct[];
  constructor(private http:HttpClient) { }

 getProduct(ProductParam:ProductParam) {
  let params = new HttpParams();
if (ProductParam.categoryId) {
  params = params.append("categoryId", ProductParam.categoryId.toString());
}
if (ProductParam.sortSelected) {
  params = params.append("sort", ProductParam.sortSelected);
}
if (ProductParam.search) {
  params = params.append("search", ProductParam.search);
}
params = params.append("PageNumber", ProductParam.pageNumber);
params = params.append("PageSize", ProductParam.pageSize);
  console.log('Params:', params.toString()); // Debug
  return this.http.get<IPagination>(this.baseUrl + "Products/get-all", { params });
}


  getCategory()
  {
    return this.http.get<ICategory[]>(this.baseUrl+ "Categories/get-all")

  }
}
