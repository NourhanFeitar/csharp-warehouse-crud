using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Warehouse
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Client_Req_Products> Client_Req_Products { get; set; }
        public virtual DbSet<Client_Request> Client_Request { get; set; }
        public virtual DbSet<Product_Measurments> Product_Measurments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sup_Req_Products> Sup_Req_Products { get; set; }
        public virtual DbSet<Supplier_Request> Supplier_Request { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<Transfer_Products> Transfer_Products { get; set; }
        public virtual DbSet<Warehouse_Data> Warehouse_Data { get; set; }
        public virtual DbSet<Warehouse_Products> Warehouse_Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Mobile_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Work_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Request>()
                .HasMany(e => e.Client_Req_Products)
                .WithRequired(e => e.Client_Request)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Client_Req_Products)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Measurments)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Sup_Req_Products)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Transfer_Products)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Warehouse_Products)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Products_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier_Request>()
                .HasMany(e => e.Sup_Req_Products)
                .WithRequired(e => e.Supplier_Request)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Mobile_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Work_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<Transfer>()
                .HasMany(e => e.Transfer_Products)
                .WithRequired(e => e.Transfer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse_Data>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Warehouse_Data>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<Warehouse_Data>()
                .HasMany(e => e.Transfers)
                .WithOptional(e => e.Warehouse_Data)
                .HasForeignKey(e => e.From_WH_ID);

            modelBuilder.Entity<Warehouse_Data>()
                .HasMany(e => e.Transfers1)
                .WithOptional(e => e.Warehouse_Data1)
                .HasForeignKey(e => e.To_WH_ID);

            modelBuilder.Entity<Warehouse_Data>()
                .HasMany(e => e.Warehouse_Products)
                .WithRequired(e => e.Warehouse_Data)
                .WillCascadeOnDelete(false);
        }
    }
}
