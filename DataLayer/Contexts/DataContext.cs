﻿using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
  public DbSet<EventEntity> Events { get; set; }
  public DbSet<PackageEntity> Packages { get; set; }
  public DbSet<EventPackageEntity> EventsPackages { get; set; }
}
