using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        // Add NuGet - Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // To perform db migration for a table you must run the following (Package Management Console(PMC)) command.
        // PM> add-migration AddHotelRoomToDb ((Performs the migration "commit")).
        // PM> update-database ((to run the migration.))
        public DbSet<User> Users { get; set; }

        //public DbSet<HotelRoomImage> HotelRoomImages { get; set; }

        //public DbSet<HotelAmenity> HotelAmenities { get; set; }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<RoomOrderDetail> RoomOrderDetails { get; set; }
    }
}
