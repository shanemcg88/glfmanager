using GLFManager.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.App
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
    }
}
