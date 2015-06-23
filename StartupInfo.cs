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
using System.Runtime.InteropServices;

namespace DAWhiteMagic
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StartupInfo
    {
        public int Size { get; set; }
        public string Reserved { get; set; }
        public string Desktop { get; set; }
        public string Title { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int XCountChars { get; set; }
        public int YCountChars { get; set; }
        public int FillAttribute { get; set; }
        public int Flags { get; set; }
        public short ShowWindow { get; set; }
        public short Reserved2 { get; set; }
        public IntPtr Reserved3 { get; set; }
        public IntPtr StdInputHandle { get; set; }
        public IntPtr StdOutputHandle { get; set; }
        public IntPtr StdErrorHandle { get; set; }
    }
}
