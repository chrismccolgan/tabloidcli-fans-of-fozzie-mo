using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        /*
        public JournalRepository(string connectionString) : base(connectionString) { }
        public List<Journal> GetAll() { }
        public Journal Get(int id) { }
        public void Insert(Journal entry) { }
        public void Update(Journal entry) { }
        public void Delete(int id) { }
        */
    }
}
