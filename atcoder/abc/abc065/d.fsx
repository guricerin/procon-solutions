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
      cost: 'T }

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Edge =
    open System

    let init (from: int) (toward: int) (cost: 'a): Edge<'a> =
        { Edge.from = from
          toward = toward
          cost = cost }

    let initWithoutFrom (toward: int) (cost: 'a): Edge<'a> = init -1 toward cost

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

    /// クラスカル法基本形
    /// n: 頂点数
    /// O(E log V)
    let inline main (edges: Edges< ^a >) (n: int) =
        // 辺をコストが小さい順に見ていく貪欲法
        edges.Sort(Edge.less)
        let uni = UnionFind.init n
        let mutable res = LanguagePrimitives.GenericZero
        for e in edges do
            // 辺を追加した場合に閉路ができなければ、その辺を採用
            if uni.Unite(e.from, e.toward) then res <- res + e.cost
        res

// -----------------------------------------------------------------------------------------------------

let main() =
    let N = read int
    let X = Array.zeroCreate N
    let Y = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let [| x; y |] = reada int
        X.[i] <- x
        Y.[i] <- y

    // 愚直にグラフを構築しようとすると完全グラフとなり、辺の数が N^2 となってしまう
    // よって、考慮すべき辺の数を減らしたい
    // コストの定義より、x座標かy座標が隣同士でないノード間に辺を貼る必要はない（隣同士の場合のコストよりも確実にでかくなるから）
    let edges = Edges.init (2 * N)
    let ids = [| 0 .. N - 1 |]
    // 座標でソートすることで、ソート後の隣の番号同士はグラフの中でx座標が最も近いノード同士であることを表現
    let xid = ids |> Array.sortBy (fun i -> X.[i])
    for i in 0 .. N - 2 do
        let u, v = xid.[i], xid.[i + 1] // 隣同士しか見る必要なし

        let cost =
            X.[u] - X.[v]
            |> abs
            |> int64

        let e = Edge.init u v cost
        edges.Add(e)
    let yid = ids |> Array.sortBy (fun i -> Y.[i])
    for i in 0 .. N - 2 do
        let u, v = yid.[i], yid.[i + 1]

        let cost =
            Y.[u] - Y.[v]
            |> abs
            |> int64

        let e = Edge.init u v cost
        edges.Add(e)

    let ans = Kruskal.main edges N
    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
