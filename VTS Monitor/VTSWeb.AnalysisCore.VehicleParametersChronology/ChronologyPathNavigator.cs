using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWeb.AnalysisCore.VehicleParametersChronology
{
    public class ChronologyPathNavigator
    {
        private VehicleParametersChronology chronology;

        public ChronologyPathNavigator(VehicleParametersChronology chronology)
        {
            if (chronology == null)
            {
                throw new ArgumentException("chronology");
            }
            this.chronology = chronology;
        }

        public void CheckPathAndCreateIfNotExists(string path)
        {
            IList<string> folders = path.Split('/');
            if (folders.Count == 0)
            {
                throw new Exception();
            }
            IList<VehicleChronologicalParametersGroup> currentGroupsCollection =
                chronology.Groups;
            for (int i = 0; i < folders.Count; i++)
            {
                VehicleChronologicalParametersGroup group =
                    currentGroupsCollection.FirstOrDefault(g =>
                        g.GroupNameKey == folders[i]);
                if (group == null) // no group yet
                {
                    currentGroupsCollection.Add(
                        new VehicleChronologicalParametersGroup(folders[i]));
                }
                currentGroupsCollection = currentGroupsCollection.First(
                    g => g.GroupNameKey == folders[i]).Groups;
            }
        }

        public void PlaceDataByPath(string path, 
            VehicleChronologicalParameter parameter)
        {
            CheckPathAndCreateIfNotExists(path);
            IList<string> folders = path.Split('/');
            if (folders.Count == 0)
            {
                throw new Exception();
            }
            IList<VehicleChronologicalParametersGroup> currentGroupsCollection =
                chronology.Groups;
            for (int i = 0; i < folders.Count; i++)
            {
                VehicleChronologicalParametersGroup group =
                    currentGroupsCollection.FirstOrDefault(g =>
                        g.GroupNameKey == folders[i]);
                if (group == null) // no group yet
                {
                    throw new Exception("Group should be already here.");
                }
                if (i == folders.Count - 1) // we are at destination
                {
                    VehicleChronologicalParameter param = group.Parameters.
                        FirstOrDefault(p => p.Type == parameter.Type);
                    if (param == null)
                    {
                        group.Parameters.Add(parameter);
                    }
                    else
                    {
                         param.Assimilate(parameter);
                    }
                }
                currentGroupsCollection = group.Groups;
            }
        }
    }
}
