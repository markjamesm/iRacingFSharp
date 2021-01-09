namespace IrsdkFS

open FSharp.Data

///<summary>comment</summary>
module IrsdkFS =

    ///<summary>Returns the simStatus in string format</summary>
    let SimStatus() =
        let simStatusURL = "http://127.0.0.1:32034/get_sim_status?object=simStatus"
        let simStatusObject = Http.RequestString(simStatusURL)
        simStatusObject