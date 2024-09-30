﻿// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Gdi;

namespace Vortice.DirectWrite;

public partial class IDWriteGdiInterop1
{
    public unsafe IDWriteFont CreateFontFromLOGFONT(LogFont logFont, IDWriteFontCollection fontCollection)
    {
        int sizeOfLogFont = Marshal.SizeOf(logFont);
        byte* nativeLogFont = stackalloc byte[sizeOfLogFont];
        Marshal.StructureToPtr(logFont, new IntPtr(nativeLogFont), false);
        return CreateFontFromLOGFONT(new IntPtr(nativeLogFont), fontCollection);
    }

    public unsafe IDWriteFontSet GetMatchingFontsByLOGFONT(LogFont logFont, IDWriteFontSet fontSet)
    {
        int sizeOfLogFont = Marshal.SizeOf(logFont);
        byte* nativeLogFont = stackalloc byte[sizeOfLogFont];
        Marshal.StructureToPtr(logFont, new IntPtr(nativeLogFont), false);
        return GetMatchingFontsByLOGFONT(new IntPtr(nativeLogFont), fontSet);
    }
}
