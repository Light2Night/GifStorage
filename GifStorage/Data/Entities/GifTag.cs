namespace GifStorage.Data.Entities {
	public class GifTag {
		public long GifId { get; set; }
		public Gif Gif { get; set; } = null!;
		public long TagId { get; set; }
		public Tag Tag { get; set; } = null!;
	}
}
