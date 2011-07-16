using System;
using Iesi.Collections.Generic;
using Microsoft.SqlServer.Types;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class ReceiveSession : UpdatableWithPacketObject
    {

        protected ReceiveSession()
        {
            Images = new HashedSet<Image>();
        }

        public ReceiveSession(long stationCatalogNumber, long satteliteCatalogNumber)
            : this()
        {
            StationCatalogNumber = stationCatalogNumber;
            SatelliteCatalogNumber = satteliteCatalogNumber;
        }

        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual long SatelliteCatalogNumber { get; set; }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual long StationCatalogNumber { get; set; }

        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual DateTime StartTime { get; set; }

        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual DateTime EndTime { get; set; }
        public virtual TimeSpan TimeSpan
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        public virtual ISet<Image> Images { get; protected set; }

        public virtual SqlGeography Coor { get; set; }
    }
}
