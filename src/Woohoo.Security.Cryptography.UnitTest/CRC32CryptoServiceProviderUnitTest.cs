// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.UnitTest;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Woohoo.Security.Cryptography;

[TestClass]
public class CRC32CryptoServiceProviderUnitTest
{
    [TestMethod]
    public void Create()
    {
        var actual = CRC32CryptoServiceProvider.Create();
        _ = actual.Should().NotBeNull();
    }
}
