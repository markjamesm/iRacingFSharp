// Learn more about F# at http://fsharp.org

open IrsdkFS

[<EntryPoint>]
let main argv =
    let test = IrsdkFS.SimStatus()
    printf "%A" test
    0 // return an integer exit code
