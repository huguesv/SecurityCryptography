# Woohoo.Security.Cryptography

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/huguesv/SecurityCryptography/build-and-test.yml)
![NuGet Version](https://img.shields.io/nuget/v/Woohoo.Security.Cryptography)

Library of hash algorithms and utilities for .NET.

## CRC32

To use the CRC32 hash algorithm, you can create an instance of the `CRC32` class and call the `ComputeHash` method with your data.

```csharp
using Woohoo.Security.Cryptography;

HashAlgorithm algorithm = CRC32.Create();

byte[] data = [1, 2, 3, 4, 5];
byte[] hash = algorithm.ComputeHash(data);
```

## Hash Algorithm Factory

To create an instance of a hash algorithm by case-insensitive name, you can use the `HashAlgorithmFactory` class.

The following hash algorithms are supported: crc32, md5, sha1, sha256, sha384, sha512.

```csharp
using Woohoo.Security.Cryptography;

HashAlgorithm crc32 = HashAlgorithmFactory.Create("crc32");
HashAlgorithm md5 = HashAlgorithmFactory.Create("md5");
HashAlgorithm sha1 = HashAlgorithmFactory.Create("sha1");
```

## Hash Calculator

To compute multiple hash algorithms in one call, you can use the `Calculate` method on the `HashCalculator` class.

The hashes will be computed in parallel, and the results will be returned in a `HashCalculatorResult` object.

There are overloads to pass in either a stream or a file path.

```csharp
using Woohoo.Security.Cryptography;

string path = "path/to/your/file.txt";

HashCalculatorResult result = new HashCalculator().Calculate(["CRC32", "MD5", "SHA1"], path);
byte[] crc32Hash = result.Checksums["CRC32"];
byte[] md5Hash = result.Checksums["MD5"];
byte[] sha1Hash = result.Checksums["SHA1"];
```

To be notified of progress, subscribe to the `Progress` event before calling `Calculate`.

```csharp
using Woohoo.Security.Cryptography;

void ProgressHandler(object? sender, HashCalculatorProgressEventArgs ea)
{
    Console.WriteLine($"{ea.FilePath}: {ea.ProgressPercentage} percent done. {ea.ProgressBytes} of {ea.Length} bytes.");
}

string path = "path/to/your/file.txt";

HashCalculator calc = new HashCalculator();
calc.Progress += ProgressHandler;
calc.Calculate(["CRC32", "MD5", "SHA1"], path);
```

To abort a calculation, you can call the `Cancel` method on the `HashCalculator` instance.

To convert the byte array to a hexadecimal string, you can use the static `HexToString` method.

```csharp
using Woohoo.Security.Cryptography;

string crc32Hex = HashCalculator.HexToString(crc32Hash);
```

## XOR Transform

To encrypt/decrypt a byte array using a key, you can use the `XorTransform`
class, which implements the `System.Security.Cryptography.ICryptoTransform` interface.

```csharp
using Woohoo.Security.Cryptography;

byte[] key = [0x41, 0x76, 0x69, 0x73, 0x20, 0x44, 0x75, 0x72, 0x67, 0x61, 0x6e];
byte[] data = [0x00, 0x00, 0x05, 0x01, 0x02, 0x03, 0x01, 0x02, 0x03, 0x05, 0x00, 0x00];

using (var transform = new XorTransform(key))
{
    byte[] xor = transform.TransformFinalBlock(data, 0, data.Length);
}
```
