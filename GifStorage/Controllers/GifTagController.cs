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
public class GifTagController(DataContext dataContext, IMapper mapper) : ControllerBase {
	[HttpPost]
	public async Task<IActionResult> Create(CreateGifTagVm? model) {
		if (model is null)
			return BadRequest("Model is null");

		if (!ModelState.IsValid)
			return BadRequest("Invalid data");

		try {
			if (await dataContext.GifTags.AnyAsync(gt => gt.GifId == model.GifId && gt.TagId == model.TagId))
				throw new ElementIsAlreadyExists();

			await dataContext.GifTags.AddAsync(mapper.Map<GifTag>(model));
			await dataContext.SaveChangesAsync();
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

		var target = await dataContext.GifTags
			.FirstOrDefaultAsync(gt => gt.GifId == model.GifId && gt.TagId == model.TagId);

		if (target is null)
			return Ok();

		try {
			dataContext.GifTags.Remove(target);
			await dataContext.SaveChangesAsync();
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
