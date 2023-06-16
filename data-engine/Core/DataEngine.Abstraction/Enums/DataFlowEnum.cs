namespace DataEngine.Abstraction
{
    public enum ConditionEnum
    {
        /// <summary>
        /// equal
        /// </summary>
        Equal = 1,

        /// <summary>
        /// not_equal
        /// </summary>
        NotEqual = 2
    }

    public enum DataBlockProcessorStatusEnum 
    {
        Unspecified = 1,
        Produced = 2,
        Completed = 3
    }
}
