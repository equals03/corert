﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Internal.TypeSystem;

namespace ILCompiler
{
    public class ExpectedIsaFeaturesRootProvider : ICompilationRootProvider
    {
        private readonly InstructionSetSupport _isaSupport;

        public ExpectedIsaFeaturesRootProvider(InstructionSetSupport isaSupport)
        {
            _isaSupport = isaSupport;
        }

        void ICompilationRootProvider.AddCompilationRoots(IRootingServiceProvider rootProvider)
        {
            if (_isaSupport.Architecture == TargetArchitecture.X64
                || _isaSupport.Architecture == TargetArchitecture.X86)
            {
                int isaFlags = HardwareIntrinsicHelpers.GetRuntimeRequiredIsaFlags(_isaSupport);
                byte[] bytes = BitConverter.GetBytes(isaFlags);
                rootProvider.RootReadOnlyDataBlob(bytes, 4, "ISA support flags", "g_requiredCpuFeatures");
            }
        }
    }
}
