// Learn more about F# at http://fsharp.org

open IrsdkFS

[<EntryPoint>]
let main argv =
    let test = IrsdkFS.SimStatus()
    //let testTwo = SimStatusTwo.mySimStatus
    printf "%A" test
    0 // return an integer exit code
