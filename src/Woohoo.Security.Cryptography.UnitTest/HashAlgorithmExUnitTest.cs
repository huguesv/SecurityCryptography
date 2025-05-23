﻿// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.UnitTest;

using Woohoo.Security.Cryptography;

public class HashAlgorithmExUnitTest
{
    [Fact]
    public void Create()
    {
        var actual = HashAlgorithmFactory.Create("CRC32");
        _ = actual.Should().NotBeNull();
    }
}
