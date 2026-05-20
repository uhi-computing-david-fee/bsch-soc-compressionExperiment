using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CompressionExperiment
{
    /// <summary>
    /// Writes compression experiment results to a CSV file.
    /// Results are appended if the file already exists.
    /// </summary>
    public class ResultReporter
    {
        private readonly string _outputFilePath;

        public ResultReporter(string outputFilePath)
        {
            _outputFilePath = outputFilePath;
            EnsureHeaderExists();
        }

        public void Write(IEnumerable<CompressionResult> results)
        {
            var sb = new StringBuilder();

            foreach (var r in results)
            {
                sb.AppendLine(
                    $"{r.Scheme}," +
                    $"{r.InputDescription}," +
                    $"{r.InputLength}," +
                    $"{r.DistinctCharacters}," +
                    $"{r.OriginalSizeBits}," +
                    $"{r.CompressedSizeBits}," +
                    $"{r.CompressionRatio:F4}," +
                    $"{r.RunCount?.ToString() ?? "N/A"}"
                );
            }

            File.AppendAllText(_outputFilePath, sb.ToString());
        }

        private void EnsureHeaderExists()
        {
            if (!File.Exists(_outputFilePath))
            {
                File.WriteAllText(_outputFilePath,
                    "Scheme,InputDescription,InputLength,DistinctCharacters," +
                    "OriginalSizeBits,CompressedSizeBits,CompressionRatio,RunCount\n");
            }
        }
    }
}