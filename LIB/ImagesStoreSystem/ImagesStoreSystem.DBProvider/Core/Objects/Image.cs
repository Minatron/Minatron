using System;
using System.Diagnostics;
using Iesi.Collections.Generic;
using Microsoft.SqlServer.Types;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class Image : UpdatableWithPacketObject
    {
        public Image()
        {
            Time = DateTime.UtcNow;
            Attributes = new HashedSet<ImageAttribute>();
        }

        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual DateTime Time { get; set; }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual ImageLevel Level { get; set; }

        public virtual SatelliteSensor Sensor { get; set; }
        public virtual DateTime? SurveyTime { get; set; }
        public virtual ReceiveSession ReceiveSession { get; protected set; }
        public virtual long? SatelliteCatalogNumber { get; set; }

        public virtual SqlGeography Polygon { get; set; }
        public virtual float Cloudiness { get; set; }

        public virtual void SetReceiveSession(ReceiveSession session)
        {
            if (session == null) throw new ArgumentNullException("session");
            if (session != ReceiveSession)
            {
                UnsetReceiveSession();
                ReceiveSession = session;
                Debug.Assert(!ReceiveSession.Images.Contains(this), "Impossible !!!");
                ReceiveSession.Images.Add(this);

            }
        }

        public virtual void UnsetReceiveSession()
        {
            if (ReceiveSession != null)
            {
                ReceiveSession.Images.Remove(this);
                ReceiveSession = null;
            }
        }

        public virtual ISet<ImageAttribute> Attributes { get; protected set; }




    }
}
