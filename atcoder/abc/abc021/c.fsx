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

type Edge<'a when 'a: comparison> =
    { from: int
      toward: int
      cost: 'a }

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

type ModInt = MVal of int64

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>] // 型名とモジュール名の重複を許す
module ModInt =
    [<Literal>]
    let Modulo = 1000000007L

    let inline init (x: ^a): ModInt =
        let x = (int64 x) % Modulo
        match x with
        | _ when x < 0L -> MVal(x + Modulo)
        | _ when x >= Modulo -> MVal(x - Modulo)
        | _ -> MVal x

    let zero = init 0
    let one = init 1

    let value (MVal x) = x

    let value2 (x: ModInt) (y: ModInt) = (value x, value y)

    let toString (MVal v): string = sprintf "%d" v

    /// 拡張ユークリッドの互除法
    /// a (mod m) における逆元 a^-1
    let inline inverse (MVal a): ModInt =
        let mutable (a, b, u, v) = (a, Modulo, 1L, 0L)
        while b > 0L do
            let t = a / b
            a <- a - (t * b)
            let tmp = a
            a <- b
            b <- tmp
            u <- u - (t * v)
            let tmp = u
            u <- v
            v <- tmp
        init u

type ModInt with

    static member inline (+) (lhs: ModInt, rhs: ModInt): ModInt =
        let l, r = ModInt.value2 lhs rhs
        let x = l + r
        ModInt.init x

    static member inline (-) (lhs: ModInt, rhs: ModInt): ModInt =
        let l, r = ModInt.value2 lhs rhs
        let x = l - r
        ModInt.init x

    static member inline (*) (lhs: ModInt, rhs: ModInt): ModInt =
        let l, r = ModInt.value2 lhs rhs
        let x = l * r
        ModInt.init x

    /// a / b = a * b^-1 (mod m)
    static member inline (/) (lhs: ModInt, rhs: ModInt): ModInt =
        let r = ModInt.inverse rhs
        lhs * r

    /// a^n (mod m) 繰り返しニ乗法
    /// O(log n)
    static member inline Pow(a: ModInt, e: int64): ModInt =
        let mutable (res, a, e) = (ModInt.one, a, e)
        while e > 0L do
            if (e &&& 1L) <> 0L then res <- res * a
            a <- a * a
            e <- e >>> 1
        res

    /// 符号反転
    static member inline (~-) (x: ModInt): ModInt =
        let v = ModInt.value x
        ModInt.init (-v)

type BiCoef =
    { modulo: int64
      fact: ModInt array
      inv: ModInt array
      finv: ModInt array }

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module BiCoef =

    let inline init (n: ^a) (modulo: ^b): BiCoef =
        let n = int n
        let one = ModInt.one
        let fact = Array.init n (fun _ -> one)
        let inv = Array.init n (fun _ -> one)
        let finv = Array.init n (fun _ -> one)
        let m = modulo |> int
        for i in 2 .. n - 1 do
            fact.[i] <- fact.[i - 1] * (ModInt.init i)
            inv.[i] <- -inv.[m % i] * (ModInt.init (m / i))
            finv.[i] <- finv.[i - 1] * inv.[i]

        { BiCoef.modulo = modulo |> int64
          fact = fact
          inv = inv
          finv = finv }

    let inline com (n: ^a) (k: ^b) (bicoef: BiCoef) =
        let n, k = int n, int k
        let zero = ModInt.zero
        match n, k with
        | _ when n < k -> zero
        | _ when n < 0 -> zero
        | _ when k < 0 -> zero
        | _ ->
            let res = bicoef.fact.[n] * bicoef.finv.[k] * bicoef.finv.[n - k]
            res

type BinaryHeap<'a>(compare: 'a -> 'a -> int) =
    let _heap = ResizeArray<'a>()
    let _compare = compare // 比較関数
    let parent n = (n - 1) / 2
    let leftChild n = (n <<< 1) + 1
    let rightChild n = (n <<< 1) + 2

    let swap x y =
        let tmp = _heap.[x]
        _heap.[x] <- _heap.[y]
        _heap.[y] <- tmp

    /// ここでの比較は昇順ソートを基準に考えている
    let compare x y = (_compare _heap.[x] _heap.[y]) < 0

    /// O(log n)
    member self.Push(x: 'a) =
        let size = _heap.Count
        _heap.Add(x)
        // 親と値を入れ替えていく
        let rec loop k =
            match k > 0 with
            | true ->
                let p = parent k
                match compare k p with
                | true ->
                    swap k p
                    loop p
                | _ -> ()
            | _ -> ()
        loop size

    /// O(log n)
    member self.Pop() =
        let res = _heap.[0]
        // 末尾ノードを根に持ってくる
        let size = _heap.Count - 1
        _heap.[0] <- _heap.[size]
        _heap.RemoveAt(size)
        // 葉ノードに達するまで子と値を入れ替えていく
        let rec loop k =
            let left = leftChild k
            match left < size with
            | true ->
                let right = rightChild k

                let c =
                    if right < size && compare right left then right else left
                match compare c k with
                | true ->
                    swap c k
                    loop c
                | _ -> ()
            | _ -> ()
        loop 0
        res

    member self.Any(): bool = _heap.Count > 0

    member self.Peek(): 'a = _heap.[0]

    member self.Dump() = String.Join(" ", _heap)

// -----------------------------------------------------------------------------------------------------

let main() =
    let N = read int
    let [| A; B |] = reada int |> Array.map (fun x -> x - 1)
    let M = read int
    let graph = WeightedGraph.init N
    for i in 0 .. M - 1 do
        let [| x; y |] = reada int |> Array.map (fun x -> x - 1)
        let e1 = Edge.init x y 1
        let e2 = Edge.init y x 1
        graph.[x].Add(e1)
        graph.[y].Add(e2)

    /// 最短経路数の数え上げ: https://drken1215.hatenablog.com/entry/2018/02/09/003200
    let inline withShortestPathNum (graph: WeightedGraph< ^a >) (startNode: int) (inf: ^a) =
        let nedge = graph |> Array.length
        // nums[v]: 始点(startNode)から頂点vへの最短経路長
        let dist = Array.init nedge (fun _ -> inf)
        dist.[startNode] <- LanguagePrimitives.GenericZero
        // nums[v]: 始点(startNode)から頂点vへの最短経路数
        let nums = Array.init nedge (fun _ -> ModInt.zero)
        nums.[startNode] <- ModInt.one
        let heap = BinaryHeap<Edge<'a>>(Edge.less)
        let start = Edge.initWithoutFrom startNode dist.[startNode]
        heap.Push(start)
        while heap.Any() do
            let from = heap.Pop()
            match dist.[from.toward] < from.cost with
            | true -> ()
            | _ ->
                for edge in graph.[from.toward] do
                    let nextCost = edge.cost + from.cost
                    if dist.[edge.toward] = nextCost then
                        nums.[edge.toward] <- nums.[edge.toward] + nums.[edge.from]
                    elif dist.[edge.toward] > nextCost then
                        dist.[edge.toward] <- nextCost
                        nums.[edge.toward] <- nums.[edge.from]
                        let nextEdge = Edge.initWithoutFrom edge.toward dist.[edge.toward]
                        heap.Push(nextEdge)

        dist, nums


    let inf = Int32.MaxValue
    let dist, nums = withShortestPathNum graph A inf
    let ans = nums.[B]
    ans
    |> ModInt.value
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
ioDispose()
