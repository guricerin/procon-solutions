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

module Gcd =

    let inline gcd (x: ^a) (y: ^a): ^a =
        let zero = LanguagePrimitives.GenericZero

        let rec loop x y =
            if y = zero then x else loop y (x % y)
        loop x y

    let inline lcm (x: ^a) (y: ^a): ^a =
        let g = gcd x y
        x / g * y

let f x =
    let mutable cnt = 0L
    let mutable x = x
    while x % 2L = 0L do
        x <- x / 2L
        cnt <- cnt + 1L
    cnt

let main() =
    let [| N; M |] = reada int64
    let A = reada int64 |> Array.map (fun a -> a / 2L)
    let ts = Array.map f A
    let mutable ok = true
    for t in ts do
        if t <> ts.[0] then ok <- false
    match ok with
    | false -> puts 0
    | true ->
        let lcm = Array.fold (fun acc a -> Gcd.lcm acc a) A.[0] A
        let cnt = M / lcm
        let cnt = (cnt + 2L - 1L) / 2L // lcmの奇数倍のみを数え上げる
        puts cnt

    ()

main()
writer.Close()
