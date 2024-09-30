// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public unsafe partial class ID3D11DeviceChild
{
    /// <summary>
    /// Gets or sets the debug-name for this object.
    /// </summary>
    public string? DebugName
    {
        get
        {
            byte* pname = stackalloc byte[1024];
            uint size = 1024 - 1;
            if (GetPrivateData(CommonGuid.DebugObjectName, ref size, pname).Failure)
            {
                return string.Empty;
            }

            pname[size] = 0;
            return Marshal.PtrToStringAnsi(new IntPtr(pname));
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                SetPrivateData(CommonGuid.DebugObjectName, 0, null);
            }
            else
            {
                IntPtr namePtr = Marshal.StringToHGlobalAnsi(value);
                SetPrivateData(CommonGuid.DebugObjectName, (uint)value!.Length, namePtr.ToPointer());
                Marshal.FreeHGlobal(namePtr);
            }
        }
    }
}
