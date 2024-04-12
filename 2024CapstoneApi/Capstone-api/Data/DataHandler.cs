using Capstone_api.Models;
using Capstone_api.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;

namespace Capstone_api.Data
{
    public class DataHandler
    {
        SQLQueryList sqlQueryList = new SQLQueryList();
        public DataHandler()
        {

        }

        public async Task<List<Person>> getAllPersons(GlobalDBContext dbContext)
        {
            var personList = new List<Person>();

            var conn = dbContext.Database.GetDbConnection();

            if(conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync();
            }

            using(var command = conn.CreateCommand())
            {
                command.Connection = conn;

                command.CommandText = sqlQueryList.GetPersonInfo();


                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var person = new Person();

                        person.Id = reader[0].ToString();
                        person.FName = reader[1].ToString();
                        person.LName = reader[2].ToString();
                        person.Email = reader[3].ToString();
                        person.Title = reader[4].ToString();
                        person.CID = reader[5].ToString();
                        person.AccessCode = reader[6].ToString();

                        personList.Add(person);
                    }
                }
                return personList;
            }
        }
    }
}
