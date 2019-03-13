export  interface Pagination {
  currentPage: number;
  itemPerPage: number;
  totalPages: number;
  totalItems: number;
  }
  export class  PaginationReuslt<T> {
    result: T;
    pagination: Pagination;
  }

