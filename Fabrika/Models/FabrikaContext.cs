using Microsoft.EntityFrameworkCore;

namespace Fabrika.Models
{
	public class FabrikaContext
		:DbContext
	{
		public DbSet<Product> Products{get;set;}
		
		/// <summary>
		///
		/// </summary>
		public FabrikaContext(DbContextOptions<FabrikaContext> options)
			:base(options)
			{			
			}
	}
}