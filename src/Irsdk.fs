namespace IrsdkFS
open System.Text.Json
open FSharp.Data

///<summary>F# implementation of the iRacing SDK.</summary>
module IrsdkFS =

    ///<summary>Returns the current state of the sim</summary>
    let SimStatus() =
        let simStatusURL = "http://127.0.0.1:32034/get_sim_status?object=simStatus"
        let simStatusObject = Http.RequestString(simStatusURL)
        match simStatusObject with
        | simStatusObject when simStatusObject.Contains("running:0") -> false
        | simStatusObject when simStatusObject.Contains("running:1") -> true
        | simStatusObject -> false