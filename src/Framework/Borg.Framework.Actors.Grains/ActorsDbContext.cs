using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.Framework.Actors.Grains
{
    public class ActorsDbContext : DbContext
    {
        public ActorsDbContext(DbContextOptions<ActorsDbContext> options):base(options)
        {

        }
    }
}
