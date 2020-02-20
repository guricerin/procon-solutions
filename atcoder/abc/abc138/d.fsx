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

type UnWeightedGraph = ResizeArray<int> array

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module UnWeightedGraph =
    let inline init (n: int): UnWeightedGraph = Array.init n (fun _ -> ResizeArray<int>())

// -----------------------------------------------------------------------------------------------------

let main() =
    let [| n; q |] = reada int
    let tree = UnWeightedGraph.init n
    for i in 0 .. n - 2 do
        let [| a; b |] = reada int
        let a, b = a - 1, b - 1
        tree.[a].Add(b)
        tree.[b].Add(a)
    let sums = Array.zeroCreate n
    for _ in 1 .. q do
        let [| p; x |] = reada int
        let p, x = p - 1, int64 x
        sums.[p] <- sums.[p] + x

    let rec dfs p v =
        for nv in tree.[v] do
            // 親方向に逆戻りするのを防止
            if nv <> p then
                sums.[nv] <- sums.[nv] + sums.[v]
                dfs v nv
    // 加算処理を根から開始
    dfs 0 0
    String.Join(" ", sums) |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
