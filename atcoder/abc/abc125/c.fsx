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

type SegTree<'Monoid> =
    {
      /// 実データの要素数(葉ノードの数)
      size: int
      height: int
      /// モノイドの単位元
      unity: 'Monoid
      /// 0-indexed
      nodes: 'Monoid array
      /// 二項演算
      merge: Merge<'Monoid>
      /// 点更新
      change: Change<'Monoid> }

and Merge<'a> = 'a -> 'a -> 'a

and Change<'a> = 'a -> 'a -> 'a

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module SegTree =

    let internal sizeAndHeight n =
        let rec loop sAcc hAcc =
            if sAcc < n then loop (sAcc <<< 1) (hAcc + 1) else (sAcc, hAcc)
        loop 1 0

    let inline internal parent i = (i - 1) / 2
    let inline internal leftChild i = (i <<< 1) + 1
    let inline internal rightChild i = (i <<< 1) + 2
    let inline internal leafIdx tree k = k + tree.size - 1

    let init (n: int) (unity: 'Monoid) (f: Merge<'Monoid>) (g: Change<'Monoid>) =
        let size, height = sizeAndHeight n
        let nodes = Array.init (size * 2 - 1) (fun _ -> unity)
        { SegTree.size = size
          height = height
          unity = unity
          nodes = nodes
          merge = f
          change = g }

    let build (sq: 'Monoid seq) unity f g =
        let sq = Array.ofSeq sq
        let len = Array.length sq
        let tree = init len unity f g
        let nodes = tree.nodes
        // 葉ノードに値を格納
        for i in 0 .. len - 1 do
            let li = leafIdx tree i
            nodes.[li] <- sq.[i]
        // 上にマージしていく
        for i in tree.size - 2 .. -1 .. 0 do
            let lc, rc = leftChild i, rightChild i
            nodes.[i] <- f nodes.[lc] nodes.[rc]
        tree

    /// 一点更新
    let update k x tree: unit =
        let k = leafIdx tree k
        let nodes = tree.nodes
        nodes.[k] <- tree.change nodes.[k] x
        // 子から親に伝搬
        let mutable p = k
        while p > 0 do
            p <- parent p
            let lc, rc = leftChild p, rightChild p
            nodes.[p] <- tree.merge nodes.[lc] nodes.[rc]

    let rec internal queryCore (a: int) (b: int) (k: int) (l: int) (r: int) tree: 'Monoid =
        // 区間外
        if r <= a || b <= l then
            tree.unity
        // 完全被覆
        elif a <= l && r <= b then
            tree.nodes.[k]
        // 一部だけ被覆
        else
            let lc, rc, mid = leftChild k, rightChild k, (l + r) / 2
            let lv = queryCore a b lc l mid tree
            let rv = queryCore a b rc mid r tree
            tree.merge lv rv

    let query a b tree: 'Monoid = queryCore a b 0 0 tree.size tree

    let get k tree =
        let k = leafIdx tree k
        tree.nodes.[k]

/// END CUT HERE

module Gcd =

    let inline gcd (x: ^a) (y: ^a): ^a =
        let zero = LanguagePrimitives.GenericZero

        let rec loop x y =
            if y = zero then x else loop y (x % y)
        loop x y

    let inline lcm (x: ^a) (y: ^a): ^a =
        let g = gcd x y
        x / g * y

let main() =
    let N = read int
    let A = reada int64
    let e = 0L
    let f = Gcd.gcd
    let g x y = y
    let seg = SegTree.build A e f g
    let mutable ans = -1L
    for i in 0 .. N - 1 do
        // i番目だけはぶく
        let l = seg |> SegTree.query 0 i
        let r = seg |> SegTree.query (i + 1) N
        ans <- max ans (Gcd.gcd l r)

    puts ans
    ()

main()
writer.Close()
