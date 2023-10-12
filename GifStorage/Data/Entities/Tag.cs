namespace GifStorage.Data.Entities {
	public class Tag {
		public long Id { get; set; }
		public string Name { get; set; } = null!;
		public List<GifTag> GifTags { get; set; } = new();
	}
}
