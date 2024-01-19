﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.DirectInput;

public partial class EffectParameters
{
    /// <summary>
    /// Optional Envelope structure that describes the envelope to be used by this effect. Not all effect types use envelope. If no envelope is to be applied, the member should be set to null. 
    /// </summary>
    public Envelope Envelope { get; set; }

    /// <summary>
    /// Gets or sets an array containing identifiers or offsets identifying the axes to which the effect is to be applied. 
    /// The flags <see cref="EffectFlags.ObjectIds"/> and <see cref="EffectFlags.ObjectOffsets"/> determine the semantics of the values in the array.
    /// The list of axes associated with an effect cannot be changed once it has been set.
    /// No more than 32 axes can be associated with a single effect. 
    /// </summary>
    /// <value>The axes.</value>
    public int[] Axes { get; set; }

    /// <summary>
    /// Gets or sets an array containing either Cartesian coordinates, polar coordinates, or spherical coordinates. 
    /// The flags <see cref="EffectFlags.Cartesian"/>, <see cref="EffectFlags.Polar"/>, and <see cref="EffectFlags.Spherical"/> determine the semantics of the values in the array.
    /// If Cartesian, each value in Directions is associated with the corresponding axis in Axes.
    /// If polar, the angle is measured in hundredths of degrees from the (0, - 1) direction, rotated in the direction of (1, 0). This usually means that north is away from the user, and east is to the user's right. The last element is not used.
    /// If spherical, the first angle is measured in hundredths of a degree from the (1, 0) direction, rotated in the direction of (0, 1). The second angle (if the number of axes is three or more) is measured in hundredths of a degree toward (0, 0, 1). The third angle (if the number of axes is four or more) is measured in hundredths of a degree toward (0, 0, 0, 1), and so on. The last element is not used. 
    /// </summary>
    /// <value>The directions.</value>
    public int[] Directions { get; set; }

    /// <summary>
    /// Gets or sets the type specific parameter.
    /// Reference to a type-specific parameters, or null  if there are no type-specific parameters.
    /// If the effect is of type <see cref="EffectType.Condition"/>, this member contains an indirect reference to a ConditionSet structures that define the parameters for the condition. A single structure may be used, in which case the condition is applied in the direction specified in the Directions array. Otherwise, there must be one structure for each axis, in the same order as the axes in rgdwAxes array. If a structure is supplied for each axis, the effect should not be rotated; you should use the following values in the Directions array: <see cref="EffectFlags.Spherical"/> : 0, 0, ... / <see cref="EffectFlags.Polar"/>: 9000, 0, ... / <see cref="EffectFlags.Cartesian"/>: 1, 0, ...
    /// If the effect is of type <see cref="EffectType.CustomForce"/>, this member contains an indirect reference to a <see cref="CustomForce"/> that defines the parameters for the custom force.
    /// If the effect is of type <see cref="EffectType.Periodic"/>, this member contains a pointer to a <see cref="PeriodicForce"/> that defines the parameters for the effect.
    /// If the effect is of type <see cref="EffectType.ConstantForce"/>, this member contains a pointer to a <see cref="ConstantForce"/> that defines the parameters for the constant force.
    /// If the effect is of type <see cref="EffectType.RampForce"/>, this member contains a pointer to a <see cref="RampForce"/> that defines the parameters for the ramp force. 
    /// To gain access to the underlying structure, you need to call the method <see cref="TypeSpecificParameters.As{T}"/>.
    /// </summary>
    /// <value>The type specific parameter.</value>
    public TypeSpecificParameters Parameters { get; set; }

    /// <summary>
    /// Gets the axes. See <see cref="Axes"/> and <see cref="Directions"/>.
    /// </summary>
    /// <param name="axes">The axes.</param>
    /// <param name="directions">The directions.</param>
    public void GetAxes(out int[] axes, out int[] directions)
    {
        axes = Axes;
        directions = Directions;
    }

    /// <summary>
    /// Sets the axes. See <see cref="Axes"/> and <see cref="Directions"/>.
    /// </summary>
    /// <param name="axes">The axes.</param>
    /// <param name="directions">The directions.</param>
    public void SetAxes(int[] axes, int[] directions)
    {
        Axes = axes;
        Directions = directions;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe partial struct __Native
    {
        public int Size;
        public EffectFlags Flags;
        public int Duration;
        public int SamplePeriod;
        public int Gain;
        public int TriggerButton;
        public int TriggerRepeatInterval;
        public int AxeCount;
        public IntPtr AxePointer;
        public IntPtr DirectionPointer;
        public void* EnvelopePointer;
        public int TypeSpecificParamCount;
        public IntPtr TypeSpecificParamPointer;
        public int StartDelay;

        internal void __MarshalFree()
        {
            if (AxePointer != IntPtr.Zero)
                Marshal.FreeHGlobal(AxePointer);
            if (DirectionPointer != IntPtr.Zero)
                Marshal.FreeHGlobal(DirectionPointer);
            if (EnvelopePointer != null)
                NativeMemory.Free(EnvelopePointer);
        }
    }

    internal static unsafe __Native __NewNative()
    {
        __Native native = default;
        native.Size = sizeof(__Native);
        return native;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        if (Parameters != null && @ref.TypeSpecificParamPointer != IntPtr.Zero)
            Parameters.MarshalFree(@ref.TypeSpecificParamPointer);

        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Size = sizeof(__Native);
        @ref.Flags = Flags;
        @ref.Duration = Duration;
        @ref.SamplePeriod = SamplePeriod;
        @ref.Gain = Gain;
        @ref.TriggerButton = TriggerButton;
        @ref.TriggerRepeatInterval = TriggerRepeatInterval;
        @ref.StartDelay = StartDelay;

        // Marshal Axes and Directions
        @ref.AxeCount = 0;
        @ref.AxePointer = IntPtr.Zero;
        @ref.DirectionPointer = IntPtr.Zero;
        if (Axes != null && Axes.Length > 0)
        {
            @ref.AxeCount = Axes.Length;

            @ref.AxePointer = Marshal.AllocHGlobal(Axes.Length * sizeof(int));
            UnsafeUtilities.Write(@ref.AxePointer, Axes, 0, Axes.Length);

            if (Directions != null && Directions.Length == Axes.Length)
            {
                @ref.DirectionPointer = Marshal.AllocHGlobal(Directions.Length * sizeof(int));
                UnsafeUtilities.Write(@ref.DirectionPointer, Directions, 0, Directions.Length);
            }
        }

        // Marshal Envelope
        @ref.EnvelopePointer = null;
        if (Envelope != null)
        {
            @ref.EnvelopePointer = UnsafeUtilities.Alloc(sizeof(Envelope.__Native));
            var envelopeNative = Envelope.__NewNative();
            Envelope.__MarshalTo(ref envelopeNative);
            UnsafeUtilities.Write(@ref.EnvelopePointer, ref envelopeNative);
        }

        // Marshal TypeSpecificParameters
        @ref.TypeSpecificParamCount = 0;
        @ref.TypeSpecificParamPointer = IntPtr.Zero;
        if (Parameters != null)
        {
            @ref.TypeSpecificParamCount = Parameters.Size;
            @ref.TypeSpecificParamPointer = Parameters.MarshalTo();
        }
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        this.Flags = @ref.Flags;
        this.Duration = @ref.Duration;
        this.SamplePeriod = @ref.SamplePeriod;
        this.Gain = @ref.Gain;
        this.TriggerButton = @ref.TriggerButton;
        this.TriggerRepeatInterval = @ref.TriggerRepeatInterval;
        this.AxeCount = @ref.AxeCount;
        this.StartDelay = @ref.StartDelay;

        // Marshal Axes and Directions
        if (AxeCount > 0)
        {
            if (@ref.AxePointer != IntPtr.Zero)
            {
                Axes = new int[AxeCount];
                Marshal.Copy(@ref.AxePointer, Axes, 0, AxeCount);
            }

            if (@ref.DirectionPointer != IntPtr.Zero)
            {
                Directions = new int[AxeCount];
                Marshal.Copy(@ref.DirectionPointer, Directions, 0, AxeCount);
            }
        }

        // Marshal Envelope
        if (@ref.EnvelopePointer != null)
        {
            var envelopeNative = *((Envelope.__Native*)@ref.EnvelopePointer);
            Envelope = new Envelope();
            Envelope.__MarshalFrom(ref envelopeNative);
        }

        // Marshal TypeSpecificParameters
        if (@ref.TypeSpecificParamCount > 0 && @ref.TypeSpecificParamPointer != IntPtr.Zero)
            Parameters = new TypeSpecificParameters(@ref.TypeSpecificParamCount, @ref.TypeSpecificParamPointer);

    }
    #endregion Marshal
}
