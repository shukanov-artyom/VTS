using System;

namespace Agent.Workspace.EventArguments
{
    public class PsaTreeSelectionChangedEventArgs : EventArgs
    {
        private object argument;

        public PsaTreeSelectionChangedEventArgs(object argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException("argument");
            }
            this.argument = argument;
        }

        public object Arg
        {
            get
            {
                return argument;
            }
        }
    }
}
