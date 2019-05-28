namespace Models
{
    /// <summary>
    /// Class to represent a CMDC duplicate result
    /// </summary>
    public class DuplicateResult
    {
        public MethodInfo MethodA { get; set; }

        public MethodInfo MethodB { get; set; }

        public double Score { get; set; }
    }
}
