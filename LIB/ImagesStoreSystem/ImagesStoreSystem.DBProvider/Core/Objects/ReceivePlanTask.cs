using System;
using System.Xml.Linq;
namespace ImagesStoreSystem.DBProvider.Core
{
    public enum PlanTaskStatus
    {
        Planned,
        Complete,
        Aborted,
        Skipped
    }

    public class ReceivePlanTask : UpdatableObject
    {
        protected ReceivePlanTask()
        {

        }

        public ReceivePlanTask(long packIdentity, XElement body)
            : this()
        {

            PackIdentity = packIdentity;
            SatelliteCatalogNumber = long.Parse(body.Element(XName.Get(@"Satellite")).Attribute(XName.Get(@"ID")).Value);
            StationCatalogNumber = long.Parse(body.Element(XName.Get(@"Station")).Attribute(XName.Get(@"ID")).Value);

            var trackingTime = body.Element(XName.Get(@"TrackingTime"));

            StartTime = DateTime.Parse(trackingTime.Attribute(XName.Get(@"start")).Value);
            EndTime = DateTime.Parse(trackingTime.Attribute(XName.Get(@"end")).Value);
            Aborted = false;
            Body = body.ToString();

        }

        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual long PackIdentity { get; protected set; }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual long SatelliteCatalogNumber { get; protected set; }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual long StationCatalogNumber { get; protected set; }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual DateTime StartTime { get; protected set; }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual DateTime EndTime { get; protected set; }
        public virtual TimeSpan TimeSpan
        {
            get
            {
                return EndTime - StartTime;
            }
        }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual bool Aborted { get; protected set; }

        public virtual string Body { get; protected set; }


        public virtual ReceiveSession ResultSession { get; set; }

        public virtual XElement XBody
        {
            get
            {
                return XElement.Parse(Body);
            }
        }

        public virtual PlanTaskStatus Status
        {
            get
            {
                if (ResultSession != null) return PlanTaskStatus.Complete;
                else if (Aborted) return PlanTaskStatus.Aborted;
                else if (StartTime < DateTime.UtcNow) return PlanTaskStatus.Skipped;
                return PlanTaskStatus.Planned;
            }
        }
    }
}
