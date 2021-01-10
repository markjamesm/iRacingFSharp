namespace IrsdkFS
open FSharp.Data
open System.IO.MemoryMappedFiles
open System.Runtime.InteropServices

///<summary>F# implementation of the iRacing SDK.</summary>
module IrsdkFS =

    type Defines = 
        { DesiredAccess: int
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

    type Result<'TSuccess,'TFailure> = 
        | Success of 'TSuccess
        | Failure of 'TFailure

    let defines =
        { DesiredAccess= 2031619;
        DataValidEventName= "Local\\IRSDKDataValidEvent";
        MemoryMapFileName= "Local\\IRSDKMemMapFileName";
        BroadcastMessageName= "IRSDK_BROADCASTMSG";
        PadCarNumName= "IRSDK_PADCARNUM";
        MaxString= 32;
        MaxDesc= 64; 
        MaxVars= 4096;
        MaxBufs= 4;
        StatusConnected= 1;
        SessionStringLength= 0x20000 }

    let 

    //144 bytes
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
            val mutable pad: int List;

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

    ///<summary>Returns the current state of the sim</summary>
    let SimStatus() =
        let simStatusURL = "http://127.0.0.1:32034/get_sim_status?object=simStatus"
        let simStatusObject = Http.RequestString(simStatusURL)
        match simStatusObject with
        | simStatusObject when simStatusObject.Contains("running:0") -> false
        | simStatusObject when simStatusObject.Contains("running:1") -> true
        | _ -> false

    ///<summary>Loads the iRacing memory map file if it is present on disk.</summary>
    let loadMemoryMap() =
        let iRacingFile = MemoryMappedFile.OpenExisting(defines.MemoryMapFileName)
        iRacingFile
        
                
    ///<summary>Create the filemap to be read.</summary>
    /// <param name="iRacingFile">The iRacing memory map file.</param>
    let CreateFileMap(iRacingFile: MemoryMappedFile) =
        let fileMapView = iRacingFile.CreateViewAccessor()
        let varHeaderSize  = Marshal.SizeOf(typeof<VarHeader>)
        varHeaderSize

  //  let private checkSimStatus = 
    //    if SimStatus() = false then Failure "Startup error. Please make sure that iRacing is running"
    //    else CreateFileMap(loadMemoryMap())