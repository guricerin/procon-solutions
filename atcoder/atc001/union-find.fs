open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

let writer = new IO.StreamWriter(new IO.BufferedStream(Console.OpenStandardOutput ()))
let write (s: string) = writer.Write s
let writeln (s: string) = writer.WriteLine s

module Util =
    let strRev (s: string): string =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

    let inline roundup (x: ^a) (y: ^a): ^a =
        let one = LanguagePrimitives.GenericOne
        (x + y - one) / y

type UnionFind = {
    par : int array
    rank : int array
}

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module UnionFind =

    let inline init (n: ^a) =
        let n = int n
        let par = Array.init n id
        let rank = Array.zeroCreate n
        {
            UnionFind.par = par
            rank = rank
        }

    let rec root x uf =
        let par = uf.par
        match x = par.[x] with
        | true -> x
        | false ->
            let px = par.[x]
            par.[x] <- root px uf
            par.[x]

    let find x y uf =
        (root x uf) = (root y uf)

    let unite x y uf =
        let par, rank = uf.par, uf.rank
        let rx, ry = root x uf, root y uf
        match rx = ry with
        | true -> false
        | false ->
            let large, small =
                if rank.[rx] < rank.[ry] then ry, rx
                else rx, ry
            par.[small] <- large
            rank.[large] <- rank.[large] + rank.[small]
            rank.[small] <- 0
            true

[<EntryPoint>]
let main _ =
    let [|N;Q|] = reada int
    let uni = UnionFind.init N

    for _ in 0..Q-1 do
        let [|p;a;b|] = reada int
        match p with
        | _ when p = 0 ->
            UnionFind.unite a b uni |> ignore
        | _ when p = 1 ->
            if UnionFind.find a b uni then "Yes" else "No"
            |> writeln
        | _ -> failwithf "hoge"

    writer.Close()
    0 // return an integer exit code
