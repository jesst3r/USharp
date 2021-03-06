﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnrealEngine.Runtime;
using UnrealEngine.Runtime.Native;

namespace UnrealEngine.Engine
{
    [UMetaPath("/Script/Engine.UserDefinedStruct", "CoreUObject", UnrealModuleType.Engine)]
    public class UUserDefinedStruct : UScriptStruct
    {
        public EUserDefinedStructureStatus Status
        {
            get
            {
                // WITH_EDITORONLY_DATA
                if (Native_UUserDefinedStruct.Get_Status == null)
                {
                    return default(EUserDefinedStructureStatus);
                }

                return Native_UUserDefinedStruct.Get_Status(Address);
            }
            set
            {
                // WITH_EDITORONLY_DATA
                if (Native_UUserDefinedStruct.Set_Status == null)
                {
                    return;
                }

                Native_UUserDefinedStruct.Set_Status(Address, value);
            }
        }

        public string ErrorMessage
        {
            get
            {
                // WITH_EDITORONLY_DATA
                if (Native_UUserDefinedStruct.Get_ErrorMessage == null)
                {
                    return null;
                }

                using (FStringUnsafe resultUnsafe = new FStringUnsafe())
                {
                    Native_UUserDefinedStruct.Get_ErrorMessage(Address, ref resultUnsafe.Array);
                    return resultUnsafe.Value;
                }
            }
            set
            {
                // WITH_EDITORONLY_DATA
                if (Native_UUserDefinedStruct.Set_ErrorMessage == null)
                {
                    return;
                }

                using (FStringUnsafe errorMessageUnsafe = new FStringUnsafe(value))
                {
                    Native_UUserDefinedStruct.Set_ErrorMessage(Address, ref errorMessageUnsafe.Array);
                }
            }
        }

        public UObject EditorData
        {
            get
            {
                // WITH_EDITORONLY_DATA
                if (Native_UUserDefinedStruct.Get_EditorData == null)
                {
                    return null;
                }

                return GCHelper.Find<UObject>(Native_UUserDefinedStruct.Get_EditorData(Address));
            }
            set
            {
                // WITH_EDITORONLY_DATA
                if (Native_UUserDefinedStruct.Set_EditorData == null)
                {
                    return;
                }

                Native_UUserDefinedStruct.Set_EditorData(Address, value == null ? IntPtr.Zero : value.Address);
            }
        }

        public Guid Guid
        {
            get
            {
                Guid result;
                Native_UUserDefinedStruct.Get_Guid(Address, out result);
                return result;
            }
            set { Native_UUserDefinedStruct.Set_Guid(Address, ref value); }
        }
    }

    public enum EUserDefinedStructureStatus : byte
    {
        /// <summary>
        /// Struct is in an unknown state.
        /// </summary>
        UpToDate,

        /// <summary>
        /// Struct has been modified but not recompiled.
        /// </summary>
        Dirty,

        /// <summary>
        /// Struct tried but failed to be compiled.
        /// </summary>
        Error,

        /// <summary>
        /// Struct is a duplicate, the original one was changed.
        /// </summary>
        Duplicate,
    }
}
