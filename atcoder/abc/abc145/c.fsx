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

module Permutation =

    /// 引数が破壊的に更新されることに注意
    let nextPermutation (arr: 'a array): bool =
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
            if l < r then failwith "nextPermutation: Bug Found!"
            swap l (r - 1)
            let mutable l = Array.length arr - 1
            while r < l do
                swap r l
                r <- r + 1
                l <- l - 1
            true

    /// 順列を列挙
    /// O (n!)
    let permutations (arr: 'a array): seq<'a array> =
        seq {
            yield arr
            let mutable arr = arr |> Array.copy
            while nextPermutation arr do
                yield arr
                arr <- arr |> Array.copy
        }

// -----------------------------------------------------------------------------------------------------

let main() =
    let n = read int
    let perms = Permutation.permutations [| 0 .. n - 1 |]
    let xys = Array.zeroCreate n
    for i in 0 .. n - 1 do
        let [| x; y |] = reada int
        xys.[i] <- x, y
    seq {
        for perm in perms do
            let mutable tot = 0.
            for i in 0 .. n - 2 do
                let x1, y1 = xys.[perm.[i]]
                let x2, y2 = xys.[perm.[i + 1]]

                let dist =
                    pown (x1 - x2) 2 + (pown (y1 - y2) 2)
                    |> float
                    |> sqrt
                tot <- tot + dist
            yield tot
    }
    |> Seq.average
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
