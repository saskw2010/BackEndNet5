using BackEnd.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealState.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BackEnd.DAL.Context
{
  public class BakEndContext : IdentityDbContext<ApplicationUser>, IBackEndContext
  {

    public BakEndContext(DbContextOptions<BakEndContext> options) : base(options)
    {
      
    }
    public virtual DbSet<WorkSpace> WorkSpaces { get; set; }
    public virtual DbSet<UserType> UserType { get; set; }
    public virtual DbSet<AspNetUsersTypes> AspNetUsersTypes { get; set; }
    public virtual DbSet<AspNetusertypjoin> AspNetusertypjoin { get; set; }
    public virtual DbSet<AspNetUsersTypes_roles> AspNetUsersTypes_roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<AspNetusertypjoin>()
         .HasKey(c => new { c.IdAspNetUsers, c.UsrTypID });

      modelBuilder.Entity<AspNetUsersTypes_roles>()
         .HasKey(c => new { c.IdAspNetRoles, c.UsrTypID });
    }
   

  }
}
