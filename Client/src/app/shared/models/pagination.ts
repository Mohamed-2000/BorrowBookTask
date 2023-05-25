import { IBook } from "./Book";

export interface IPagination {
  currentPage: number
  pageSize: number
  count: number
  data: IBook[]
}
