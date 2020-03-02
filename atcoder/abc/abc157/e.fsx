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

    /// O(n)
    let init (n: int) (unity: 'Monoid) (f: Merge<'Monoid>) (g: Change<'Monoid>) =
        let size, height = sizeAndHeight n
        let nodes = Array.init (size * 2 - 1) (fun _ -> unity)
        { SegTree.size = size
          height = height
          unity = unity
          nodes = nodes
          merge = f
          change = g }

    /// O(n)
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

    let rec internal foldCore (a: int) (b: int) (k: int) (l: int) (r: int) tree: 'Monoid =
        // 区間外
        if r <= a || b <= l then
            tree.unity
        // 完全被覆
        elif a <= l && r <= b then
            tree.nodes.[k]
        // 一部だけ被覆
        else
            let lc, rc, mid = leftChild k, rightChild k, (l + r) / 2
            let lv = foldCore a b lc l mid tree
            let rv = foldCore a b rc mid r tree
            tree.merge lv rv


type SegTree<'Monoid> with

    /// 一点更新
    /// O(log n)
    member self.Update(k, x): unit =
        let k = SegTree.leafIdx self k
        let nodes = self.nodes
        nodes.[k] <- self.change nodes.[k] x
        // 子から親に伝搬
        let rec loop k =
            if k > 0 then
                let p = SegTree.parent k
                let lc, rc = SegTree.leftChild p, SegTree.rightChild p
                nodes.[p] <- self.merge nodes.[lc] nodes.[rc]
                loop p
            else
                ()
        loop k

    /// O(log n)
    member self.Fold(a: int, b: int): 'Monoid = SegTree.foldCore a b 0 0 self.size self

    /// O(1)
    member self.At(k: int) =
        let k = SegTree.leafIdx self k
        self.nodes.[k]

// -----------------------------------------------------------------------------------------------------

let popcount (n: int) =
    let n, msb =
        if n < 0 then n &&& Int32.MaxValue, 1 else n, 0

    let n = n - ((n >>> 1) &&& 0x55555555)
    let n = (n &&& 0x33333333) + ((n >>> 2) &&& 0x33333333)
    (((n + (n >>> 4) &&& 0xF0F0F0F) * 0x1010101) >>> 24) + msb

let main() =
    let N = read int

    // 文字の集合をビットで表す
    // アルファベットなので高々 2^26
    let S = readChars() |> Array.map ((fun c -> int c - int 'a') >> (fun x -> 1 <<< x))

    let seg = SegTree.build S 0 (|||) (fun a b -> b)
    let Q = read int
    for _ in 0 .. Q - 1 do
        let [| q; i; c |] = reada string
        match q with
        | "1" ->
            let i = Convert.ToInt32(i) - 1
            let c = c.[0]
            let y = int c - int 'a'
            let y = (1 <<< y)
            seg.Update(i, y)
        | _ ->
            let l, r = Convert.ToInt32(i), Convert.ToInt32(c)
            let a = seg.Fold(l - 1, r)
            puts (popcount a)
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
