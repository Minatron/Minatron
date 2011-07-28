using System;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Band.WeightData.Terminal
{
    public class DataBase
    {
        bool _isWork = false;
        readonly SqlConnection _connect;
        readonly SqlCommand _insert;
        public DataBase()
        {
            _connect = new SqlConnection(ServerConfig.DBConnectionString());
            _connect.Open();

            if (_connect.State == ConnectionState.Open)
            {
                _insert = new SqlCommand(@"INSERT INTO WeighData ([CourseID],[WeighTime],[Weigh],[AvgSpeed]) VALUES (@curse ,@time ,@weight ,@speed)", _connect);
                _insert.Parameters.Add("@curse", SqlDbType.Int);
                _insert.Parameters.Add("@time", SqlDbType.DateTime2, 7);
                _insert.Parameters.Add("@weight", SqlDbType.Float);
                _insert.Parameters.Add("@speed", SqlDbType.Float);

                _insert.Parameters["@curse"].Value = ServerConfig.Course;
                _isWork = true;
            }
            else
            {
                _connect.Close();
            }
            

        }

        public bool IsConnect
        {
            get { if (_isWork) return _connect.State == ConnectionState.Open; else return _isWork; }
        }

        public void Store(string packet)
        {
            try
            {
                string[] param = packet.Split(new char[] { ' ', '=' });
                DateTime d = DateTime.Parse(param[0]);

                _insert.Parameters["@time"].Value = (d > DateTime.Now)?d.AddDays(-1):d;
                _insert.Parameters["@weight"].Value = float.Parse(param[2], new CultureInfo("en-US"));
                _insert.Parameters["@speed"].Value = float.Parse(param[4], new CultureInfo("en-US"));
                Logger.WriteMessage(Logger.EventID.StoreData, packet);
            }
            catch 
            {
                Logger.WriteMessage(Logger.EventID.ErrorData, packet);
                return;
            }

            _insert.ExecuteNonQuery();
        }

        public void Close()
        {
            if (_isWork)
            {
                _connect.Close();
                _isWork = false;
            }
        }
    }
}
