using System;
using System.Collections.Generic;

namespace Agent.Connector.PSA.Refactor.Peugeot
{
    internal class PeugeotScreen
    {
        private IList<PeugeotRawParameterPoint> points = 
            new List<PeugeotRawParameterPoint>();

        public string Name
        {
            get;
            set;
        }

        public IList<PeugeotRawParameterPoint> Points 
        { 
            get
            {
                return points;
            }
        }
    }
}
