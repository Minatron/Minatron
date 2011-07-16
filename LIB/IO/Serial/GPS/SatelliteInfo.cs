using System;

namespace Lacross.IO.GPS
{
	public class SatelliteInfo
	{	
		/// <summary>
		/// Отношение сигнал - шум
		/// </summary>
		public int	SNR;			
		/// <summary>
		/// Угол наклона 
		/// </summary>
		public int	Elevation;	
		/// <summary>
		/// Азимут
		/// </summary>
		public int	Azimuth;
        public SatelliteInfo()
        {
            SNR = 0;
            Elevation = 0;
            Azimuth = 0;
        }
        public SatelliteInfo(SatelliteInfo o)
        {
            SNR = o.SNR;
            Elevation = o.Elevation;
            Azimuth = o.Azimuth;
        }
        public SatelliteInfo(int snr,int elevation,int azimuth)
        {
            SNR = snr;
            Elevation = elevation;
            Azimuth = azimuth;
        }
	}
}
