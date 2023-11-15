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
public class TagController : ControllerBase {
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public TagController(DataContext dataContext, IMapper mapper) {
		_context = dataContext;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> Get() {
		var response = await _context.Tags
			.Include(t => t.GifTags)
			.ThenInclude(gt => gt.Gif)
			.Select(t => _mapper.Map<TagVm>(t))
			.ToListAsync();

		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateTagVm? model) {
		if (model is null)
			return BadRequest("Model is null");

		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

		try {
			//if (await _context.Tags.AnyAsync(t => t.Name == model.Name))
			//	throw new ElementIsAlreadyExists();

			await _context.Tags.AddAsync(_mapper.Map<Tag>(model));
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
