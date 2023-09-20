namespace GifStorage.Data.Entities {
	public class GifTag {
		public long GifId { get; set; }
		public long TagId { get; set; }
		public Gif Gif { get; set; } = null!;
		public Tag Tag { get; set; } = null!;
	}
}
