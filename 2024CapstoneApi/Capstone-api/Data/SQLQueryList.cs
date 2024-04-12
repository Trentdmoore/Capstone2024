namespace Capstone_api.Data
{
    public class SQLQueryList
    {
        public SQLQueryList() { }

        public string GetPersonInfo()
        {
            string sql =
                @"SELECT ID, FName, LName, Email, Title, CID, AccessCode
                  FROM CP_Person;                                                               ";

            return sql;
        }
    }
}
