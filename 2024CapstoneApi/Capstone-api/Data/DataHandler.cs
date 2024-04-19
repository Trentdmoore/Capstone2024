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

        public async Task<List<AccessLog>> getAllAccessLogs(GlobalDBContext dbContext)
        {
            var AccessLogList = new List<AccessLog>();

            var conn = dbContext.Database.GetDbConnection();

            if (conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync();
            }

            using (var command = conn.CreateCommand())
            {
                command.Connection = conn;

                command.CommandText = sqlQueryList.GetAllAccessLogs();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var accessLog = new AccessLog();

                        accessLog.ID = reader.GetInt32("ID");
                        accessLog.LockNum = reader.GetInt32("LockNum");
                        accessLog.AccesTime = reader.GetDateTime("AccessTime");
                        accessLog.CID = reader.GetInt32("CID");
                        accessLog.Accepted = reader.GetBoolean("Accepted");

                        AccessLogList.Add(accessLog);
                    }

                }
                return AccessLogList;
            }
        }

        public async Task<List<RequestLog>> getAllRequestLogs(GlobalDBContext dbContext)
        {
            var RequestLogList = new List<RequestLog>();

            var conn = dbContext.Database.GetDbConnection();

            if (conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync();
            }

            using (var command = conn.CreateCommand())
            {
                command.Connection = conn;

                command.CommandText = sqlQueryList.GetAllRequestLogs();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var requestLog = new RequestLog();

                        requestLog.ID = reader.GetInt32("ID");
                        requestLog.LockNum = reader.GetInt32("LockNum");
                        requestLog.AccessCode = reader.GetString("AccessCode");
                        requestLog.DateRequested = reader.GetDateTime("DateRequested");
                        requestLog.DateApproved = reader.GetDateTime("DateApproved");
                        requestLog.AdvisorID = reader.GetInt32("AdvisorID");

                        RequestLogList.Add(requestLog);
                    }

                }
                return RequestLogList;
            }
        }
        public async Task<string> insertNewPerson(GlobalDBContext dbContext, Person person)
        {

            var conn = dbContext.Database.GetDbConnection();

            if (conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync();
            }

            using (var command = conn.CreateCommand())
            {
                command.Connection = conn;
                var param = new CustomDBParameter(command);
                command.Parameters.Add(param.CreateDbParameter("ID", person.Id));
                command.Parameters.Add(param.CreateDbParameter("FName", person.FName));
                command.Parameters.Add(param.CreateDbParameter("LName", person.LName));
                command.Parameters.Add(param.CreateDbParameter("Email", person.Email));
                command.Parameters.Add(param.CreateDbParameter("Title", person.Title));
                command.Parameters.Add(param.CreateDbParameter("CID", person.CID));
                command.Parameters.Add(param.CreateDbParameter("AccessCode", person.AccessCode));

                command.CommandText = sqlQueryList.InsertPersonInfo();


                var rowsaffected = command.ExecuteNonQuery();

                if(rowsaffected == 1)
                {
                    return "Success";
                }
                else
                {
                    return "Fail";
                }
            }
        }
    }
}
