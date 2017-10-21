using System;

namespace Common.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// User's unique ID.
        /// </summary>
        long Id
        {
            get;
            set;
        }

        /// <summary>
        /// User's name.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Defines whether this user can manage records.
        /// </summary>
        bool IsAdministrator
        {
            get;
            set;
        }
    }
}
