using GifStorage.Data;
using GifStorage.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GifStorage.Controllers {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GifController : ControllerBase {
		private readonly DataContext _dataContext;

		public GifController(DataContext dataContext) {
			_dataContext = dataContext;
		}

		[HttpGet]
		public IEnumerable<Gif> Get() {
			return _dataContext.Gifs.ToList();
		}
	}
}
