using Aspose.Html;

namespace GifStorage.Helpers;

public static class TenorHelper {
	public static bool IsTenorUrl(string url) => url.StartsWith("https://tenor.com");

	public static string GetGifUrlByPageUrl(string pageUrl) {
		using var document = new HTMLDocument(pageUrl);

		var divElement = document.GetElementById("single-gif-container");
		var divWithMetaTags = (HTMLDivElement)divElement.FirstChild;

		var gifUrl = divWithMetaTags.Children
			.First(element => element.Attributes.GetNamedItem("itemprop")?.Value == "contentUrl")
			.Attributes.GetNamedItem("content")
			.Value;

		return gifUrl;
	}
}
