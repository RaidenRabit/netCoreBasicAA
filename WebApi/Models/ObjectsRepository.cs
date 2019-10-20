using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public static class ObjectsRepository
    {

        //Users
        static User adminUser = new User
        {
            Id = 1,
            Username = "adminUser",
            Roles = new List<string> { "admin" }
        };

        static User employeeUser = new User
        {
            Id = 2,
            Username = "employeeUser",
            Roles = new List<string> { "employee" }
        };

        static User partnerUser = new User
        {
            Id = 3,
            Username = "partnerUser",
            Roles = new List<string> { "partner" }
        };

        //Clinics
        static Clinic c1 = new Clinic
        {
            Id = 1,
            City = "City 1",
            CreatedAt = DateTime.Now,
            CreatedBy = partnerUser,
            Description = "Created by partnerUser, not active, partnerUser member",
            IsActive = false,
            Members = new List<User> { partnerUser },
            Name = "clinic 1",
            ZipCode = "9200"
        };
        static Clinic c2 = new Clinic
        {
            Id = 2,
            City = "City 2",
            CreatedAt = DateTime.Now,
            CreatedBy = employeeUser,
            Description = "Created by employeeUser, active, no members",
            IsActive = true,
            Name = "clinic 2",
            ZipCode = "9000"
        };
        static Clinic c3 = new Clinic
        {
            Id = 3,
            City = "City 3",
            CreatedAt = DateTime.Now,
            CreatedBy = adminUser,
            Description = "Created by adminUser, not active, no partners",
            IsActive = false,
            Name = "clinic 3",
            ZipCode = "123"
        };

        //lists
        public static List<Clinic> clinics = new List<Clinic> { c1, c2, c3 };
        public static List<User> users = new List<User> { adminUser, employeeUser, partnerUser };
    }
}
