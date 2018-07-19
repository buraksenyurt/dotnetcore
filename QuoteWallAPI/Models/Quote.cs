using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuoteWallAPI.Models
{
    ///<summary>
    ///Some quote from you
    ///</summary>
    public class Quote
    {
        ///<summary>Id of the Quote</summary>
        [Required]
        public int Id { get; set; }
        ///<summary>Text of the Quote</summary>
        [Required]
        public string Text { get; set; }
        ///<summary>Author of this Quote</summary>
        [DefaultValue("Anonymous")]
        public string Author { get; set; }
    }
}