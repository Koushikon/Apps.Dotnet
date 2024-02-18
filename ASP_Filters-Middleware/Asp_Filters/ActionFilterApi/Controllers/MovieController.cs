using ActionFilterApi.Filters;
using ActionFilterApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActionFilterApi.Controllers;

[Route("api/Movie")]
[ApiController]
public class MovieController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var movie = new Movie
        {
            Id = id,
            Name = "Mission Impossible II",
            Genre = "Action",
            Director = "Christopher McQuarrie"
        };
        return Ok(movie);
    }


    /***
     * Used ValidateModelFilter Filter:
     * Validate Model is null or ModelState.IsValid check
     */
    [ServiceFilter(typeof(ValidateModelFilter))]
    [HttpPost]
    public IActionResult Post([FromBody] Movie movie)
    {
        // save the model data in database
        return CreatedAtRoute(nameof(Get), new { id = movie.Id }, movie);
    }


    /***
     * Used ValidateModelFilter Filter:
     * Validate Model is null or ModelState.IsValid check
     */
    [ServiceFilter(typeof(ValidateModelFilter))]
    [HttpPut("{id:guid}")]
    public IActionResult Put(Guid id, [FromBody] Movie movie)
    {
        // No data found in the database with this id
        //Movie movieObj = null;
        var movieObj = new Movie();
        if (movieObj == null)
        {
            return NotFound();
        }

        // Update the movie data in database
        return NoContent();
    }


    /***
     * Used ValidateModelFilter Filter:
     * Validate Model is null or ModelState.IsValid check
     * 
     * Used ValidateEntityExistsFilter Filter:
     * Validate if the provided is data is exists in the database or not
     * If yes add the model data in the HttpContext
     */
    [ServiceFilter(typeof(ValidateModelFilter))]
    [ServiceFilter(typeof(ValidateEntityExistsFilter<Movie>))]
    [HttpPut("UpdateMovie/{id:guid}")]
    public IActionResult UpdateMovie(Guid id, [FromBody] Movie movie)
    {
        var resultData = HttpContext.Items["entity"] as Movie;

        // Update the movie data in database
        return NoContent();
    }


    /***
     * Used ValidateEntityExistsFilter Filter:
     * Validate if the provided is data is exists in the database or not
     * If yes add the model data in the HttpContext
     */
    [ServiceFilter(typeof(ValidateEntityExistsFilter<Movie>))]
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var resultData = HttpContext.Items["entity"] as Movie;
        return NoContent();
    }
}