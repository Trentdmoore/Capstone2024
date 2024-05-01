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


        public string InsertPersonInfo()
        {
            string sql =
                @"INSERT INTO CP_Person (ID, FName, LName, Email, Title, CID, AccessCode)
                  VALUES (@ID, @FName, @LName, @Email, @Title, @CID, @AccessCode)";
            return sql;
        }

        public string GetAllAccessLogs()
        {
            string sql =
                @"SELECT ID, LockNum, AccessTime, CID, Accepted
                  FROM CP_AccessLog;                                ";

            return sql;
        }

        public string GetIndividualAccessLogs()
        {
            string sql =
                @"SELECT ID, LockNum, AccessTime, CID, Accepted
                  FROM CP_AccessLog
                  WHERE ID = @ID;                                ";

            return sql;
        }

        public string GetAllRequestLogs()
        {
            string sql =
                @"SELECT ID, LockNum, AccessCode, DateRequested, DateApproved, AdvisorID
                  FROM CP_RequestLog;                                ";

            return sql;
        }

        public string GetIndividualRequestLogs()
        {
            string sql =
                @"SELECT ID, LockNum, AccessCode, DateRequested, DateApproved, AdvisorID
                  FROM CP_RequestLog
                  WHERE ID = @ID;                                ";

            return sql;
        }

        public string UdatePersonInfo()
        {
            string sql =
                @"UPDATE CP_Person
                  SET FName =			@FName,
	                  LName =			@LName,
	                  Email =			@Email,
	                  Title =			@Title,
	                  CID =			    @CID,
	                  AccessCode =	    @AccessCode
                 WHERE ID = @ID;";
            return sql;
        }

        public string DeletePerson()
        {
            string sql =
                @"DELETE FROM CP_Person
                  WHERE ID = @ID;";
            return sql;
        }

        public string QuickStatistics()
        {
            string sql =
                @"SELECT P.ID, StartTime, EndTime,
                    (SELECT count(*)
                    FROM CP_AccessLog
                    WHERE Accepted = 'N'
					AND CP_AccessLog.ID = p.ID) as NumFailed,
                    (SELECT CASE WHEN EXISTS (
	                    SELECT *
	                    FROM CP_RequestLog
	                    WHERE Approved = 'N'
                    )
                    THEN 'Y'
                    ELSE 'N' END) as HasPendingRequest,
                    (Select count(*)
                    FROM CP_AccessLog
					WHERE CP_AccessLog.ID = p.ID) as NumAccessesLastHour
                    FROM CP_Person P INNER JOIN CP_AccessCodes AC
                    ON P.AccessCode = AC.AccessCode;";
            return sql;
        }
    }
}
