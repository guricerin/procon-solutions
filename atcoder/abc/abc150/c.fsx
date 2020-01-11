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

let nextPermutation (arr: 'a array) =
    let swap i j =
        let tmp = arr.[i]
        arr.[i] <- arr.[j]
        arr.[j] <- tmp

    let mutable r = Array.length arr - 1
    while r > 0 && arr.[r - 1] >= arr.[r] do
        r <- r - 1
    if r <= 0 then
        false
    else
        let mutable l = Array.length arr - 1
        while (arr.[l] <= arr.[r - 1]) do
            l <- l - 1
        if l < r then failwith "hoge"
        swap l (r - 1)
        let mutable l = Array.length arr - 1
        while r < l do
            swap r l
            r <- r + 1
            l <- l - 1
        true

let rec permutations (arr: 'a array): seq<'a array> =
    seq {
        yield arr
        let mutable arr = arr |> Array.copy
        while nextPermutation arr do
            yield arr
            arr <- arr |> Array.copy
    }

let main() =
    let n = read int
    let P = reada int
    let Q = reada int

    let head =
        [| for i in 1 .. n -> i |]

    let perms = permutations head
    let lim = Seq.length perms
    // for pe in perms do
    //     sprintf "pe: %A" pe |> puts
    let mutable (p, q) = (0, 0)
    perms
    |> Seq.iteri (fun i pe ->
        if pe = P then p <- i + 1
        if pe = Q then q <- i + 1)

    p - q
    |> abs
    |> puts
    ()

main()
writer.Close()
