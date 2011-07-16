using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class RecivePlanFile
    {
        public IEnumerable<ReceivePlanTask> Parse(Stream src)
        {
            var list = new List<ReceivePlanTask>();
            try
            {
                var doc = XDocument.Load(src);

                foreach (var plan in doc.Descendants(@"ReceivePlan"))
                {
                    var plan_id = long.Parse(plan.Attribute(@"ID").Value);
                    foreach (var session in plan.Descendants(@"Session"))
                    {
                        try
                        {
                            list.Add(new ReceivePlanTask(plan_id, session));
                        }
                        catch
                        {
                            // неверный формат сессий
                        }
                    }
                }
            }
            catch
            {
                // неверный формат файла
                return null;
            }

            return list;
        }


		public string Write(IEnumerable<ReceivePlanTask> list, long? plan_id = null)
		{
			return CreateXML(list, plan_id).ToString();
		}
		public void Write(IEnumerable<ReceivePlanTask> list, Stream stream, long? plan_id = null)
		{
			CreateXML(list, plan_id).Save(stream);
		}

		XElement CreateXML(IEnumerable<ReceivePlanTask> list, long? plan_id = null)
        {
            var plan = new XElement(@"ReceivePlan");
			if (plan_id.HasValue) plan.SetAttributeValue("ID", plan_id);

            foreach (var item in list)
            {
				var session = item.XBody;
				session.SetAttributeValue("status", item.Status);
                plan.Add(session);
            }

            return plan;
        }

    }
}
