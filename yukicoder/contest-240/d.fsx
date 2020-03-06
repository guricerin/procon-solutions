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
    let [| N; K |] = reada int
    let A = reada int64
    if A.[K - 1] = 0L then
        printfn "0"
        exit 0

    let A = Array.append [| 0L |] A
    let A = Array.append A [| 0L |]
    let mutable r = K + 1
    let mutable ok = true
    while r < N && ok do
        if A.[r] <= 1L then ok <- false
        if ok then r <- r + 1

    let mutable l = K - 1
    let mutable ok = true
    while l > 0 && ok do
        if A.[l] <= 1L then ok <- false
        if ok then l <- l - 1

    // A.[K] > 1 なら、1以下のマスになるまで両側に進める
    // A.[K] = 1 なら、片方にしか進めない
    let ans = A.[l..r] |> Array.sum
    let ans2 = max (Array.sum A.[l..K]) (Array.sum A.[K..r])

    let ans =
        if A.[K] = 1L then ans2 else ans
    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
