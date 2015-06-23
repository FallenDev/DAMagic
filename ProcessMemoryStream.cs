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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DAWhiteMagic
{
    public sealed class ProcessMemoryStream : Stream
    {
        // Fields
        private readonly ProcessAccess access;
        private bool disposed;
        private IntPtr hProcess;

        // Methods
        public ProcessMemoryStream(int processId, ProcessAccess access)
        {
            this.access = access;
            ProcessId = processId;
            hProcess = NativeMethods.OpenProcess(access, false, processId);
            if (hProcess == IntPtr.Zero)
            {
                throw new ArgumentException("Unable to open the process.");
            }
        }

        public override void Close()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ProcessMemoryStream");
            }
            if (hProcess != IntPtr.Zero)
            {
                NativeMethods.CloseHandle(hProcess);
                hProcess = IntPtr.Zero;
            }
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (hProcess != IntPtr.Zero)
                {
                    NativeMethods.CloseHandle(hProcess);
                    hProcess = IntPtr.Zero;
                }
                base.Dispose(disposing);
            }
            disposed = true;
        }

        ~ProcessMemoryStream()
        {
            Dispose(false);
        }

        public override void Flush()
        {
            throw new NotSupportedException("Flush is not supported.");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ProcessMemoryStream");
            }
            if (hProcess == IntPtr.Zero)
            {
                throw new InvalidOperationException("Process is not open.");
            }
            IntPtr ptr = Marshal.AllocHGlobal(count);
            if (ptr == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to allocate memory.");
            }
            int bytesRead = 0;
            NativeMethods.ReadProcessMemory(hProcess, (IntPtr)Position, ptr, count, out bytesRead);
            Position += bytesRead;
            Marshal.Copy(ptr, buffer, offset, count);
            Marshal.FreeHGlobal(ptr);
            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ProcessMemoryStream");
            }
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;

                case SeekOrigin.Current:
                    Position += offset;
                    break;

                case SeekOrigin.End:
                    throw new NotSupportedException("SeekOrigin.End not supported.");
            }
            return Position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Cannot set the length for this stream.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ProcessMemoryStream");
            }
            if (hProcess == IntPtr.Zero)
            {
                throw new InvalidOperationException("Process is not open.");
            }
            IntPtr destination = Marshal.AllocHGlobal(count);
            if (destination == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to allocate memory.");
            }
            Marshal.Copy(buffer, offset, destination, count);
            int bytesWritten = 0;
            NativeMethods.WriteProcessMemory(hProcess, (IntPtr)Position, destination, count, out bytesWritten);
            Position += bytesWritten;
            Marshal.FreeHGlobal(destination);
        }

        public override void WriteByte(byte value)
        {
            Write(new byte[] { value }, 0, 1);
        }

        public void WriteString(string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            Write(bytes, 0, bytes.Length);
        }

        // Properties
        public override bool CanRead
        {
            get
            {
                return ((access & ProcessAccess.VmRead) > ProcessAccess.None);
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return ((access & (ProcessAccess.VmWrite | ProcessAccess.VmOperation)) > ProcessAccess.None);
            }
        }

        public override long Length
        {
            get
            {
                throw new NotSupportedException("Length is not supported.");
            }
        }

        public override long Position { get; set; }

        public int ProcessId { get; set; }
    }
}

