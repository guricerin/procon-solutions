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

let toInt s =
    match s with
    | "XII" -> 0
    | "I" -> 1
    | "II" -> 2
    | "III" -> 3
    | "IIII" -> 4
    | "V" -> 5
    | "VI" -> 6
    | "VII" -> 7
    | "VIII" -> 8
    | "IX" -> 9
    | "X" -> 10
    | "XI" -> 11
    | _ -> invalidArg "s" "hoge"

let toMod t =
    let sign =
        if t >= 0 then 1 else -1

    let t = abs t
    sign * (t % 12)

let toRome t =
    let t = t % 12

    let t =
        if t >= 0 then t else t + 12
    match t with
    | 0 -> "XII"
    | 1 -> "I"
    | 2 -> "II"
    | 3 -> "III"
    | 4 -> "IIII"
    | 5 -> "V"
    | 6 -> "VI"
    | 7 -> "VII"
    | 8 -> "VIII"
    | 9 -> "IX"
    | 10 -> "X"
    | 11 -> "XI"
    | _ -> invalidArg "t" "hoge"

let solve() =
    let [| s; t |] = reada string
    let t = Convert.ToInt32(t) |> toMod
    let s = s |> toInt
    s + t
    |> toRome
    |> puts


[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
