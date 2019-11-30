open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

let rec gcd x y =
    if y = 0L then x
    else gcd y (x % y)

let primeFactors n =
    let limit = n |> float |> sqrt |> int64
    let rec count x p acc =
        if x % p = 0L then count (x / p) p (acc + 1L)
        else acc
    let mutable n = n
    let res =
        seq {
            for p in 2L..limit+1L do
                let c = count n p 0L
                if c <> 0L then
                    let div = (float p) ** (float c) |> int64
                    n <- n / div
                    yield (p, c)
        } |> Map.ofSeq
    if n = 1L then res
    else res.Add (n, 1L)

[<EntryPoint>]
let main _ =
    let [|A;B|] = reada int64
    let primes = gcd A B |> primeFactors
    primes.Count + 1
    |> printfn "%d"
    0
