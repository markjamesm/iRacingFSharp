namespace IrsdkFS

open System.Runtime.InteropServices

[<AutoOpen>]
module Types =
    type ConnectionParameters = 
        { DesiredAccess: uint32
          DataValidEventName: string
          MemoryMapFileName: string
          BroadcastMessageName: string
          PadCarNumName: string
          MaxString: int 
          MaxDesc: int 
          MaxVars: int 
          MaxBufs: int 
          StatusConnected: int 
          SessionStringLength: int 
        }

    ///<summary>This type calculates the size of the header in memory. Equals 144 bytes.</summary>
    [<type:StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)>]
    type VarHeader =
        struct
            //16 bytes: offset = 0
            val mutable typeOf: int;
            //offset = 4
            val mutable offset: int;
            //offset = 8
            val mutable count: int;
            [<MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)>]
            val mutable pad: int [];

            //32 bytes: offset = 16
            [<MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)>]
            val mutable name: string;
            //64 bytes: offset = 48
            [<MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)>]
            val mutable desc: string;
            //32 bytes: offset = 112
            [<MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)>]
            val mutable unit: string;
       end

    [<type:StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)>]
    type VarBuf =
        struct
            val mutable tickCount: int;
            val mutable bufOffset: int;
            [<MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)>]
            val mutable pad: int []
        end

    type VarBufWithIndex =
        struct
            val tickCount: int;
            val bufOffset: int;
            val index: int;
        end

    [<type:StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)>]
    type IrsdkHeader =
        struct
            // 12 bytes: offset = 0
            val mutable version: int;
            val mutable status: int;
            val mutable tickRate: int

            // 12 bytes: offset = 12
            val mutable sessionInfoUpdate: int;
            val mutable sessionInfoLength: int;
            val mutable sessionInfoOffset: int;

            // 8 bytes: offset = 24
            val mutable numVars: int;
            val mutable varHeaderOffset: int;

            // 16 bytes: offset = 32
            val mutable numBuf: int;
            val mutable bufLength: int;
            [<MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)>]
            val mutable pad1: int []

            // 128 bytes: offset = 48
            [<MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)>]
            val mutable varbuf: VarBuf; 
        end

    type IrsdkHeaderExtensions() = 
        
        let hasChangedSinceReading(header: IrsdkHeader)(buf: VarBufWithIndex) =
            let changed = header.varbuf.tickCount <> buf.tickCount
            changed

        let FindLatestBuf(header: IrsdkHeader)(requestedTickCount: int) =
            let maxbuf = new VarBuf()
            let maxIndex = -1

    // This class is incomplete.