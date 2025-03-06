using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Models;

namespace PizzaShop.Data.Data;

public partial class PizzaShopDbContext : DbContext
{
    public PizzaShopDbContext()
    {
    }

    public PizzaShopDbContext(DbContextOptions<PizzaShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Favouriteitem> Favouriteitems { get; set; }

    public virtual DbSet<Itemassociatemodifier> Itemassociatemodifiers { get; set; }

    public virtual DbSet<Menuitem> Menuitems { get; set; }

    public virtual DbSet<Modifiergroup> Modifiergroups { get; set; }

    public virtual DbSet<Modifieritem> Modifieritems { get; set; }

    public virtual DbSet<Orderassociatedtable> Orderassociatedtables { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Orderheader> Orderheaders { get; set; }

    public virtual DbSet<Orderitemmodifier> Orderitemmodifiers { get; set; }

    public virtual DbSet<Ordertaxesandfee> Ordertaxesandfees { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rolepermission> Rolepermissions { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Taxis> Taxes { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Waitingtoken> Waitingtokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=pizza_shop;Username=postgres;Password=tatva123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("unit", new[] { "pcs", "gm", "kg", "slice" });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Accountid).HasName("accounts_pkey");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Email, "accounts_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "unique_username").IsUnique();

            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Isrememberme).HasColumnName("isrememberme");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Categoryid)
                .HasDefaultValueSql("nextval('categoryid'::regclass)")
                .HasColumnName("categoryid");
            entity.Property(e => e.Categorydescription)
                .HasColumnType("character varying")
                .HasColumnName("categorydescription");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .HasColumnName("categoryname");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Cityid).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Cityname)
                .HasMaxLength(100)
                .HasColumnName("cityname");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Stateid).HasColumnName("stateid");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Stateid)
                .HasConstraintName("city_stateid_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Countryid).HasName("country_pkey");

            entity.ToTable("country");

            entity.HasIndex(e => e.Countrycode, "idx_country_code");

            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Countrycode)
                .HasMaxLength(3)
                .HasColumnName("countrycode");
            entity.Property(e => e.Countryname)
                .HasMaxLength(100)
                .HasColumnName("countryname");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.Customerid)
                .ValueGeneratedNever()
                .HasColumnName("customerid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customeremail)
                .HasMaxLength(100)
                .HasColumnName("customeremail");
            entity.Property(e => e.Customername)
                .HasMaxLength(50)
                .HasColumnName("customername");
            entity.Property(e => e.Customerphonenumber)
                .HasMaxLength(15)
                .HasColumnName("customerphonenumber");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<Favouriteitem>(entity =>
        {
            entity.HasKey(e => e.Favouriteitemid).HasName("favouriteitems_pkey");

            entity.ToTable("favouriteitems");

            entity.Property(e => e.Favouriteitemid)
                .ValueGeneratedNever()
                .HasColumnName("favouriteitemid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Item).WithMany(p => p.Favouriteitems)
                .HasForeignKey(d => d.Itemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("favouriteitems_itemid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Favouriteitems)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("favouriteitems_userid_fkey");
        });

        modelBuilder.Entity<Itemassociatemodifier>(entity =>
        {
            entity.HasKey(e => e.Itemassociatemodifierid).HasName("itemassociatemodifiers_pkey");

            entity.ToTable("itemassociatemodifiers");

            entity.Property(e => e.Itemassociatemodifierid)
                .ValueGeneratedNever()
                .HasColumnName("itemassociatemodifierid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Modifiergroupid).HasColumnName("modifiergroupid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Item).WithMany(p => p.Itemassociatemodifiers)
                .HasForeignKey(d => d.Itemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("itemassociatemodifiers_itemid_fkey");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Itemassociatemodifiers)
                .HasForeignKey(d => d.Modifiergroupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("itemassociatemodifiers_modifiergroupid_fkey");
        });

        modelBuilder.Entity<Menuitem>(entity =>
        {
            entity.HasKey(e => e.Menuitemid).HasName("menuitems_pkey");

            entity.ToTable("menuitems");

            entity.HasIndex(e => new { e.Menuitemid, e.Itemname }, "menuitems_menuitemid_itemname_key").IsUnique();

            entity.HasIndex(e => e.Shortcode, "menuitems_shortcode_key").IsUnique();

            entity.Property(e => e.Menuitemid)
                .HasDefaultValueSql("nextval('menuitemid'::regclass)")
                .HasColumnName("menuitemid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Defaulttax).HasColumnName("defaulttax");
            entity.Property(e => e.Deletedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Deletedby).HasColumnName("deletedby");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Itemimage).HasColumnName("itemimage");
            entity.Property(e => e.Itemname)
                .HasMaxLength(255)
                .HasColumnName("itemname");
            entity.Property(e => e.Itemtype).HasColumnName("itemtype");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("0")
                .HasColumnName("quantity");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Shortcode)
                .HasMaxLength(50)
                .HasColumnName("shortcode");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("status");
            entity.Property(e => e.Taxpercentage).HasColumnName("taxpercentage");
            entity.Property(e => e.Unitid).HasColumnName("unitid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Category).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menuitems_categoryid_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.Unitid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_unit");
        });

        modelBuilder.Entity<Modifiergroup>(entity =>
        {
            entity.HasKey(e => e.Modifiergroupid).HasName("modifiergroups_pkey");

            entity.ToTable("modifiergroups");

            entity.Property(e => e.Modifiergroupid)
                .ValueGeneratedNever()
                .HasColumnName("modifiergroupid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiergroupdescription)
                .HasMaxLength(255)
                .HasColumnName("modifiergroupdescription");
            entity.Property(e => e.Modifiergroupname)
                .HasMaxLength(50)
                .HasColumnName("modifiergroupname");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<Modifieritem>(entity =>
        {
            entity.HasKey(e => e.Modifieritemid).HasName("modifieritems_pkey");

            entity.ToTable("modifieritems");

            entity.Property(e => e.Modifieritemid)
                .ValueGeneratedNever()
                .HasColumnName("modifieritemid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiergroupid).HasColumnName("modifiergroupid");
            entity.Property(e => e.Modifieritemdescription)
                .HasMaxLength(255)
                .HasColumnName("modifieritemdescription");
            entity.Property(e => e.Modifieritemname)
                .HasMaxLength(255)
                .HasColumnName("modifieritemname");
            entity.Property(e => e.Modifierquantity).HasColumnName("modifierquantity");
            entity.Property(e => e.Modifierrate).HasColumnName("modifierrate");
            entity.Property(e => e.Unitid).HasColumnName("unitid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Modifieritems)
                .HasForeignKey(d => d.Modifiergroupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifieritems_modifiergroupid_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Modifieritems)
                .HasForeignKey(d => d.Unitid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_unitid");
        });

        modelBuilder.Entity<Orderassociatedtable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderassociatedtables_pkey");

            entity.ToTable("orderassociatedtables");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Specialrequest)
                .HasMaxLength(255)
                .HasColumnName("specialrequest");
            entity.Property(e => e.Tableid).HasColumnName("tableid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderassociatedtables)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order");

            entity.HasOne(d => d.Table).WithMany(p => p.Orderassociatedtables)
                .HasForeignKey(d => d.Tableid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_table");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.Orderdetailsid).HasName("orderdetails_pkey");

            entity.ToTable("orderdetails");

            entity.Property(e => e.Orderdetailsid)
                .ValueGeneratedNever()
                .HasColumnName("orderdetailsid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Itemnote)
                .HasMaxLength(255)
                .HasColumnName("itemnote");
            entity.Property(e => e.Menuitemid).HasColumnName("menuitemid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("1")
                .HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1")
                .HasColumnName("status");
            entity.Property(e => e.Unitprice).HasColumnName("unitprice");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Menuitem).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Menuitemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderdetails_menuitemid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderdetails_orderid_fkey");
        });

        modelBuilder.Entity<Orderheader>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("orderheader_pkey");

            entity.ToTable("orderheader");

            entity.Property(e => e.Orderid)
                .ValueGeneratedNever()
                .HasColumnName("orderid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customername)
                .HasMaxLength(30)
                .HasColumnName("customername");
            entity.Property(e => e.Customerphonenumber)
                .HasMaxLength(15)
                .HasColumnName("customerphonenumber");
            entity.Property(e => e.Numberofpersons).HasColumnName("numberofpersons");
            entity.Property(e => e.Ordercomment).HasColumnName("ordercomment");
            entity.Property(e => e.Orderheaderstatus).HasColumnName("orderheaderstatus");
            entity.Property(e => e.Orderrating).HasColumnName("orderrating");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<Orderitemmodifier>(entity =>
        {
            entity.HasKey(e => e.Orderitemmodifierid).HasName("orderitemmodifier_pkey");

            entity.ToTable("orderitemmodifier");

            entity.Property(e => e.Orderitemmodifierid)
                .ValueGeneratedNever()
                .HasColumnName("orderitemmodifierid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Itemmodifierquantity).HasColumnName("itemmodifierquantity");
            entity.Property(e => e.Modifieroptionid).HasColumnName("modifieroptionid");
            entity.Property(e => e.Orderdetailid).HasColumnName("orderdetailid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Modifieroption).WithMany(p => p.Orderitemmodifiers)
                .HasForeignKey(d => d.Modifieroptionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitemmodifier_modifieroptionid_fkey");

            entity.HasOne(d => d.Orderdetail).WithMany(p => p.Orderitemmodifiers)
                .HasForeignKey(d => d.Orderdetailid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitemmodifier_orderdetailid_fkey");
        });

        modelBuilder.Entity<Ordertaxesandfee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ordertaxesandfees_pkey");

            entity.ToTable("ordertaxesandfees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Taxid).HasColumnName("taxid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordertaxesandfees)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order");

            entity.HasOne(d => d.Tax).WithMany(p => p.Ordertaxesandfees)
                .HasForeignKey(d => d.Taxid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tax");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.Paymentid)
                .ValueGeneratedNever()
                .HasColumnName("paymentid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Ordertotalamount).HasColumnName("ordertotalamount");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(30)
                .HasColumnName("paymentmethod");
            entity.Property(e => e.Qrcode).HasColumnName("qrcode");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("permission_pkey");

            entity.ToTable("permission");

            entity.Property(e => e.Permissionid).HasColumnName("permissionid");
            entity.Property(e => e.Permissionname)
                .HasMaxLength(50)
                .HasColumnName("permissionname");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Rolepermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rolepermission_pkey");

            entity.ToTable("rolepermission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Canaddupdate).HasColumnName("canaddupdate");
            entity.Property(e => e.Candelete).HasColumnName("candelete");
            entity.Property(e => e.Canview).HasColumnName("canview");
            entity.Property(e => e.Permissionid).HasColumnName("permissionid");
            entity.Property(e => e.Roleid).HasColumnName("roleid");

            entity.HasOne(d => d.Permission).WithMany(p => p.Rolepermissions)
                .HasForeignKey(d => d.Permissionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rolepermission_permissionid_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Rolepermissions)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rolepermission_roleid_fkey");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Sectionid).HasName("sections_pkey");

            entity.ToTable("sections");

            entity.Property(e => e.Sectionid)
                .ValueGeneratedNever()
                .HasColumnName("sectionid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Sectiondescription)
                .HasMaxLength(255)
                .HasColumnName("sectiondescription");
            entity.Property(e => e.Sectionname)
                .HasMaxLength(50)
                .HasColumnName("sectionname");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Stateid).HasName("state_pkey");

            entity.ToTable("state");

            entity.HasIndex(e => e.Statecode, "idx_state_code");

            entity.Property(e => e.Stateid).HasColumnName("stateid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Statecode)
                .HasMaxLength(3)
                .HasColumnName("statecode");
            entity.Property(e => e.Statename)
                .HasMaxLength(100)
                .HasColumnName("statename");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("state_countryid_fkey");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("students_pkey");

            entity.ToTable("students");

            entity.Property(e => e.Studentid).HasColumnName("studentid");
            entity.Property(e => e.Studentname)
                .HasMaxLength(30)
                .HasColumnName("studentname");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Tableid).HasName("tables_pkey");

            entity.ToTable("tables");

            entity.Property(e => e.Tableid)
                .ValueGeneratedNever()
                .HasColumnName("tableid");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Sectionid).HasColumnName("sectionid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("true")
                .HasColumnName("status");
            entity.Property(e => e.Tablenumber)
                .HasMaxLength(20)
                .HasColumnName("tablenumber");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Section).WithMany(p => p.Tables)
                .HasForeignKey(d => d.Sectionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tables_sectionid_fkey");
        });

        modelBuilder.Entity<Taxis>(entity =>
        {
            entity.HasKey(e => e.Taxid).HasName("taxes_pkey");

            entity.ToTable("taxes");

            entity.Property(e => e.Taxid)
                .ValueGeneratedNever()
                .HasColumnName("taxid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Isenabled).HasColumnName("isenabled");
            entity.Property(e => e.Taxamount).HasColumnName("taxamount");
            entity.Property(e => e.Taxisdefault).HasColumnName("taxisdefault");
            entity.Property(e => e.Taxname)
                .HasMaxLength(30)
                .HasColumnName("taxname");
            entity.Property(e => e.Taxtype)
                .HasMaxLength(20)
                .HasColumnName("taxtype");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Unitid).HasName("units_pkey");

            entity.ToTable("units");

            entity.Property(e => e.Unitid).HasColumnName("unitid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Unitname)
                .HasMaxLength(10)
                .HasColumnName("unitname");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Userid)
                .HasDefaultValueSql("nextval('userid'::regclass)")
                .HasColumnName("userid");
            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(15)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Profileimage).HasColumnName("profileimage");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1")
                .HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(10)
                .HasColumnName("zipcode");

            entity.HasOne(d => d.Account).WithMany(p => p.Users)
                .HasForeignKey(d => d.Accountid)
                .HasConstraintName("users_accountid_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_roleid_fkey");
        });

        modelBuilder.Entity<Waitingtoken>(entity =>
        {
            entity.HasKey(e => e.Tokenid).HasName("waitingtoken_pkey");

            entity.ToTable("waitingtoken");

            entity.Property(e => e.Tokenid)
                .ValueGeneratedNever()
                .HasColumnName("tokenid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customeremail)
                .HasMaxLength(100)
                .HasColumnName("customeremail");
            entity.Property(e => e.Customername)
                .HasMaxLength(50)
                .HasColumnName("customername");
            entity.Property(e => e.Customerphonenumber)
                .HasMaxLength(15)
                .HasColumnName("customerphonenumber");
            entity.Property(e => e.Isassigned).HasColumnName("isassigned");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Numberofpersons).HasColumnName("numberofpersons");
            entity.Property(e => e.Sectionid).HasColumnName("sectionid");
            entity.Property(e => e.Tokennumber)
                .HasMaxLength(30)
                .HasColumnName("tokennumber");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Section).WithMany(p => p.Waitingtokens)
                .HasForeignKey(d => d.Sectionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("waitingtoken_sectionid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
