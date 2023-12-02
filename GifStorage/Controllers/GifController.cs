using AutoMapper;
using GifStorage.Data;
using GifStorage.Data.Entities;
using GifStorage.Exceptions;
using GifStorage.Helpers;
using GifStorage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GifStorage.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class GifController(DataContext _dataContext, IMapper _mapper) : ControllerBase {
	[HttpGet]
	public async Task<IActionResult> Get() {
		var response = await _dataContext.Gifs
			.Include(g => g.GifTags)
			.ThenInclude(gt => gt.Tag)
			.Select(g => _mapper.Map<GifVm>(g))
			.ToListAsync();

		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromForm] CreateGifVm model) {
		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

		if (TenorHelper.IsTenorUrl(model.Url))
			model.Url = TenorHelper.GetGifUrlByPageUrl(model.Url);

		try {
			if (await _dataContext.Gifs.AnyAsync(g => g.Url == model.Url))
				throw new ElementIsAlreadyExists();

			await _dataContext.Gifs.AddAsync(_mapper.Map<Gif>(model));
			await _dataContext.SaveChangesAsync();
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
