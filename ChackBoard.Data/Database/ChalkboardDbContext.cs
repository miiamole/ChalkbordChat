using ChackBoard.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChackBoard.Data.Database
{
    public class ChalkboardDbContext : DbContext
    {
        public ChalkboardDbContext(DbContextOptions<ChalkboardDbContext> options) : base(options)
        {
            
        }

        public DbSet<MessageModel> Messages { get; set; }
    }
}
