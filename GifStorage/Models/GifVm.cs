namespace GifStorage.Models;

public class GifVm {
	public long Id { get; set; }
	public string Url { get; set; } = null!;
	public List<InnerTagVm> Tags { get; set; } = new();
}
