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

type UnionFind =
    {
      /// 添字iが属するグループID (0-indexed)
      par: int array
      /// 各集合の要素数
      size: int array }

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module UnionFind =

    /// O(n)
    let init (n: int): UnionFind =
        let par = Array.init n id
        let size = Array.init n (fun _ -> 1)
        { UnionFind.par = par
          size = size }

    /// xの先祖(xが属するグループID)
    let rec root (x: int) (uf: UnionFind): int =
        let par = uf.par
        match x = par.[x] with
        | true -> x
        | false ->
            let px = par.[x]
            par.[x] <- root px uf
            par.[x]

    /// 連結判定
    /// ならし O(α(n))
    let find (x: int) (y: int) (uf: UnionFind) = (root x uf) = (root y uf)

    /// xとyを同じグループに併合
    /// ならし O(α(n))
    let unite (x: int) (y: int) (uf: UnionFind): bool =
        let par, size = uf.par, uf.size
        let rx, ry = root x uf, root y uf
        match rx = ry with
        | true -> false // 既に同じグループ
        | _ ->
            // マージテク(大きい方に小さい方を併合)
            let large, small =
                if size.[rx] < size.[ry] then ry, rx else rx, ry
            par.[small] <- large
            size.[large] <- size.[large] + size.[small]
            size.[small] <- 0
            true

    /// xが属する素集合の要素数
    /// O(1)
    let size (x: int) (uf: UnionFind): int =
        let rx = root x uf
        uf.size.[rx]

    /// 連結成分の個数
    /// O(n)
    let treeNum (uf: UnionFind): int =
        let par = uf.par
        let mutable cnt = 0
        par
        |> Array.iteri (fun i x ->
            if i = x then cnt <- cnt + 1)
        cnt

// -----------------------------------------------------------------------------------------------------

let main() =
    let [| n; m |] = reada int
    let query = Array.zeroCreate m
    for i in 0 .. m - 1 do
        let [| a; b |] = reada int
        let a, b = a - 1, b - 1
        query.[i] <- a, b
    let query = query |> Array.rev
    let uni = UnionFind.init n
    let n = int64 n
    let mutable total = n * (n - 1L) / 2L
    let ans = Array.zeroCreate m
    for i in 0 .. m - 1 do
        ans.[i] <- total
        let a, b = query.[i]
        if UnionFind.find a b uni then
            ()
        else
            let asize, bsize = UnionFind.size a uni, UnionFind.size b uni
            total <- total - (int64 asize * int64 bsize)
        UnionFind.unite a b uni |> ignore

    let ans = ans |> Array.rev
    String.Join("\n", ans) |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
