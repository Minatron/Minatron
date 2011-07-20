
namespace Band.Storage.Core
{
    public class LoginInfo
    {
        public string Server = @"server";
        public string DBName = @"WeighDataBase";
        public string UserID = @"sa";
        public string PWD = @"barlbarl";
        public bool SSPI = false;
        public string ConnectionString
        {
            get
            {
                return string.Format("server={0}; database={1}; {2}",
                    Server, DBName, (SSPI) ? @"Integrated Security=SSPI; MultipleActiveResultSets=True;" : string.Format(@"User ID={0}; Password={1}; Trusted_Connection=False; ", UserID, PWD));
            }
        }
    }
}
