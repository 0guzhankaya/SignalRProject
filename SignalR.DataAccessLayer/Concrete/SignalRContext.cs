using Microsoft.EntityFrameworkCore;
using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.Concrete
{
	public class SignalRContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=OGUZHAN\\SQLEXPRESS;Initial Catalog=SignalR.Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
		}

		public DbSet<About> Abouts { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Discount> Discounts { get; set; }
		public DbSet<Feature> Features { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<SocialMedia> SocialMedias { get; set; }
		public DbSet<Testimonial> Testimonials { get; set; }
	}
}
