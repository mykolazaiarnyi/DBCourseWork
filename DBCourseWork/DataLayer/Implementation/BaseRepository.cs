using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataLayer.Implementation {
    public abstract class BaseRepository {
        protected readonly string _connectionString;

        public BaseRepository() {
            _connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build().GetConnectionString("DefaultConnection");
        }
    }
}
