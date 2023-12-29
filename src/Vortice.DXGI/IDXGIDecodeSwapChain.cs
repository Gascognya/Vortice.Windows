// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.DXGI;

public partial class IDXGIDecodeSwapChain
{
    public SizeI DestSize
    {
        get
        {
            GetDestSize(out int width, out int height);
            return new(width, height);
        }
        set
        {
            SetDestSize(value.Width, value.Height);
        }
    }

    public Result PresentBuffer(int bufferToPresent, int syncInterval)
    {
        return PresentBuffer(bufferToPresent, syncInterval, PresentFlags.None);
    }
}
