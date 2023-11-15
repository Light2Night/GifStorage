using AutoMapper;
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
	private readonly IMapper _mapper;

	public GifController(DataContext dataContext, IMapper mapper) {
		_context = dataContext;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> Get() {
		return Ok(await _context.Gifs.Select(g => _mapper.Map<GifVm>(g)).ToListAsync());
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddGifVm? model) {
		if (model is null)
			return BadRequest("Model is null");

		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

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
