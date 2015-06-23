#region License/Copyright

// DAWhiteMagic - Injected .NET Helper Library
//     Copyright (C) 2015 Fallen Dev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion
using System;

namespace DAWhiteMagic
{
    [Flags]
    public enum ProcessCreationFlags
    {
        BreakawayFromJob = 0x1000000,
        DebugOnlyThisProcess = 2,
        DebugProcess = 1,
        DefaultErrorMode = 0x4000000,
        DetachedProcess = 8,
        ExtendedStartupInfoPresent = 0x80000,
        InheritParentAffinity = 0x1000,
        NewConsole = 0x10,
        NewProcessGroup = 0x200,
        NoWindow = 0x8000000,
        PreserveCodeAuthZLevel = 0x2000000,
        ProtectedProcess = 0x40000,
        SeparateWowVdm = 0x800,
        SharedWowVdm = 0x1000,
        Suspended = 4,
        UnicodeEnvironment = 0x400
    }
}
