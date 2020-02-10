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
    let s = read string
    let len = s.Length
    let st = SortedSet<string>()

    let rec dfs l r (t: string) =
        if l >= len || r < 0 || t.Length >= len then
            // printfn "%s" t
            st.Add(t) |> ignore
        else
            let a = t + string s.[l]
            dfs (l + 1) r a
            let b = t + string s.[r]
            dfs l (r - 1) b

    dfs 0 (len - 1) ""
    st.Count |> puts

    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
