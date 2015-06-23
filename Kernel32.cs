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
using System.Diagnostics;
using System.Runtime.InteropServices;
#if X64
using ADDR = System.UInt64;
#else
using ADDR = System.UInt32;
#endif

namespace DAWhiteMagic
{
    public class NativeMethods : IDisposable
    {
        private readonly IntPtr _processHandle;
        const int MOD_ALT = 0x1;
        const int MOD_CONTROL = 0x2;
        const int MOD_SHIFT = 0x4;
        const int MOD_WIN = 0x8;
        const int WM_HOTKEY = 0x312;

        internal NativeMethods(IntPtr procHandle)
        {
            _processHandle = procHandle != IntPtr.Zero
                ? procHandle : OpenProcess((ProcessAccess) 0x001F0FFF, true, Process.GetCurrentProcess().Id);
        }

        #region IDisposable Members

        public void Dispose()
        {
            CloseHandle(_processHandle);
        }

        #endregion

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CreateProcess(string applicationPath,
            string commandLine,
            IntPtr processSecurityAttributes,
            IntPtr threadSecurityAttributes,
            bool inheritHandles,
            ProcessCreationFlags creationFlags,
            IntPtr environment,
            string currentDirectory,
            ref StartupInfo startupInfo,
            out ProcessInformation processInformation);

        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccess access, bool inheritHandle, int processId);

        public static bool Peek(Process proc, int target, byte[] data)
        {
            return ReadProcessMemory(proc.Handle, target, data, data.Length, 0);
        }

        internal static bool Poke(Process proc, int target, byte[] data)
        {
            return WriteProcessMemory(proc.Handle, target, data, data.Length, 0);
        }

        [DllImport("kernel32", SetLastError = true)]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, [Out] byte[] lpBuffer, int nSize, byte lpNumberOfBytesRead);
        [DllImport("kernel32", SetLastError = true)]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr baseAddress, IntPtr buffer, int count, out int bytesRead);

        [DllImport("kernel32.dll")]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, byte lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr baseAddress, IntPtr buffer, int count, out int bytesWritten);

        [DllImport("kernel32", SetLastError = true)]
        public static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32", SetLastError = true)]
        internal static extern WaitEventResult WaitForSingleObject(IntPtr hObject, int timeout);

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32")]
        public static extern int GetLastError();

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize,
            out int lpNumberOfBytesRead);
    }
}
