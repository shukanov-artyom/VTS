using System;
using System.Linq;
using System.Xml.Linq;
using Agent.Connector.PSA.Refactor.Peugeot;

namespace Agent.Connector.PSA
{
    public abstract class PsaTraceFactoryBase
    {
        protected virtual DateTime GetFirstCardDateTime(XDocument doc)
        {
            try
            {
                XElement element = doc.Root.Elements(PeugeotTraceStrings.Command).FirstOrDefault();
                XElement card = element.Elements(PeugeotTraceStrings.Card).FirstOrDefault();
                string dateTimeString = card.Attribute(PeugeotTraceStrings.DateHour).Value;
                return DateTime.Parse(dateTimeString);
            }
            catch (NullReferenceException)
            {
                return DateTime.MinValue;
            }
        }
    }
}
