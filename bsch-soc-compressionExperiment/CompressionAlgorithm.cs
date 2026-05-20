using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompressionExperiment
{
    /// <summary>
    /// Contains compression algorithm implementations.
    ///
    /// RLE is supplied as the baseline algorithm.
    /// Your task is to implement fixed length encoding in the stub below.
    ///
    /// Cost model:
    ///   RLE:           16 bits per run (8 bits for count + 8 bits for character)
    ///   Fixed length:  input length * bits_per_character
    /// </summary>
    public static class CompressionAlgorithm
    {
        // RLE cost model constant: bits per run.
        public const int BitsPerRun = 16;

        // ---------------------------------------------------------------
        // RLE — supplied implementation
        // ---------------------------------------------------------------

        /// <summary>
        /// Compresses input using run length encoding.
        /// Returns the encoded string in human-readable form (e.g. "4A3B2C"),
        /// the number of runs produced, and the compressed size in bits
        /// using the fixed cost model of 16 bits per run.
        /// </summary>
        public static (string encoded, int runCount, int compressedSizeBits) RLECompress(string input)
        {
            if (string.IsNullOrEmpty(input))
                return ("", 0, 0);

            var encoded = new StringBuilder();
            int runCount = 0;
            int i = 0;

            while (i < input.Length)
            {
                char current = input[i];
                int count = 1;

                while (i + count < input.Length && input[i + count] == current)
                    count++;

                encoded.Append(count);
                encoded.Append(current);
                runCount++;
                i += count;
            }

            int compressedSizeBits = runCount * BitsPerRun;
            return (encoded.ToString(), runCount, compressedSizeBits);
        }

        /// <summary>
        /// Decompresses an RLE encoded string.
        /// </summary>
        public static string RLEDecompress(string encoded)
        {
            var result = new StringBuilder();
            int i = 0;

            while (i < encoded.Length)
            {
                // Read the count digits.
                int countStart = i;
                while (i < encoded.Length && char.IsDigit(encoded[i]))
                    i++;

                int count = int.Parse(encoded.Substring(countStart, i - countStart));
                char character = encoded[i];
                i++;

                result.Append(character, count);
            }

            return result.ToString();
        }

        // ---------------------------------------------------------------
        // Fixed Length Encoding — your implementation goes here
        // ---------------------------------------------------------------

        /// <summary>
        /// Compresses input using fixed length binary encoding.
        ///
        /// Every character in the input alphabet is assigned a unique binary
        /// code of equal length. The number of bits per character is the
        /// smallest k such that 2^k is greater than or equal to the number
        /// of distinct characters in the input.
        ///
        /// Returns the encoded binary string, the code table used, the number
        /// of bits per character, and the compressed size in bits.
        ///
        /// Compressed size in bits = input length * bits per character.
        ///
        /// See the fixed length encoding tutorial articles for implementation
        /// guidance including how to calculate bits per character and how to
        /// build the code table.
        /// </summary>
        public static (string encoded, Dictionary<char, string> codeTable, int bitsPerChar, int compressedSizeBits)
            FixedLengthCompress(string input)
        {
            // TODO: Implement fixed length encoding.
            throw new NotImplementedException("Implement fixed length encoding here.");
        }

        /// <summary>
        /// Decompresses a fixed length encoded binary string using the provided code table.
        /// </summary>
        public static string FixedLengthDecompress(string encoded, Dictionary<char, string> codeTable, int bitsPerChar)
        {
            // TODO: Implement fixed length decoding.
            throw new NotImplementedException("Implement fixed length decoding here.");
        }
    }
}