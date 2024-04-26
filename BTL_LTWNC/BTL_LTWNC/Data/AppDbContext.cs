using BTL_LTWNC.Models.EF;
using BTL_LTWNC.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWNC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> tblUser { get; set; }

        public DbSet<Role> tblRole { get; set; }

        public DbSet<ProductCategory> tblProductCategory { get; set; }

        public DbSet<Product> tblProduct { get; set; }

        public DbSet<Order> tblOrder { get; set; }

        public DbSet<OrderDetail> tblOrderDetail { get; set; }

        public DbSet<UserViewModel> userViewModels { get; set; }

        public DbSet<ProductViewModel> productViewModels { get; set; }

        public DbSet<OrderDetailViewModel> orderDetailViewModels { get; set; }
    }
}
