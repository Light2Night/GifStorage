using GifStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GifStorage.Data {
	public class DataContext : DbContext {
		public DataContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<GifTag>()
				.HasKey(x => new {
					x.GifId,
					x.TagId
				});
		}

		public DbSet<Gif> Gifs { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<GifTag> GifTags { get; set; }
	}
}
