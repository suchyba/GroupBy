using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
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
