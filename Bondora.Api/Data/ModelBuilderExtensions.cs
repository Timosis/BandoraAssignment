using Bandora.Models;
using Bondora.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Firstname = "Ava",
                    Lastname = "Mackenzie",
                    Email = "ava.mackenzie@bondora",
                    Points = 0

                },
                new Customer
                {
                    Id = 2,
                    Firstname = "Lucas",
                    Lastname = "Young",
                    Email = "lucas.young@bondora",
                    Points = 0
                },
                new Customer
                {
                    Id = 3,
                    Firstname = "Dorothy",
                    Lastname = "Wilkins",
                    Email = "dorothy.wilkins@bondora",
                    Points = 0
                },
                new Customer
                {
                    Id = 4,
                    Firstname = "Dominic",
                    Lastname = "Buckland",
                    Email = "dorothy.wilkins@bondora",
                    Points = 0
                },
                new Customer
                {
                    Id = 5,
                    Firstname = "Joe",
                    Lastname = "Harris",
                    Email = "joe.harris@bondora",
                    Points = 0
                }
            );
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    Name = "Caterpillar Bulldozer",
                    Type = EquiptmentType.Heavy,
                },
                new Equipment
                {
                    Id = 2,
                    Name = "Kamaz truck",
                    Type = EquiptmentType.Regular,
                },
                new Equipment
                {
                     Id = 3,
                     Name = "Komatsu Crane",
                     Type = EquiptmentType.Heavy,
                },
                new Equipment
                {
                     Id = 4,
                     Name = "Volvo Steamroller",
                     Type = EquiptmentType.Regular,
                },
                new Equipment
                {
                     Id = 5,
                     Name = "Bosch Jackhammer",
                     Type = EquiptmentType.Specialize,
                }
            );
        }
    }
}
