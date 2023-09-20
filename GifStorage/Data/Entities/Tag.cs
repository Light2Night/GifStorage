namespace GifStorage.Data.Entities {
	public class Tag {
		public long Id { get; set; }
		public string Name { get; set; } = null!;
		public List<Gif> Gifs { get; set; } = new();
		public List<GifTag> GifTags { get; set; } = new();
	}
}
