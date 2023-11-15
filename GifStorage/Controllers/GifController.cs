using GifStorage.Data;
using GifStorage.Data.Entities;
using GifStorage.Exceptions;
using GifStorage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GifStorage.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class GifController : ControllerBase {
	private readonly DataContext _context;

	public GifController(DataContext dataContext) {
		_context = dataContext;
	}

	[HttpGet]
	public async Task<IActionResult> Get() {
		return Ok(await _context.Gifs.ToListAsync());
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddGifModel? model) {
		if (model is null)
			return BadRequest("Model is null");

		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

		try {
			if (await _context.Gifs.AnyAsync(g => g.Url == model.Url))
				throw new ElementIsAlreadyExists();

			await _context.Gifs.AddAsync(new Gif {
				Url = model.Url
			});
			await _context.SaveChangesAsync();
		}
		catch (ElementIsAlreadyExists ex) {
			return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
		}
		catch (Exception ex) {
			return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
		}

		return Ok();
	}
}
