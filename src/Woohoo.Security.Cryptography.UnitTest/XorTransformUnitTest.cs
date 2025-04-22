// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.UnitTest;

using Woohoo.Security.Cryptography;

public class XorTransformUnitTest
{
    private static readonly byte[] XorKey = [0x41, 0x76, 0x69, 0x73, 0x20, 0x44, 0x75, 0x72, 0x67, 0x61, 0x6e];
    private static readonly byte[] DecryptedData = [0x00, 0x00, 0x05, 0x01, 0x02, 0x03, 0x01, 0x02, 0x03, 0x05, 0x00, 0x00];
    private static readonly byte[] EncryptedData = [0x41, 0x76, 0x6c, 0x72, 0x22, 0x47, 0x74, 0x70, 0x64, 0x64, 0x6e, 0x41];

    [Fact]
    public void Encrypt()
    {
        using (var transform = new XorTransform(XorKey))
        {
            var actual = transform.TransformFinalBlock(DecryptedData, 0, DecryptedData.Length);
            actual.Should().BeEquivalentTo(EncryptedData);
        }
    }

    [Fact]
    public void Decrypt()
    {
        using (var transform = new XorTransform(XorKey))
        {
            var actual = transform.TransformFinalBlock(EncryptedData, 0, EncryptedData.Length);
            actual.Should().BeEquivalentTo(DecryptedData);
        }
    }
}
