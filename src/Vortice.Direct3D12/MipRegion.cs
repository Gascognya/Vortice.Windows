﻿// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial struct MipRegion
{
    /// <summary>
    /// Initializes a new instance of <see cref="MipRegion"/> structure.
    /// </summary>
    /// <param name="width">The width component of the region.</param>
    /// <param name="height">The height component of the region.</param>
    /// <param name="depth">The depth component of the region.</param>
    public MipRegion(int width, int height, int depth)
    {
        Width = width;
        Height = height;
        Depth = depth;
    }
}
