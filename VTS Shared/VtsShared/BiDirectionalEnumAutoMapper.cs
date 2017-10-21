using System;

namespace VTS.Shared
{
    /// <summary>
    /// Automatically maps one enumeration to another.
    /// Used for converting DTO to enum and back.
    /// </summary>
    public class BiDirectionalEnumAutoMapper<TSourceEnum, TTargetEnum> 
        where TSourceEnum : struct
        where TTargetEnum : struct
    {
    }
}
