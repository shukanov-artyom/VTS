using System;
using System.Collections.Generic;

namespace Measurements
{
    internal class Activity
    {
        private const int maxMapsHistory = 10;
        private readonly IList<ExecutionMap> maps = new List<ExecutionMap>();
        private ExecutionMap currentExecutionMap;

        private readonly string name;
        private bool complete;

        public Activity(string name, DateTime startTime)
        {
            this.name = name;
            currentExecutionMap = new ExecutionMap();
            currentExecutionMap.Start(name, String.Empty, startTime);
        }

        public bool Complete
        {
            get
            {
                return complete;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public IList<ExecutionMap> Maps
        {
            get
            {
                return maps;
            }
        }

        public void Finish(DateTime finishTime)
        {
            if (Complete)
            {
                throw new NotSupportedException(String.Format("Cannot finish already completed activity {0}", Name));
            }
            complete = true;
            currentExecutionMap.Finish(name, finishTime);
            maps.Add(currentExecutionMap);
            if (maps.Count > maxMapsHistory)
            {
                maps.RemoveAt(0);
            }
            currentExecutionMap = null;
        }

        public void Restart(DateTime restartTime)
        {
            if (!Complete)
            {
                throw new NotSupportedException(String.Format("Cannot restart an activity {0} which is not complete yet.", Name));
            }
            if (currentExecutionMap != null)
            {
                throw new NotSupportedException("Current execution map must be null when restarting an activity.");
            }
            ExecutionMap newCurrent = new ExecutionMap();
            newCurrent.Start(name, String.Empty, restartTime);
            currentExecutionMap = newCurrent;
            complete = false;
        }

        public void StartSub(string subActivityName, string parentName, DateTime startTime)
        {
            currentExecutionMap.Start(subActivityName, parentName, startTime);
        }

        public void FinishSub(string subActivityName, DateTime finishTime)
        {
            currentExecutionMap.Finish(subActivityName, finishTime);
        }
    }
}
