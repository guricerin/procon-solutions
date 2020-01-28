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

type LazySegmentTree<'Monoid, 'OpMonoid> =
    {
      /// 実データの要素数
      size: int
      /// 木の高さ
      height: int
      /// モノイドの単位元
      mUnity: 'Monoid
      /// 作用素の単位元
      omUnity: 'OpMonoid
      nodes: 'Monoid array
      lazyNodes: 'OpMonoid array
      f: F<'Monoid>
      g: G<'Monoid, 'OpMonoid>
      h: H<'OpMonoid> }

and F<'Monoid> = 'Monoid -> 'Monoid -> 'Monoid

and G<'Monoid, 'OpMonoid> = 'Monoid -> 'OpMonoid -> 'Monoid

and H<'OpMonoid> = 'OpMonoid -> 'OpMonoid -> 'OpMonoid

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module LazySegmentTree =
    let internal sizeAndHeight n =
        let rec loop sAcc hAcc =
            if sAcc < n then loop (sAcc <<< 1) (hAcc + 1) else (sAcc, hAcc)
        loop 1 0

    let internal parent i = (i - 1) / 2
    let internal leftChild i = (i <<< 1) + 0
    let internal rightChild i = (i <<< 1) + 1
    let internal leafIdx tree k = k + tree.size

    let init (n: int) f g h (mUnity: 'Monoid) (omUnity: 'OpMonoid): LazySegmentTree<_, _> =
        let size, height = sizeAndHeight n
        let nodes = Array.init (size * 2) (fun _ -> mUnity)
        let lazyNodes = Array.init (size * 2) (fun _ -> omUnity)
        { LazySegmentTree.size = size
          height = height
          mUnity = mUnity
          omUnity = omUnity
          nodes = nodes
          lazyNodes = lazyNodes
          f = f
          g = g
          h = h }

    /// k番目(0-indexed)の要素をxに更新
    let set (k: int) x tree: unit =
        let size = tree.size
        let idx = leafIdx tree k
        tree.nodes.[idx] <- x

    /// k番目のノードについて遅延評価を行う
    let internal apply (k: int) tree =
        let nodes, lazyNodes = tree.nodes, tree.lazyNodes
        let omUnity = tree.omUnity
        let g = tree.g
        match lazyNodes.[k] = omUnity with
        | true -> nodes.[k]
        | false -> g nodes.[k] lazyNodes.[k]

    /// 親から子への遅延伝播
    let internal propagate (k: int) tree: unit =
        let nodes, lazyNodes = tree.nodes, tree.lazyNodes
        let omUnity = tree.omUnity
        let lc, rc = leftChild k, rightChild k
        let h = tree.h
        let g = tree.g
        if lazyNodes.[k] <> omUnity then
            if k < tree.size then
                lazyNodes.[lc] <- h lazyNodes.[lc] lazyNodes.[k]
                lazyNodes.[rc] <- h lazyNodes.[rc] lazyNodes.[k]
            // nodes.[k] <- apply k tree
            nodes.[k] <- g nodes.[k] lazyNodes.[k]
            lazyNodes.[k] <- omUnity

    let rec internal updateCore a b (x: 'OpMonoid) k l r tree =
        propagate k tree
        let nodes = tree.nodes
        let lazyNodes = tree.lazyNodes
        let f = tree.f
        let h = tree.h
        if r <= a || b <= l then
            nodes.[k]
        elif a <= l && r <= b then
            lazyNodes.[k] <- h lazyNodes.[k] x
            propagate k tree
            nodes.[k]
        else
            let lc, rc, mid = leftChild k, rightChild k, (l + r) >>> 1
            let lm = updateCore a b x lc l mid tree
            let rm = updateCore a b x rc mid r tree
            nodes.[k] <- f lm rm
            nodes.[k]

    /// 半開区間[l, r)を更新する
    let update (l: int) (r: int) (x: 'OpMonoid) (tree: LazySegmentTree<_, _>): 'Monoid =
        updateCore l r x 1 0 tree.size tree

    let rec internal queryCore (a: int) (b: int) (k: int) (l: int) (r: int) tree: 'Monoid =
        propagate k tree
        let mUnity = tree.mUnity
        let f = tree.f
        if r <= a || b <= l then
            mUnity
        elif a <= l && r <= b then
            tree.nodes.[k]
        else
            let lc, rc, mid = leftChild k, rightChild k, (l + r) >>> 1

            let lm = queryCore a b lc l mid tree
            let rm = queryCore a b rc mid r tree
            f lm rm

    /// 半開区間[l, r)の演算結果
    let query (l: int) (r: int) tree: 'Monoid = queryCore l r 1 0 tree.size tree

    /// k番目の要素を取得
    let get (k: int) tree = query k (k + 1) tree

    let dump tree =
        let size = tree.size
        let nodes = tree.nodes.[0..size - 1]
        let mutable buf = ""
        for a in nodes do
            buf <- sprintf "%s%A " buf a
        buf


let main() =
    let [| N; D; A |] = reada int64
    let N = int N
    let xhs = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let [| x; h |] = reada int64
        xhs.[i] <- (x, h)
    let xhs = xhs |> Array.sortBy (fun (x, h) -> x)

    let f x y = x + y
    let seg = LazySegmentTree.init N f f f 0L 0L
    let mutable cnt = 0L
    for i in 0 .. N - 1 do
        let x, h = xhs.[i]
        let need = (h + A - 1L) / A - LazySegmentTree.get i seg
        let need = max 0L need
        let mutable (ng, ok) = (-1, N)
        while abs (ok - ng) > 1 do
            let mid = (ok + ng) / 2
            let midX = xhs.[mid] |> fst
            if midX > x + 2L * D then ok <- mid else ng <- mid
        let right = ok
        LazySegmentTree.update i right need seg |> ignore
        cnt <- cnt + need

    cnt |> puts
    ()

main()
writer.Close()
