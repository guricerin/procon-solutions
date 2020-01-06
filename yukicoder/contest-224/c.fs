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

[<AutoOpen>]
module Prime =
    let limit n =
        n
        |> float
        |> sqrt
        |> int

    let inline isPrime (n: int) =
        let lim = limit n
        seq {
            for p in 2 .. lim do
                if n % p = 0 then yield ()
        }
        |> Seq.isEmpty

    let isheihou n =
        seq {
            for i in 2 .. (limit n) do
                if i * i = n then yield ()
        }
        |> Seq.isEmpty
        |> not

    let isripou n =
        seq {
            for i in 2 .. (limit n) do
                if i * i * i = n then yield ()
        }
        |> Seq.isEmpty
        |> not

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

    let divsum n = divisors n |> Array.sum

let solve() =
    let n = read int
    match n with
    | _ when n < 2 -> string n
    | _ when isPrime n -> "Sosu!"
    | _ when isheihou n -> "Heihosu!"
    | _ when isripou n -> "Ripposu!"
    | _ when (divsum (int64 n)) = (int64 n) * 2L -> "Kanzensu!"
    | _ -> string n
    |> puts

    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> failwithf "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
