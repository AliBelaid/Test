namespace DatingApp.api.Helpers {
    public class PaginationHeader {
        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public PaginationHeader (int currentPage, int itemPerPage, int totalPages, int totalItems) {
            CurrentPage = currentPage;
            ItemPerPage = itemPerPage;
            TotalPages = totalPages;
            TotalItems = totalItems;
        }
    }
}