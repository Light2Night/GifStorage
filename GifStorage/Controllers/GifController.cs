using GifStorage.Data;
using GifStorage.Data.Entities;
using GifStorage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GifStorage.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class GifController : ControllerBase {
	private readonly DataContext _сontext;

	public GifController(DataContext dataContext) {
		_сontext = dataContext;
	}

	[HttpGet]
	public async Task<IEnumerable<Gif>> Get() {
		return await _сontext.Gifs.ToListAsync();
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddGifModel? model) {
		if (model is null)
			return BadRequest("Model is null");

		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

		try {
			await _сontext.Gifs.AddAsync(new Gif {
				Url = model.Url
			});
			await _сontext.SaveChangesAsync();
		}
		catch (Exception ex) {
			return StatusCode(500, "Internal server error: " + ex.Message);
		}

		return Ok();
	}
}
