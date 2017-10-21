using System;

namespace VTS.Agent.Host
{
    public class HostInfo
    {
        private InstalledApplications installedApplications;

        public HostInfo()
        {
            installedApplications = new InstalledApplications();
        }

        public InstalledApplications InstalledApplications
        {
            get
            {
                return installedApplications;
            }
        }
    }
}
