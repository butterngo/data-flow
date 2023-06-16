using System;

namespace DataEngine.Abstraction
{
    [Flags]
    public enum JsonObjectTypeEnum
    {
        /// <summary>
        /// none
        /// </summary>
        None = 0,

        /// <summary>
        /// array
        /// </summary>
        Array = 1,

        /// <summary>
        /// boolean
        /// </summary>
        Boolean = 2,

        /// <summary>
        /// integer
        /// </summary>
        Integer = 4,

        /// <summary>
        /// null
        /// </summary>
        Null = 5,

        /// <summary>
        /// number
        /// </summary>
        Number = 6,

        /// <summary>
        /// object
        /// </summary>
        Object = 7,

        /// <summary>
        /// string
        /// </summary>
        String = 8,

        /// <summary>
        /// date-time
        /// </summary>
        DateTime = 9,

        /// <summary>
        /// date
        /// </summary>
        Date = 10,

        /// <summary>
        /// time
        /// </summary>
        Time = 11,
    }
}
