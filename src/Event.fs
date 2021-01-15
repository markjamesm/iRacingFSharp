namespace IrsdkFS

open System
open System.Runtime.InteropServices

module Event =
    [<Literal>]
    let STANDARD_RIGHTS_REQUIRED = 0x000F0000u
    [<Literal>]
    let SYNCHRONIZE = 0x00100000u
    [<Literal>]
    let EVENT_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED ||| SYNCHRONIZE ||| 0x3u)
    [<Literal>]
    let EVENT_MODIFY_STATE = 0x0002u
    [<Literal>]
    let ERROR_FILE_NOT_FOUND = 2L
    [<DllImport("Kernel32.dll", SetLastError = true)>]
    extern IntPtr OpenEvent(uint dwDesiredAccess, bool bInheritHandle, string lpName);
    [<DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)>]
    extern Int32 WaitForSingleObject(IntPtr Handle, Int32 Wait);