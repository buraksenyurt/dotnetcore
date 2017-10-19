using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fabrika.Models
{
	public class Product
	{
		[Required]
		public long Id {get;set;}
		[Required]
		public string Name {get;set;}
		[DefaultValue("1.50")]
		public double UnitPrice {get;set;}
	}
}