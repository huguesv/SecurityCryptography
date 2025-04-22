// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.UnitTest;

using Woohoo.Security.Cryptography;

public class CRC32UnitTest
{
    [Fact]
    public void Create()
    {
        var actual = CRC32.Create();
        _ = actual.Should().NotBeNull();
    }

    [Fact]
    public void ComputeHash()
    {
        var data = new Tuple<byte[], byte[]>[]
        {
            Tuple.Create<byte[], byte[]>([0x00], [0xd2, 0x02, 0xef, 0x8d]),
            Tuple.Create<byte[], byte[]>([0x10, 0x20, 0x30, 0x40, 0x50], [0xb9, 0x89, 0x34, 0xc0]),
        };

        foreach (var current in data)
        {
            var algorithm = CRC32.Create();
            var actual = algorithm.ComputeHash(current.Item1);
            _ = actual.Should().BeEquivalentTo(current.Item2);
        }
    }
}
