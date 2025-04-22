namespace Woohoo.Security.Cryptography;

using System;
using System.Security.Cryptography;

/// <summary>
/// Performs an XOR transformation on an input buffer.
/// </summary>
public sealed class XorTransform : ICryptoTransform
{
    private readonly byte[] key;

    /// <summary>
    /// Initializes a new instance of the <see cref="XorTransform"/> class.
    /// </summary>
    /// <param name="key">XOR encryption key.</param>
    public XorTransform(byte[] key)
    {
        this.key = key;
    }

    /// <summary>
    /// Gets a value indicating whether the current transform can be reused.
    /// </summary>
    public bool CanReuseTransform
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// Gets a value indicating whether multiple blocks can be transformed.
    /// </summary>
    public bool CanTransformMultipleBlocks
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the input block size.
    /// </summary>
    public int InputBlockSize
    {
        get
        {
            return 1;
        }
    }

    /// <summary>
    /// Gets the output block size.
    /// </summary>
    public int OutputBlockSize
    {
        get
        {
            return 1;
        }
    }

    /// <summary>
    /// Computes the transformation for the specified region of the input byte array and copies the resulting transformation to the specified region of the output byte array.
    /// </summary>
    /// <param name="inputBuffer">The input on which to perform the operation.</param>
    /// <param name="inputOffset">The offset into the input byte array from which to begin using data.</param>
    /// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
    /// <param name="outputBuffer">The output to which to write the data.</param>
    /// <param name="outputOffset">The offset into the output byte array from which to begin writing data.</param>
    /// <returns>The number of bytes written.</returns>
    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
        if (inputBuffer == null)
        {
            throw new ArgumentNullException(nameof(inputBuffer));
        }

        if (inputOffset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(inputOffset));
        }

        if (inputCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        }

        if (inputCount > inputBuffer.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        }

        if ((inputBuffer.Length - inputCount) < inputOffset)
        {
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        }

        if (outputBuffer == null)
        {
            throw new ArgumentNullException(nameof(outputBuffer));
        }

        if (outputOffset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(outputOffset));
        }

        if (inputCount > outputBuffer.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        }

        if ((outputBuffer.Length - inputCount) < outputOffset)
        {
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        }

        int keyLength = this.key.Length;
        for (int index = 0; index < inputCount; index++)
        {
            outputBuffer[outputOffset + index] = (byte)(inputBuffer[inputOffset + index] ^ this.key[index % keyLength]);
        }

        return inputCount;
    }

    /// <summary>
    /// Computes the transformation for the specified region of the specified byte array.
    /// </summary>
    /// <param name="inputBuffer">The input on which to perform the operation.</param>
    /// <param name="inputOffset">The offset into the byte array from which to begin using data.</param>
    /// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
    /// <returns>The computed transform.</returns>
    public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
        if (inputBuffer == null)
        {
            throw new ArgumentNullException(nameof(inputBuffer));
        }

        if (inputOffset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(inputOffset));
        }

        if (inputCount < 0 || inputCount > inputBuffer.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        }

        if ((inputBuffer.Length - inputCount) < inputOffset)
        {
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        }

        int keyLength = this.key.Length;
        byte[] outputBuffer = new byte[inputCount];

        for (int index = 0; index < inputCount; index++)
        {
            outputBuffer[index] = (byte)(inputBuffer[inputOffset + index] ^ this.key[index % keyLength]);
        }

        return outputBuffer;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
    }
}
