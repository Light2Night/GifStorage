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
public class GifTagController : ControllerBase {
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public GifTagController(DataContext dataContext, IMapper mapper) {
		_context = dataContext;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateGifTagVm? model) {
		if (model is null)
			return BadRequest("Model is null");

		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

		try {
			if (await _context.GifTags.AnyAsync(gt => gt.GifId == model.GifId && gt.TagId == model.TagId))
				throw new ElementIsAlreadyExists();

			await _context.GifTags.AddAsync(_mapper.Map<GifTag>(model));
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

	[HttpDelete]
	public async Task<IActionResult> Delete(DeleteGifTagVm? model) {
		if (model is null)
			return BadRequest("Model is null");

		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

		var target = await _context.GifTags
			.FirstOrDefaultAsync(gt => gt.GifId == model.GifId && gt.TagId == model.TagId);

		if (target is null)
			return Ok();

		try {
			_context.GifTags.Remove(target);
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
