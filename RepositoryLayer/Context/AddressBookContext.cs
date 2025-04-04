﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options) : base(options) { }

        public DbSet<AddressBookEntity> Addresses { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
