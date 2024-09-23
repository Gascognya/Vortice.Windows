﻿// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the root signature 1.0 layout of a descriptor table as a collection of descriptor ranges that appear one after the other in a descriptor heap.
/// </summary>
public unsafe partial class RootDescriptorTable
{
    /// <summary>
    /// Gets or sets the descriptor ranges.
    /// </summary>
    public DescriptorRange[] Ranges { get; set; }

    public RootDescriptorTable(params DescriptorRange[] ranges)
    {
        Ranges = ranges;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public int NumDescriptorRanges;
        public void* PDescriptorRanges;

        internal void __MarshalFree()
        {
            if (PDescriptorRanges != null)
            {
                NativeMemory.Free(PDescriptorRanges);
            }
        }
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal void __MarshalFrom(in __Native @ref)
    {
        Ranges = new DescriptorRange[@ref.NumDescriptorRanges];
        if (@ref.NumDescriptorRanges > 0)
        {
            UnsafeUtilities.Read(@ref.PDescriptorRanges, Ranges);
        }
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.NumDescriptorRanges = Ranges?.Length ?? 0;
        @ref.PDescriptorRanges = UnsafeUtilities.AllocToPointer(Ranges);
    }
    #endregion
}
