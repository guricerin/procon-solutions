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

    /// xの先祖(xが属するグループID)
    member self.Root(x: int) =
        let par = self.par

        let rec loop x =
            match x = par.[x] with
            | true -> x
            | false ->
                let px = par.[x]
                par.[x] <- loop px
                par.[x]
        loop x

    /// 連結判定
    /// ならし O(α(n))
    member self.Find(x: int, y: int) = self.Root(x) = self.Root(y)

    /// xとyを同じグループに併合
    /// ならし O(α(n))
    member self.Unite(x: int, y: int): bool =
        let par, size = self.par, self.size
        let rx, ry = self.Root(x), self.Root(y)
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
    member self.Size(x: int): int =
        let rx = self.Root(x)
        self.size.[rx]

    /// 連結成分の個数
    /// O(n)
    member self.TreeNum: int =
        let par = self.par
        let mutable cnt = 0
        par
        |> Array.iteri (fun i x ->
            if i = x then cnt <- cnt + 1)
        cnt

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module UnionFind =

    /// O(n)
    let init (n: int): UnionFind =
        let par = Array.init n id
        let size = Array.init n (fun _ -> 1)
        { UnionFind.par = par
          size = size }

type Edge<'T when 'T: comparison> =
    { from: int
      toward: int
      cost: 'T
      label: int }

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Edge =
    open System

    let init (from: int) (toward: int) (cost: 'a) (label: int): Edge<'a> =
        { Edge.from = from
          toward = toward
          cost = cost
          label = label }

    let initWithoutFrom (toward: int) (cost: 'a): Edge<'a> = init -1 toward cost -1

    /// 昇順
    let less (x: Edge<'a>) (y: Edge<'a>) = (x.cost :> IComparable<_>).CompareTo(y.cost)

    /// 降順
    let greater (x: Edge<'a>) (y: Edge<'a>) = (y.cost :> IComparable<_>).CompareTo(x.cost)

/// 重み付き辺集合
type Edges<'a when 'a: comparison> = ResizeArray<Edge<'a>>

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Edges =
    let inline init (n: int): Edges<'a> = ResizeArray<Edge<'a>>(n)

/// 重み付きグラフ
/// g.[a] => ノードaに直接繋がっている辺群(隣接リスト)
type WeightedGraph<'a when 'a: comparison> = Edges<'a> array

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module WeightedGraph =
    let inline init (n: int): WeightedGraph<'a> = Array.init n (fun _ -> ResizeArray<Edge<'a>>())

/// 重みなしグラフ
/// g.[a] => ノードaと辺で直接繋がっているノード群(隣接リスト)
/// コストは一律 0 or 1
type UnWeightedGraph = ResizeArray<int> array

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module UnWeightedGraph =
    let inline init (n: int): UnWeightedGraph = Array.init n (fun _ -> ResizeArray<int>())

[<RequireQualifiedAccess>]
module Kruskal =

    /// 辺をコストが小さい順に見ていき、閉路ができなければ追加していく貪欲法
    /// n: 頂点数
    /// O(E log V)
    let inline main (edges: Edges<'a>) (n: int) =
        edges.Sort(Edge.less)
        let uni = UnionFind.init n
        let mutable res = LanguagePrimitives.GenericZero
        for e in edges do
            if uni.Unite(e.from, e.toward) then res <- res + e.cost
        res

// -----------------------------------------------------------------------------------------------------

let main() =
    let [| N; M |] = reada int
    let edges = Edges.init N
    for i in 0 .. M - 1 do
        let [| a; b; c |] = reada int
        let a, b = a - 1, b - 1
        let c = int64 c
        let e = Edge.init a b c (i + 1)
        edges.Add(e)

    edges.Sort(Edge.greater)
    let uni = UnionFind.init N
    let mutable total = 0L
    let ans = ResizeArray<int>()
    for e in edges do
        if uni.Unite(e.from, e.toward) then
            total <- total + e.cost
            ans.Add(e.label)

    ans.Sort()
    String.Join("\n", ans) |> puts

    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
