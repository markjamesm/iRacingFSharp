// Learn more about F# at http://fsharp.org

open IrsdkFS

[<EntryPoint>]
let main argv =
    let test = IrsdkFS.SimStatus()
    let connection = IrsdkFS.CreateFileMap(IrsdkFS.loadMemoryMap())
    //let testTwo = SimStatusTwo.mySimStatus
    printfn "%A" test
    printfn "%A" connection
    0 // return an integer exit code
