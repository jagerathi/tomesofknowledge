using System.Collections.Generic;

namespace FullStack.Domain
{
    /// <summary>
    /// List Component API Response
    /// </summary>
    public class ListComponentResponse
    {
        /// <summary>
        /// Components
        /// </summary>
        public IEnumerable<Component> Components { get; set; }

        /// <summary>
        /// Total Components
        /// </summary>
        public int Total { get; set; }
    }
}
