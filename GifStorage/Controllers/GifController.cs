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
public class GifController : ControllerBase {
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public GifController(DataContext dataContext, IMapper mapper) {
		_context = dataContext;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> Get() {
		var response = await _context.Gifs
			.Include(g => g.GifTags)
			.ThenInclude(gt => gt.Tag)
			.Select(g => _mapper.Map<GifVm>(g))
			.ToListAsync();

		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateGifVm? model) {
		if (model is null)
			return BadRequest("Model is null");

		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

		if (TenorHelper.IsTenorUrl(model.Url))
			model.Url = TenorHelper.GetGifUrlByPageUrl(model.Url);

		try {
			if (await _context.Gifs.AnyAsync(g => g.Url == model.Url))
				throw new ElementIsAlreadyExists();

			await _context.Gifs.AddAsync(_mapper.Map<Gif>(model));
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
