namespace GifStorage.Data.Entities {
	public class Gif {
		public long Id { get; set; }
		public string Url { get; set; } = null!;
		public List<Tag> Tags { get; set; } = new();
		public List<GifTag> GifTags { get; set; } = new();
	}
}
