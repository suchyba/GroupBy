using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Represents the text file
    /// </summary>
    public class Document : Element
    {
        /// <summary>
        /// Path to the file on server
        /// </summary>
        [Required]
        public string FilePath { get; set; }
    }
}
