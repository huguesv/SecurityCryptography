// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography;

using System;
using System.Security.Cryptography;

/// <summary>
/// Hash algorithm factory class.
/// </summary>
public static class HashAlgorithmFactory
{
    /// <summary>
    /// Create the specified hash algorithm.
    /// </summary>
    /// <param name="hashName">Name of algorithm (case-insensitive).</param>
    /// <returns>Hash algorithm.</returns>
    public static HashAlgorithm Create(string hashName)
    {
        Requires.NotNullOrEmpty(hashName);

        if (string.Equals(hashName, "crc32", StringComparison.OrdinalIgnoreCase))
        {
            return CRC32.Create();
        }
        else if (string.Equals(hashName, "md5", StringComparison.OrdinalIgnoreCase))
        {
            return MD5.Create();
        }
        else if (string.Equals(hashName, "sha1", StringComparison.OrdinalIgnoreCase))
        {
            return SHA1.Create();
        }
        else if (string.Equals(hashName, "sha256", StringComparison.OrdinalIgnoreCase))
        {
            return SHA256.Create();
        }
        else if (string.Equals(hashName, "sha384", StringComparison.OrdinalIgnoreCase))
        {
            return SHA384.Create();
        }
        else if (string.Equals(hashName, "sha512", StringComparison.OrdinalIgnoreCase))
        {
            return SHA512.Create();
        }

        throw new NotSupportedException($"Algorithm not supported: {hashName}");
    }
}
