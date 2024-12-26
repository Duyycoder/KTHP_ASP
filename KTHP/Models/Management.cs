using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KTHP.Models
{
    public partial class Management : DbContext
    {
        public Management()
            : base("name=ManagementControllers")
        {
        }

        public virtual DbSet<Nhanvien> Nhanvien { get; set; }
        public virtual DbSet<Phongban> Phongban { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.matkhau)
                .IsUnicode(false);
        }
    }
}
