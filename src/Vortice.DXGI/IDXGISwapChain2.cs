// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.DXGI;

public partial class IDXGISwapChain2
{
    public SizeI SourceSize
    {
        get
        {
            GetSourceSize(out int width, out int height);
            return new(width, height);
        }
        set => SetSourceSize(value.Width, value.Height);
    }
}
