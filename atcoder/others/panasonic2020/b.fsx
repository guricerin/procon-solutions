open System
open System.Collections.Generic

[<AutoOpen>]
module FastIO =
    let reader = new IO.StreamReader(new IO.BufferedStream(Console.OpenStandardInput()))
    let inline read f = reader.ReadLine() |> f
    let inline reada f = reader.ReadLine().Split() |> Array.map f
    let inline readChars() = read string |> Seq.toArray
    let inline readInts() = readChars() |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

    let writer = new IO.StreamWriter(new IO.BufferedStream(Console.OpenStandardOutput()))
    let inline write (s: string) = writer.Write s
    let inline writeln (s: string) = writer.WriteLine s
    let inline puts (s: ^a) = string s |> writeln

    let inline ioDispose() =
        reader.Dispose()
        writer.Dispose()

// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------

let main() =
    let [| h; w |] = reada int64
    if h = 1L || w = 1L then
        puts 1
    else
        let hw = h * w
        let ans = (hw + 2L - 1L) / 2L
        puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
ioDispose()
ioDispose()
