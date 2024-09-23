﻿// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

/// <summary>
/// A state subobject that represents a raytracing pipeline configuration.
/// </summary>
public partial struct RaytracingPipelineConfig : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
{
    StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.RaytracingPipelineConfig;

    /// <summary>
    /// Initializes a new instance of the <see cref="RaytracingPipelineConfig"/> struct.
    /// </summary>
    /// <param name="maxTraceRecursionDepth">
    /// Limit on ray recursion for the raytracing pipeline. It must be in the range of 0 to 31. 
    /// Below the maximum recursion depth, shader invocations such as closest hit or miss shaders can call <b>TraceRay</b> any number of times. 
    /// At the maximum recursion depth, <b>TraceRay</b> calls result in the device going into removed state.
    /// </param>
    public RaytracingPipelineConfig(uint maxTraceRecursionDepth)
    {
        MaxTraceRecursionDepth = maxTraceRecursionDepth;
    }

    #region Marshal
    unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup)
    {
        var native = Marshal.AllocHGlobal(sizeof(RaytracingPipelineConfig));
        Unsafe.WriteUnaligned(native.ToPointer(), this);
        return native;
    }

    unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        Marshal.FreeHGlobal(pDesc);
    }
    #endregion Marshal
}
