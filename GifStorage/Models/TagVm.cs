namespace GifStorage.Models;

public class TagVm {
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	public List<InnerGifVm> Gifs { get; set; } = new();
}
