namespace Band.Storage.Minatron
{
    public enum CourseType
    {
        Moscow = 0,
        SaintPetersburg=1,
    }

    public class WeighData : Band.Storage.Core.RemovableObject
    {
        public virtual CourseType Course { get; protected set; }
        public virtual System.DateTime WeighTime { get; protected set; }
	    public virtual float Weigh { get; protected set; }
        public virtual float AvgSpeed { get; protected set; }
    }
}
