// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

#nullable enable

namespace Woohoo;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

internal static class Requires
{
    [DebuggerStepThrough]
#if NET6_0_OR_GREATER
    public static void NotNull<T>([ValidatedNotNull, NotNull] T? value, [CallerArgumentExpression("value")] string? paramName = null)
#elif NETSTANDARD2_1_OR_GREATER
    public static void NotNull<T>([ValidatedNotNull, NotNull] T? value, string? paramName = null)
#else
    public static void NotNull<T>(T? value, string? paramName = null)
#endif
        where T : class
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    [DebuggerStepThrough]
#if NET6_0_OR_GREATER
    public static void NotNullOrEmpty([ValidatedNotNull, NotNull] string? value, [CallerArgumentExpression("value")] string? paramName = null)
#elif NETSTANDARD2_1_OR_GREATER
    public static void NotNullOrEmpty([ValidatedNotNull, NotNull] string? value, string? paramName = null)
#else
    public static void NotNullOrEmpty(string? value, string? paramName = null)
#endif
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }

        if (value.Length == 0)
        {
            throw new ArgumentException("Parameter value should not be empty string.", paramName);
        }
    }
}
