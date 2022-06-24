namespace Api.Helpers.Pagination
{
    public static class PaginationBuilder<T>
    {
        public static object ToPagination(IList<T> listData, int page, int itemsPerPage)
        {
            var items = listData.Skip((page - 1) * itemsPerPage)
                                .Take(itemsPerPage)
                                .ToList();

            var totalPages = Math.Ceiling(listData.Count() / (float)itemsPerPage);

            return new
            {
                items = items,
                currentPage = page,
                totalItems = listData.Count(),
                totalPages = totalPages
            };
        }
    }
}
