namespace Band.WeightData.Terminal
{
    public class ServerConfig
    {
        public static int    ListenPort = 501; //502
        public static string Name = null;
        static string db_server = "(local)";
        static string db_name = "WeighDataBase";
        static string db_user = "sa";
        static string db_pwd = "";
        static bool db_sspi = true;
        public static int Course = 0;

        public static void ParseConfig(string[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                string[] param;
                for (int i = 0; i < args.Length; i++)
                {
                    param = args[i].Split('=');
                    switch (param[0])
                    {
                        case "port":
                            int.TryParse(param[1],out ListenPort);
                            break;
                        case "dbserver":
                            db_server = param[1];
                            break;
                        case "dbname":
                            db_name = param[1];
                            break;
                        case "dbuser":
                            db_user = param[1];
                            db_sspi = false;
                            break;
                        case "dbpwd":
                            db_pwd = param[1];
                            db_sspi = false;
                            break;
                        case "name":
                            Name = param[1];
                            break;
                        case "course":
                            int.TryParse(param[1], out Course);
                            break;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(Name)) Name = ListenPort.ToString();
        }

        public static string DBConnectionString()
        {
            return string.Format("server={0}; database={1}; {2}",
                    db_server, db_name, (db_sspi) ? @"Integrated Security=SSPI; MultipleActiveResultSets=True;" : string.Format(@"User ID={0}; Password={1}; Trusted_Connection=False; ", db_user, db_pwd));
        }
    }
}
