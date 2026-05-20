using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompressionExperiment
{
    /// <summary>
    /// Runs compression experiments on a given input string or file,
    /// applying both RLE and fixed length encoding and recording results.
    /// </summary>
    public class ExperimentRunner
    {
        /// <summary>
        /// Runs both compression schemes on the provided input string.
        /// Returns one CompressionResult per scheme.
        /// </summary>
        public List<CompressionResult> Run(string input, string inputDescription)
        {
            var results = new List<CompressionResult>();

            int inputLength = input.Length;
            int distinctCharacters = input.Distinct().Count();
            int originalSizeBits = inputLength * 8;

            // --- RLE ---
            Console.WriteLine("  Running RLE...");
            var (rleEncoded, runCount, rleCompressedBits) = CompressionAlgorithm.RLECompress(input);
            string rleDecoded = CompressionAlgorithm.RLEDecompress(rleEncoded);

            if (rleDecoded != input)
                Console.WriteLine("  WARNING: RLE decode does not match original input.");

            results.Add(new CompressionResult(
                "RLE",
                inputDescription,
                inputLength,
                distinctCharacters,
                originalSizeBits,
                rleCompressedBits,
                runCount));

            Console.WriteLine($"  RLE complete. Runs: {runCount}, " +
                              $"Compressed: {rleCompressedBits} bits, " +
                              $"Ratio: {(double)originalSizeBits / rleCompressedBits:F4}");

            // --- Fixed Length Encoding ---
            Console.WriteLine("  Running Fixed Length Encoding...");

            try
            {
                var (flEncoded, codeTable, bitsPerChar, flCompressedBits) =
                    CompressionAlgorithm.FixedLengthCompress(input);

                string flDecoded = CompressionAlgorithm.FixedLengthDecompress(flEncoded, codeTable, bitsPerChar);

                if (flDecoded != input)
                    Console.WriteLine("  WARNING: Fixed length decode does not match original input.");

                results.Add(new CompressionResult(
                    "FixedLength",
                    inputDescription,
                    inputLength,
                    distinctCharacters,
                    originalSizeBits,
                    flCompressedBits,
                    null));

                Console.WriteLine($"  Fixed length complete. Bits per char: {bitsPerChar}, " +
                                  $"Compressed: {flCompressedBits} bits, " +
                                  $"Ratio: {(double)originalSizeBits / flCompressedBits:F4}");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("  Fixed length encoding not yet implemented.");
            }

            return results;
        }

        /// <summary>
        /// Loads a text file and runs both compression schemes on its contents.
        /// </summary>
        public List<CompressionResult> RunFromFile(string filePath, string inputDescription)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Input file not found: {filePath}");

            string input = File.ReadAllText(filePath);
            return Run(input, inputDescription);
        }
    }
}