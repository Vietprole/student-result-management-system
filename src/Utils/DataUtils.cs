namespace Student_Result_Management_System.Utils
{
    public static class DataUtils
    {
        /// <summary>
        /// Applies pagination to an IQueryable if pageNumber and pageSize are provided
        /// </summary>
        /// <typeparam name="T">The type of the elements in the query</typeparam>
        /// <param name="query">The IQueryable to apply pagination to</param>
        /// <param name="pageNumber">The page number (1-based). Returns empty result if less than or equal to 0.</param>
        /// <param name="pageSize">The page size. If not provided or less than or equal to 0, returns empty result.</param>
        /// <returns>The paginated IQueryable or empty collection if invalid pagination parameters</returns>
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int? pageNumber, int? pageSize)
        {
            // Return empty result for invalid parameters
            if ((pageSize.HasValue && pageSize.Value <= 0) || (pageNumber.HasValue && pageNumber.Value <= 0) ||
                (pageNumber.HasValue && !pageSize.HasValue))
            {
                return query.Take(0);
            }

            // Apply pagination only when pageSize is valid
            if (pageSize.HasValue)
            {
                // Skip records if pageNumber is provided and valid
                if (pageNumber.HasValue && pageNumber.Value > 0)
                {
                    query = query.Skip((pageNumber.Value - 1) * pageSize.Value);
                }

                // Take specified number of records
                query = query.Take(pageSize.Value);
            }

            return query;
        }
    }
}
