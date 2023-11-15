using GifStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GifStorage.Data;

public class DataContext : DbContext {
	public DataContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<GifTag>()
			.HasKey(x => new {
				x.GifId,
				x.TagId
			});

		modelBuilder.Entity<Gif>()
			.HasAlternateKey(g => g.Url);

		modelBuilder.Entity<Gif>()
			.Property(g => g.Url)
			.HasMaxLength(200);

		modelBuilder.Entity<Tag>()
			.Property(g => g.Name)
			.HasMaxLength(100);
	}

	public DbSet<Gif> Gifs { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<GifTag> GifTags { get; set; }
}
