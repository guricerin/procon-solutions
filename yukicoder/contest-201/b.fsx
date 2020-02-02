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
    let [| n; m; p; q |] = reada int
    let p = p - 1
    let month = Array.init 12 (fun _ -> m)
    for i in p .. p + q - 1 do
        let i = i % 12
        month.[i] <- 2 * m

    let rec loop acc n i =
        if n > 0 then
            let n = n - month.[i]
            let i = (i + 1) % 12
            loop (acc + 1) n i
        else
            acc

    loop 0 n 0 |> puts
    ()

main()
writer.Close()
