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

type LazySegTree<'Monoid, 'OpMonoid> =
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
module LazySegTree =
    let internal sizeAndHeight n =
        let rec loop sAcc hAcc =
            if sAcc < n then loop (sAcc <<< 1) (hAcc + 1) else (sAcc, hAcc)
        loop 1 0

    let internal parent i = (i - 1) / 2
    let internal leftChild i = (i <<< 1) + 0
    let internal rightChild i = (i <<< 1) + 1
    let internal leafIdx tree k = k + tree.size

    let init (n: int) f g h (mUnity: 'Monoid) (omUnity: 'OpMonoid): LazySegTree<_, _> =
        let size, height = sizeAndHeight n
        let nodes = Array.init (size * 2) (fun _ -> mUnity)
        let lazyNodes = Array.init (size * 2) (fun _ -> omUnity)
        { LazySegTree.size = size
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
    let update (l: int) (r: int) (x: 'OpMonoid) (tree: LazySegTree<'Monoid, 'OpMonoid>): 'Monoid =
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
    let N = read int

    let inf = 1e12 |> int64
    let e = (-1, 0L, inf)

    let ab = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let [| a; b |] = reada int64
        ab.[i] <- (i + 1, a, b)

    let f x y =
        let _, xh, xa = x
        let _, yh, ya = y
        let xtoy = (yh + xa - 1L) / xa
        let ytox = (xh + ya - 1L) / ya
        xtoy < ytox

    // まず最強候補をひとつ決めうちする
    let mutable ans = ab.[0]
    for i in 1 .. N - 1 do
        if f ans ab.[i] then () else ans <- ab.[i]

    // 最強候補が本当に最強なのか(自分以外の全員に勝てるか)調べる
    let mutable ok = true
    let ai, _, _ = ans
    for i in 0 .. N - 1 do
        if i = ai - 1 then ()
        else if f ans ab.[i] |> not then ok <- false

    if ok then ai else -1
    |> puts
    ()

main()
writer.Close()
