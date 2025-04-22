// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.UnitTest;

using Woohoo.Security.Cryptography;

public class CRC32CryptoServiceProviderUnitTest
{
    [Fact]
    public void Create()
    {
        var actual = CRC32CryptoServiceProvider.Create();
        _ = actual.Should().NotBeNull();
    }
}
