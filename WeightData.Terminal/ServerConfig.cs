namespace Band.WeightData.Terminal
{
    public class ServerConfig
    {
        public int    ListenPort = 502; //502
        public string Name = null;
        public string DBConnectionString = null;

        public ServerConfig(string[] args)
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
                        case "db":
                            DBConnectionString = param[1];
                            break;
                        case "name":
                            Name = param[1];
                            break;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(Name)) Name = ListenPort.ToString();
        }
    }
}
