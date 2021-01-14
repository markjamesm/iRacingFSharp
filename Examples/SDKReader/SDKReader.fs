// Learn more about F# at http://fsharp.org

open IrsdkFS

[<EntryPoint>]
let main argv =
    let simStatus iRacingStatus = 
        match iRacingStatus with
        | true -> "iRacing is running."
        | false -> "iRacing is not running."

    let irsdkTest = IrsdkFS.start()
    let status = simStatus(IrsdkFS.simStatus())

    printfn "\n%s" status
    printfn "Header version: %A" irsdkTest
    0 // return an integer exit code
