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
    public enum ProcessAccess
    {
        CreateProcess = 0x80,
        CreateThread = 2,
        DuplicateHandle = 0x40,
        None = 0,
        QueryInformation = 0x400,
        QueryLimitedInformation = 0x1000,
        SetInformation = 0x200,
        SetQuota = 0x100,
        SuspendResume = 0x800,
        Terminate = 1,
        VmOperation = 8,
        VmRead = 0x10,
        VmWrite = 0x20
    }
}
