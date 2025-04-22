// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography;

using System.Security.Cryptography;

/// <summary>
/// Represents the abstract class from which all implementations of the
/// <see cref="CRC32"/> hash algorithm inherit.
/// </summary>
public abstract class CRC32 : HashAlgorithm
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CRC32"/> class.
    /// </summary>
    protected CRC32()
    {
    }

    /// <summary>
    /// Creates an instance of the default implementation of the
    /// <see cref="CRC32"/> hash algorithm.
    /// </summary>
    /// <returns>
    /// A new instance of the <see cref="CRC32"/>hash algorithm.
    /// </returns>
    public static new CRC32 Create()
    {
        return new CRC32CryptoServiceProvider();
    }

    /// <summary>
    /// Creates an instance of the specified implementation of the
    /// <see cref="CRC32"/>hash algorithm.
    /// </summary>
    /// <param name="hashName">
    /// The name of the specific implementation of <see cref="CRC32"/> to use.
    /// </param>
    /// <returns>
    /// A new instance of the specified implementation of <see cref="CRC32"/>.
    /// </returns>
    public static new CRC32 Create(string hashName)
    {
        return new CRC32CryptoServiceProvider();
    }
}
