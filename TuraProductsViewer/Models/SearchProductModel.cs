using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace TuraProductsViewer.Models
{
    public class SearchProductModel
    {
        [Required]
        public string ProductId { get; set; }
    }
}
