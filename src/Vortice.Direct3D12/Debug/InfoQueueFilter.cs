// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Debug;

public partial class InfoQueueFilter
{
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public InfoQueueFilterDescription.__Native AllowList;

        public InfoQueueFilterDescription.__Native DenyList;

        internal void __MarshalFree()
        {
            AllowList.__MarshalFree();
            DenyList.__MarshalFree();
        }
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal void __MarshalFrom(ref __Native @ref)
    {
        AllowList = new InfoQueueFilterDescription();
        AllowList.__MarshalFrom(ref @ref.AllowList);
        DenyList = new InfoQueueFilterDescription();
        DenyList.__MarshalFrom(ref @ref.DenyList);
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.AllowList = default;
        if (AllowList != null)
        {
            AllowList.__MarshalTo(ref @ref.AllowList);
        }

        @ref.DenyList = default;
        if (DenyList != null)
        {
            DenyList.__MarshalTo(ref @ref.DenyList);
        }
    }
}
