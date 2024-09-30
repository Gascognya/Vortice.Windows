// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XAudio2;

public partial class EffectDescriptor
{
    private IUnknown _effect;

    /// <summary>
    /// Initializes a new instance of the <see cref="EffectDescriptor"/> class with a Stereo Effect.
    /// </summary>
    /// <param name="effect">The effect.</param>
    public EffectDescriptor(IUnknown effect) : this(effect, 2)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EffectDescriptor"/> class.
    /// </summary>
    /// <param name="effect">The effect.</param>
    /// <param name="outputChannelCount">The output channel count.</param>
    public EffectDescriptor(IUnknown effect, uint outputChannelCount)
    {
        EffectPointer = null;
        Effect = effect;
        InitialState = true;
        OutputChannelCount = outputChannelCount;
    }

    /// <summary>
    /// Gets or sets the AudioProcessor. The AudioProcessor cannot be set more than one.
    /// </summary>
    /// <value>The effect.</value>
    public IUnknown Effect
    {
        get => _effect;
        set
        {
            if (value == null)
                throw new ArgumentNullException("value", "Effect cannot be set to null");
            if (_effect != null)
                throw new ArgumentException("Cannot set Effect twice on the same EffectDescriptor");

            _effect = value;
            EffectPointer = _effect;
        }
    }
}
