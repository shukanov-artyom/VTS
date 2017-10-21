using System;
using System.Collections.Generic;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    public class LexiaScreen
    {
        private IList<LexiaRawParameterPoint> points = 
            new List<LexiaRawParameterPoint>();

        public IList<LexiaRawParameterPoint> Points
        {
            get
            {
                return points;
            }
        }

        public string Name
        {
            get;
            set;
        }
    }
}
