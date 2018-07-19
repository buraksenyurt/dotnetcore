using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuoteWallAPI.Models;

namespace QuoteWallAPI.Controllers
{
    ///<summary>
    ///CRUD Controller for Daily Quote
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController 
        : ControllerBase
    {
        ///<summary>
        ///List all public quotes sorted by most recently liked.
        ///</summary>
        ///<remarks>with paging, this returns up to 100 quotes.</remarks>
        ///<return>Quotes list</return>
        ///<response code="200"></response>    
        [Produces("application/json")]  
        [HttpGet]
        public ActionResult<IEnumerable<Quote>> Get()
        {
            return new List<Quote>();
        }

        ///<summary>
        ///Return a specific Quote from ID
        ///</summary>
        ///<param name="id">ID of Quote</param>
        ///<return>Quotes list</return>
        ///<response code="200">If found</response>    
        ///<response code="404">If not found</response>
        [Produces("application/json")]
        [HttpGet("{id}")]
        public ActionResult<Quote> Get(int id)
        {
            return new Quote();
        }

        ///<summary>
        ///Add a new Quote to QDB
        ///</summary>
        ///<param name="value">JSON content of Quote</param>
        ///<remarks>
        ///Sample body content
        ///{"id":1,"Text":"Some words..","Author":"you"}
        ///</remarks>
        ///<response code="201">Added</response>    
        [Consumes("application/json")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        ///<summary>
        ///Update any Quote belongs to a specific Author and ID
        ///</summary>        
        ///<param name="id">Identity value of Quote</param>
        ///<param name="author">Author of Quote</param>
        ///<param name="value">JSON content of updates</param>
        ///<remarks>
        ///Sample body content
        ///{"Text":"Any update"}
        ///</remarks>
        ///<response code="201">Updated</response>    
        ///<response code="404">If not found from ID</response> 
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public void Put(int id,string author, [FromBody] string value)
        {
        }

        ///<summary>
        ///Delete any Quote from QDB
        ///</summary>
        ///<param name="id">ID of Quote</param>
        ///<response code="204">Deleted</response>    
        ///<response code="404">If not found from ID</response> 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
