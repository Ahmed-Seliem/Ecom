import { IPhoto } from "./IPhoto"


export interface IProduct {
  name: string
  description: string
  newPrice: number
  oldPrice: number
  photos: IPhoto[]
  categoryName: string
}
