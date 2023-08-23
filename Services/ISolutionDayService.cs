namespace AdventOfCode.Services
{
    public interface ISolutionDayService
    {
        /// <summary>
        /// Execute this day's first half
        /// </summary>
        /// <param name="example"></param>
        /// <returns></returns>
        string FirstHalf(bool example);

        /// <summary>
        /// Execute this day's second half
        /// </summary>
        /// <param name="example"></param>
        /// <returns></returns>
        string SecondHalf(bool example);
    }
}