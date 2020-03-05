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

// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------

let main() =
    let [| N; M |] = reada int
    let bs = Array.init N (fun _ -> 1)
    let red = Array.zeroCreate N
    red.[0] <- true
    // 固体ではなく液体の移動と捉えるとわかりやすい
    // もしくはウイルス感染
    for i in 0 .. M - 1 do
        let [| x; y |] = reada int
        let x, y = x - 1, y - 1
        bs.[x] <- bs.[x] - 1
        bs.[y] <- bs.[y] + 1
        if red.[x] then red.[y] <- true
        if bs.[x] = 0 then red.[x] <- false

    red
    |> Array.filter id
    |> Array.length
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
