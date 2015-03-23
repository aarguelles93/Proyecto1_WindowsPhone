using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Proyecto1.CustomClasses
{
    public class RubroDataContext : DataContext
    {
        // Specify the connection string as a static
        public static string DBConnectionString = @"isostore:/Project1.sdf";
        //public static string DBConnectionString = @"isostore:/University.sdf";

        // Pass the connection string to the base class.
        public RubroDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a single table for the Rubros.
        public Table<Rubro> Rubros;

    }
}
