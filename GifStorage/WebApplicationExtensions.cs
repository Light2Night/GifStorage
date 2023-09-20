using GifStorage.Data;
using GifStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GifStorage {
	public static class WebApplicationExtensions {
		public static void MigrateDB(this IApplicationBuilder builder) {
			using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<DataContext>();

			context.Database.Migrate();
		}

		public static void SeedTestData(this IApplicationBuilder builder) {
			using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<DataContext>();

			if (!context.Gifs.Any()) {
				context.Gifs.Add(new Gif {
					Url = "https://media.tenor.com/UpuNwwksW6kAAAAd/bigfoot-jinx-jinx.gif"
				});
				context.Gifs.Add(new Gif {
					Url = "https://media.discordapp.net/attachments/806654255918350396/981892603886837820/IMG_4870.gif"
				});
				context.Gifs.Add(new Gif {
					Url = "https://cdn.discordapp.com/attachments/1122933366057082940/1147541719181697145/tumblr_faad488515baf54df537979a9e630fd7_5a73cce7_400.gif"
				});
				context.Gifs.Add(new Gif {
					Url = "https://media.discordapp.net/attachments/802241430621650984/1093474011926757397/R.gif"
				});
				context.Gifs.Add(new Gif {
					Url = "https://media.tenor.com/ooEAKXqSRp0AAAAd/perro-xd-xd.gif"
				});
				context.Gifs.Add(new Gif {
					Url = "https://media.tenor.com/ZJAgeFAAwT4AAAAC/rat-playing-dead.gif"
				});
				context.Gifs.Add(new Gif {
					Url = "https://media.tenor.com/74i6b0Z0vCoAAAAd/jeonqmi-monkey.gif"
				});
				context.Gifs.Add(new Gif {
					Url = "https://media.tenor.com/ezRuC2ke2jMAAAAd/shannon-sharpe-suit-meme.gif"
				});
			}

			context.SaveChanges();
		}
	}
}