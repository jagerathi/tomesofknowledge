namespace FullStack.Domain
{
    /// <summary>
    /// List Componnet View Model
    /// </summary>
    public class ListComponentViewModel
    {
        /// <summary>
        /// Direction to sort
        /// </summary>
        public string SortDir { get; set; }

        /// <summary>
        /// Field to sort on
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Current page number
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Current page size
        /// </summary>
        public int? PageSize { get; set; }
    }
}
