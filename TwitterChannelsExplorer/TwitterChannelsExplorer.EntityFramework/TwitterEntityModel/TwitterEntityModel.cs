namespace TwitterChannelsExplorer.EntityFramework.TwitterEntityModel
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class TwitterEntityModel : DbContext
	{
		public TwitterEntityModel()
			: base("name=TwitterEntity")
		{
		}

		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Channel> Channels { get; set; }
		public virtual DbSet<Tweet> Tweets { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Channel>()
				.Property(e => e.src_Name)
				.IsUnicode(false);

			modelBuilder.Entity<Channel>()
				.Property(e => e.name)
				.IsUnicode(false);
		}
	}
}
