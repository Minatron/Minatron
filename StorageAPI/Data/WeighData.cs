namespace Band.Storage.Minatron.Data
{
    public enum CourseType
    {
        Moscow = 0,
        SaintPetersburg=1,
    }

    public class WeighData : Band.Storage.Core.RemovableObject
    {
        public virtual CourseType Course { get;  set; }
        public virtual System.DateTime WeighTime { get; set; }
	    public virtual float Weigh { get; set; }
        public virtual float AvgSpeed { get; set; }
    }
}
