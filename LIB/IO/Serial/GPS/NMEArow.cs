using System;
using System.Threading;
using System.Globalization;

namespace Lacross.IO.GPS
{

    public class NMEArow 
    {
        public  string Name;
        protected byte m_baseCS;
        protected bool m_valid = false;
        protected Mutex m_access = new Mutex();
        protected byte[] m_row = null;
        protected int m_Count = 0;
        protected int[] m_comms = null;
        protected int m_CommsCount = 0;
        internal  GPS Owner =null;

        #region Constructor
        public NMEArow()
        {
            Init(255, "",0);
        }
        public NMEArow(string name)
        {
            Init(255, name,0);
        }
        public NMEArow(string name,int coutComms)
        {
            Init(255, name, coutComms);
        }
        public NMEArow(int maxsize)
        {
            Init(maxsize, "",0);
        }
        public NMEArow(int maxsize, string name)
        {
            Init(maxsize, name,0);
        }
        public NMEArow(int maxsize, string name, int coutComms)
        {
            Init(maxsize, name, coutComms);
        }
        protected void Init(int maxsize, string name, int coutComms)
        {
            Name = name;
            m_baseCS = 0;
            if (name.Length>0)
            {
                byte[] b = System.Text.Encoding.Default.GetBytes(name);
                for (int i = 0; i < b.Length; i++) m_baseCS ^= b[i];
            }            
            m_row = new byte[maxsize];
            if (coutComms>0)  m_comms = new int[coutComms];
        }
        #endregion 

        byte CalculateCS()
        {
            byte res = m_baseCS;
            for (int i = 0; i < m_Count; i++) res ^= m_row[i];
            return res;
        }

        public string Value
        {
            get
            {
                string res;
                m_access.WaitOne();
                string cs = CalculateCS().ToString("X");
                if (cs.Length == 1) cs = "*0" + cs; else cs = "*" + cs;
                if (m_valid) res = "$" + Name + System.Text.Encoding.Default.GetString(m_row, 0, m_Count) + cs;
                else res = "";
                m_access.ReleaseMutex();
                return res;
            }
        }
        public bool IsValid
        {
            get
            {
                m_access.WaitOne();
                bool res = m_valid;
                m_access.ReleaseMutex();
                return res;
            }
        }
        public virtual bool CheckCS(byte cs)
        {
            m_access.WaitOne();
            m_valid = (CalculateCS() == cs);
            m_access.ReleaseMutex();
            return m_valid;
        }

        public void ResetValue()
        {            
            m_access.WaitOne();
            m_Count = 0;
            m_valid = false;
            m_CommsCount = 0;
            m_access.ReleaseMutex();
        }
        public bool AddByte(byte b)
        {
            bool res = false;
            m_access.WaitOne();
            if (m_Count != m_row.Length)
            {
                m_row[m_Count] = b;
                if (m_comms != null)
                {
                    if (b == (byte)NMEAChars.Comma)
                    {
                        if (m_CommsCount < m_comms.Length)
                        {
                            m_comms[m_CommsCount] = m_Count;
                            m_CommsCount++;
                        }
                    }
                    
                }
                m_Count++;
                res = true;
            }
            m_access.ReleaseMutex();
            return res;
        }

        public override string ToString()
        {
            string res;
            m_access.WaitOne();
            res = System.Text.Encoding.Default.GetString(m_row, 0, m_Count);
            m_access.ReleaseMutex();
            return res;      
        }
        public string ToString2
        {
            get
            {
                string res;
                m_access.WaitOne();
                res = System.Text.Encoding.Default.GetString(m_row, 0, m_Count);
                m_access.ReleaseMutex();
                return res;
            }
        }


        private bool GetStarCountOfParam(int nParam, out int start, out int count)
        {
            start = 0;
            count = 0;
            if ((m_comms != null)&&(nParam < m_CommsCount))
            {
               start = m_comms[nParam] + 1;
               nParam++;
               if (nParam == m_CommsCount)
               {
                  count = m_Count - start;
               }
               else
               {
                  count = m_comms[nParam] - start;
               }
               if (count > 0) return true;         
            }
            return false;
        }

        public byte GetCharParam(int nParam)
        {
            byte res=0;
            int start,count;
            m_access.WaitOne();
            if (GetStarCountOfParam(nParam,out start,out count))
            {
                res = m_row[start];
            }
            m_access.ReleaseMutex();
            return res;
        }
        public string GetStringParam(int nParam)
        {
            string res = "";
            int start, count;
            m_access.WaitOne();
            if (GetStarCountOfParam(nParam, out start, out count))
            {
                res = System.Text.Encoding.Default.GetString(m_row, start, count);
            }
            m_access.ReleaseMutex();
            return res;
        }
        public int GetIntParam(int nParam)
        {
            string r = GetStringParam(nParam);
            if (r == "") return 0;
            int res=0;
            try
            {
                res = int.Parse(r);
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        public float GetFloatParam(int nParam)
        {
            string r = GetStringParam(nParam);            
            if (r == "") return 0;
            string dcs = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            float res = 0;
            try
            {
                if (dcs != ".") r = r.Replace(".", dcs);
                res = float.Parse(r);
            }
            catch
            {
                res = 0;
            }
            return res;
        }
        public float GetLatitudeParam(int nParam)
        {
            string r = GetStringParam(nParam);
            if (r == "") return 0;
            string dcs = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            float res = 0;
            try
            {
                res = float.Parse(r.Substring(0, 2));
                if (dcs != ".") r = r.Replace(".", dcs);
                res += float.Parse(r.Substring(2))/60;
            }
            catch
            {
                res = 0;
            }
            return res;
        }
        public float GetLongitudeParam(int nParam)
        {
            string r = GetStringParam(nParam);
            if (r == "") return 0;
            string dcs = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            float res = 0;
            try
            {
                res = float.Parse(r.Substring(0, 3));
                if (dcs != ".") r = r.Replace(".", dcs);
                res += float.Parse(r.Substring(3))/60;
            }
            catch
            {
                res = 0;
            }
            return res;
        }
        public DateTime GetTimeParam(int nParam)
        {
            DateTime res = DateTime.MinValue;
            int start, count;
            m_access.WaitOne();
            if (GetStarCountOfParam(nParam, out start, out count))
            {
                try
                {
                    res = res.AddHours(int.Parse(System.Text.Encoding.Default.GetString(m_row, start, 2)));
                    res = res.AddMinutes(int.Parse(System.Text.Encoding.Default.GetString(m_row, start + 2, 2)));
                    res = res.AddSeconds(int.Parse(System.Text.Encoding.Default.GetString(m_row, start + 4, 2)));
                }
                catch
                {
                    res= DateTime.MinValue;
                }
            }            
            m_access.ReleaseMutex();
            return res;
        }
        public DateTime GetDateParam(int nParam)
        {
            DateTime res = DateTime.MinValue;
            int start, count;
            m_access.WaitOne();
            if (GetStarCountOfParam(nParam, out start, out count))
            {
                try
                {
                    res = new DateTime(2000 + int.Parse(System.Text.Encoding.Default.GetString(m_row, start + 4, 2)),
                                      int.Parse(System.Text.Encoding.Default.GetString(m_row, start + 2, 2)),
                                      int.Parse(System.Text.Encoding.Default.GetString(m_row, start, 2)));
                }
                catch
                {
                    res = DateTime.MinValue;
                }
            }
            m_access.ReleaseMutex();
            return res;
        }

    }

    public class GPRMCrow : NMEArow
    {
        public GPRMCrow()
        {
            Init(255, "GPRMC",11);
        }
       
        public override bool CheckCS(byte cs)
        {
            if (base.CheckCS(cs))
            {
                if (Owner != null)
                {
                    Owner.m_access.WaitOne();
                    Owner.m_valid = GetCharParam(1) == 0x41;
                    Owner.m_azimut = GetFloatParam(7);
                    Owner.m_latitude = GetLatitudeParam(2);
                    if (GetCharParam(3) == 0x53) Owner.m_latitude *= -1;
                    Owner.m_longitude = GetLongitudeParam(4);
                    if (GetCharParam(5) == 0x57) Owner.m_longitude *= -1;
                    Owner.m_speed = GetFloatParam(6);
                    DateTime date = GetDateParam(8);
                    DateTime time = GetTimeParam(0);
                    Owner.m_utcdatetime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
                    Owner.m_access.ReleaseMutex();         
                }
                return true;
            }
            else return false;
        }
    }
    public class GPGGArow : NMEArow
    {
        public GPGGArow()
        {
            Init(255, "GPGGA", 14);
        }

        public override bool CheckCS(byte cs)
        {
            if (base.CheckCS(cs))
            {
                if (Owner != null)
                {
                    Owner.m_access.WaitOne();
                    DateTime time = GetTimeParam(0);
                    Owner.m_utcdatetime = new DateTime(Owner.m_utcdatetime.Year, Owner.m_utcdatetime.Month, Owner.m_utcdatetime.Day, time.Hour, time.Minute, time.Second);
                    Owner.m_latitude = GetLatitudeParam(1);
                    if (GetCharParam(2) == 0x53) Owner.m_latitude *= -1;
                    Owner.m_longitude = GetLongitudeParam(3);
                    if (GetCharParam(4) == 0x57) Owner.m_longitude *= -1;
                    Owner.m_singnalquality = GetIntParam(5);
                    Owner.m_satellitecount = GetIntParam(6);
                    Owner.m_hdop = GetFloatParam(7);
                    Owner.m_altitude = GetFloatParam(8);
                    Owner.m_altitude += GetFloatParam(10);                        
                    Owner.m_access.ReleaseMutex();
                    
                }
                return true;
            }
            else return false;
        }
    }
    public class GPGLLrow : NMEArow
    {
        public GPGLLrow()
        {
            Init(255, "GPGLL", 6);
        }
    }
    public class GPGSArow : NMEArow
    {
        public GPGSArow()
        {
            Init(255, "GPGSA", 17);
        }
        public override bool CheckCS(byte cs)
        {
            if (base.CheckCS(cs))
            {
                if (Owner != null)
                {
                    Owner.m_access.WaitOne();
                    Owner.m_mode = GetIntParam(1);
                    for (int i = 0; i < 12; i++) 
                            Owner.m_currentsatelitesPRN[i] = GetIntParam(i + 2);
                    Owner.m_access.ReleaseMutex();
                }
                return true;
            }
            else return false;
        }
    }
    public class GPGSVrow : NMEArow
    {
        public GPGSVrow()
        {
            Init(255, "GPGSV", 19);
        }
        public override bool CheckCS(byte cs)
        {
            if (base.CheckCS(cs))
            {
                if (Owner != null)
                {
                    Owner.m_access.WaitOne();

                    int prn;
                    SatelliteInfo info;
                    object old;
                    for (int i = 0; i < 4; i++)
                    {
                        prn = GetIntParam(i*4 + 3);
                        if (prn !=0)
                        {
                            info = new SatelliteInfo(GetIntParam(i * 4 + 6), GetIntParam(i * 4 + 4), GetIntParam(i * 4 + 5));
                            old = Owner.m_satellites[prn];
                            if (old != null)
                            {
                                old = info;
                            }
                            else
                            {
                                Owner.m_satellites.Add(prn, info);
                            }
                        }
                    }
                    Owner.m_access.ReleaseMutex();
                }
                return true;
            }
            else return false;
        }
    }
    public class GPVTGrow : NMEArow
    {
        public GPVTGrow()
        {
            Init(255, "GPVTG", 10);
        }
    }
    public class GPZDArow : NMEArow
    {
        public GPZDArow()
        {
            Init(255, "GPZDA", 6);
        }
    }
}
