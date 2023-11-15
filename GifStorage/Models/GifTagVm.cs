namespace GifStorage.Models;

public class GifTagVm {
	public long GifId { get; set; }
	public GifVm Gif { get; set; } = null!;
	public long TagId { get; set; }
	public TagVm Tag { get; set; } = null!;
}
