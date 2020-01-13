open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f
    let readChars() = read string |> Seq.toArray
    let readInts() = readChars() |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

[<AutoOpen>]
module Cout =
    let writer = new IO.StreamWriter(new IO.BufferedStream(Console.OpenStandardOutput()))
    let print (s: string) = writer.Write s
    let println (s: string) = writer.WriteLine s
    let inline puts (s: ^a) = string s |> println

let main() =
    let [| N; M |] = reada int
    let acs = Array.zeroCreate N
    let mutable ac = 0
    let mutable wa = 0
    let was = Array.zeroCreate N
    for i in 0 .. M - 1 do
        let [| p; s |] = reada string
        let p = Convert.ToInt32(p) - 1
        match s with
        | "AC" ->
            if acs.[p] |> not then
                acs.[p] <- true
                ac <- ac + 1
                wa <- wa + was.[p]
        | "WA" -> was.[p] <- was.[p] + 1
        | _ -> failwith "hoge"

    sprintf "%d %d" ac wa |> puts
    ()

main()
writer.Close()
