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

module Prime =

    let inline isPrime (n: int) =
        let limit =
            n
            |> float
            |> sqrt
            |> int
        seq {
            for p in 2 .. limit do
                if n % p = 0 then yield ()
        }
        |> Seq.isEmpty

    /// nを素因数分解した結果を返す
    let primeFactors (n: int64): Map<int64, int64> =
        let limit =
            n
            |> float
            |> sqrt
            |> int64
        let rec count x p acc =
            if x % p = 0L then count (x / p) p (acc + 1L) else acc

        let mutable n = n

        let res =
            seq {
                for p in 2L .. limit + 1L do
                    let c = count n p 0L
                    if c <> 0L then
                        let div = (float p) ** (float c) |> int64
                        n <- n / div
                        yield (p, c)
            }
            |> Map.ofSeq
        if n = 1L then res else res.Add(n, 1L)

    /// nの約数の個数
    let divisorsCount (n: int64): int64 = primeFactors n |> Map.fold (fun acc k v -> acc * (v + 1L)) 1L

    /// upper以下の素数を列挙
    let sieveToUpper upper =
        seq {
            yield 2
            let knownComposites = System.Collections.Generic.SortedSet<int>()
            for i in 3 .. 2 .. upper do
                let found = knownComposites.Contains(i)
                if not found then yield i
                for j in i .. i .. upper do
                    knownComposites.Add(j) |> ignore
        }



    /// nの約数を列挙(n自身を含む)
    /// O(log n)
    let divisors (n: int64): int64 array =
        let lim =
            n
            |> float
            |> sqrt
            |> int64
        seq {
            for i in 1L .. lim do
                if n % i = 0L then
                    yield i
                    if i * i <> n then yield n / i
        }
        |> Array.ofSeq
        |> Array.sort

let eraSieve upper =
    let upper = int upper + 1
    let res = Array.init upper (fun _ -> true)
    res.[0] <- false
    res.[1] <- false
    let rec loop a b =
        let c = a * b
        if c >= upper then
            ()
        else
            res.[c] <- false
            loop a (b + 1)
    for i in 2 .. upper - 1 do
        if res.[i] then loop i 2
    res

let main() =
    let n = read int
    let sieve = eraSieve 10000
    let res = Array.zeroCreate 10001
    for i in 0 .. 3 do
        res.[i] <- false
    for i in 4 .. 10 do
        res.[i] <- true
    for i in 11 .. 10000 do
        let mutable ok = false
        for j in 2 .. i do
            if sieve.[j] then
                // 相手に負けパターンを押しつけられる素数が1つでもあれば勝ち
                if i - j >= 2 && res.[i - j] |> not then ok <- true
        res.[i] <- ok

    if res.[n] then "Win" else "Lose"
    |> puts
    ()

main()
writer.Close()
writer.Close()
