using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public List<User> Members { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public User CreatedBy { get; set; }
    }
}
