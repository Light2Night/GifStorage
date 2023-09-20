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
			}

			context.SaveChanges();
		}
	}
}