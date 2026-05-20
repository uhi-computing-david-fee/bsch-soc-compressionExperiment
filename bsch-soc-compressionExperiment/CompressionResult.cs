namespace CompressionExperiment
{
    /// <summary>
    /// Holds the result of a single compression experiment run.
    /// RunCount is null for fixed length encoding since it does not produce runs.
    /// </summary>
    public class CompressionResult
    {
        public string Scheme { get; }
        public string InputDescription { get; }
        public int InputLength { get; }
        public int DistinctCharacters { get; }
        public int OriginalSizeBits { get; }
        public int CompressedSizeBits { get; }
        public double CompressionRatio { get; }
        public int? RunCount { get; }

        public CompressionResult(
            string scheme,
            string inputDescription,
            int inputLength,
            int distinctCharacters,
            int originalSizeBits,
            int compressedSizeBits,
            int? runCount)
        {
            Scheme = scheme;
            InputDescription = inputDescription;
            InputLength = inputLength;
            DistinctCharacters = distinctCharacters;
            OriginalSizeBits = originalSizeBits;
            CompressedSizeBits = compressedSizeBits;
            CompressionRatio = compressedSizeBits > 0
                ? (double)originalSizeBits / compressedSizeBits
                : 0;
            RunCount = runCount;
        }
    }
}