using System;

namespace FullStack.Domain
{
    /// <summary>
    /// Component Entity
    /// </summary>
    public class Component
    {
        /// <summary>
        /// Component Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Component Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Component Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Component Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The component ID</param>
        public Component(int id)
        {
            Id = id;
            Name = $"Component {id}";
            Status = "Created";

            Random r = new Random();
            switch (r.Next(4))
            {
                case 1:
                    Type = "Video";
                    break;
                case 2:
                    Type = "Audio";
                    break;
                case 3:
                    Type = "Image";
                    break;
                case 4:
                default:
                    Type = "Text";
                    break;
            }
        }
    }
}
