namespace IrsdkFS

open FSharp.Data
open System.IO.MemoryMappedFiles

///<summary>F# implementation of the iRacing SDK.</summary>
module IrsdkFS =

    type Outcome =
        | OK of filename:MemoryMappedFile
        | Failed of filename:string

    let connectionParameters : ConnectionParameters =
        { DesiredAccess= 2031619u;
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

    ///<summary>Returns the current state of the sim</summary>
    let simStatus() =
        let simStatusURL = { URL= @"http://127.0.0.1:32034/get_sim_status?object=simStatus" }
        let simStatusObject = Http.RequestString(simStatusURL.URL)
        match simStatusObject with
        | simStatusObject when simStatusObject.Contains("running:0") -> false
        | simStatusObject when simStatusObject.Contains("running:1") -> true
        | _ -> false

   // let private Create

    let private loadMemoryMap (memoryMappedFile: MemoryMappedFile) =
        let iRacingMemoryMapAccessor = memoryMappedFile.CreateViewAccessor(0L, 12L)
        iRacingMemoryMapAccessor
        
    let private openMemoryMappedFile =
        try
            let iRacingMemoryMap = MemoryMappedFile.OpenExisting(connectionParameters.MemoryMapFileName) 
            Some(iRacingMemoryMap)
        with
            | ex -> eprintf "Error: %s" ex.Message 
                    None

    ///<summary>Loads the iRacing memory map file if it is present on disk.</summary>
    let start () =
        openMemoryMappedFile
        |> Option.map loadMemoryMap