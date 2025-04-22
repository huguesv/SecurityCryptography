// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.UnitTest;

using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Woohoo.Security.Cryptography;

[TestClass]
public class HashCalculatorUnitTest
{
    private static readonly byte[] InputData = { 0x10, 0x20, 0x30, 0x40, 0x50 };
    private static readonly byte[] ExpectedCrc32 = { 0xb9, 0x89, 0x34, 0xc0 };
    private static readonly byte[] ExpectedMd5 = { 0x07, 0xb2, 0x99, 0xbe, 0xab, 0x59, 0x87, 0xca, 0xe8, 0x02, 0x77, 0xd7, 0x66, 0x53, 0xb2, 0xbb };
    private static readonly byte[] ExpectedSha1 = { 0x70, 0x94, 0xfa, 0xd0, 0x09, 0x87, 0x92, 0xd6, 0xdf, 0xa0, 0xfc, 0x27, 0xc9, 0x91, 0x81, 0xd2, 0x3d, 0xe0, 0x2b, 0xd3 };

    [TestMethod]
    public void CalculateFromPath()
    {
        var path = "HashCalculatorData.bin";
        File.WriteAllBytes(path, InputData);

        var actual = new HashCalculator().Calculate(new string[] { "CRC32", "MD5", "SHA1" }, path);
        _ = actual.Should().NotBeNull();
        _ = actual.Checksums["CRC32"].Should().BeEquivalentTo(ExpectedCrc32);
        _ = actual.Checksums["MD5"].Should().BeEquivalentTo(ExpectedMd5);
        _ = actual.Checksums["SHA1"].Should().BeEquivalentTo(ExpectedSha1);
    }

    [TestMethod]
    public void CalculateFromStream()
    {
        var stream = new MemoryStream(InputData);
        stream.Flush();
        _ = stream.Seek(0, SeekOrigin.Begin);

        using (stream)
        {
            var actual = new HashCalculator().Calculate(new string[] { "CRC32", "MD5", "SHA1" }, stream, stream.Length);
            _ = actual.Should().NotBeNull();
            _ = actual.Checksums["CRC32"].Should().BeEquivalentTo(ExpectedCrc32);
            _ = actual.Checksums["MD5"].Should().BeEquivalentTo(ExpectedMd5);
            _ = actual.Checksums["SHA1"].Should().BeEquivalentTo(ExpectedSha1);
        }
    }

    [TestMethod]
    public void HexToStringTwoDigits()
    {
        var actual = HashCalculator.HexToString(new byte[] { 0x10, 0x20, 0x30, 0x40, 0xFF });
        _ = actual.Should().Be("10203040ff");
    }

    [TestMethod]
    public void HexToStringOneDigit()
    {
        var actual = HashCalculator.HexToString(new byte[] { 0x10, 0x20, 0x30, 0x40, 0x05 });
        _ = actual.Should().Be("1020304005");
    }
}
