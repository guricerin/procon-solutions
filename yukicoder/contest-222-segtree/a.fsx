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

let main() =
    let [| N; Q |] = reada int
    let A = reada int |> Array.indexed

    let f x y =
        let xi, xv = x
        let yi, yv = y
        if xv < yv then x else y

    let g x y = y

    let e = Int32.MaxValue - 1, Int32.MaxValue - 1
    let seg = SegTree.build A e f g

    for _ in 0 .. Q - 1 do
        let [| q; l; r |] = reada int
        let l, r = l - 1, r - 1
        match q with
        | 1 ->
            let li, lv = seg |> SegTree.get l
            let ri, rv = seg |> SegTree.get r
            seg |> SegTree.update l (l, rv)
            seg |> SegTree.update r (r, lv)
        | _ ->
            let si, sv = seg |> SegTree.query l (r + 1)
            puts (si + 1)

    ()

main()
writer.Close()
