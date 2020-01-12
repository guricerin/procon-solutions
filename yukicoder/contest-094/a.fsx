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
    let [| x; y; r |] = reada float
    let x, y = abs x, abs y
    // ↓ 誤差が半端ない
    // let edgeX = x + r * (cos 45.)
    // let edgeY = y + r * (sin 45.)
    // let ans = ceil (edgeX + edgeY)
    // puts ans
    x + y + r * (sqrt 2.)
    |> ceil
    |> puts
    ()

main()
writer.Close()
