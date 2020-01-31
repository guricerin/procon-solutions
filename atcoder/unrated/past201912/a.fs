open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

module Util =
    let strRev s =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

[<EntryPoint>]
let main _ =
    let s = read string |> Array.ofSeq
    if Array.forall (fun c -> '0' <= c && c <= '9') s then
        let s = s |> String |> int
        printfn "%d" (s * 2)
    else
        printfn "error"
    0 // return an integer exit code
