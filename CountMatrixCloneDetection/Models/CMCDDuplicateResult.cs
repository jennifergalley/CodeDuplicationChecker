namespace CountMatrixCloneDetection
{
    /// <summary>
    /// Class to represent a CMDC duplicate result
    /// </summary>
    public class CMCDDuplicateResult
    {
        public CMCDMethodInfo MethodA { get; set; }

        public CMCDMethodInfo MethodB { get; set; }

        public double Score { get; set; }
    }
}
