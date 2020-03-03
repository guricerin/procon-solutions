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

type BitSet =
    { value: uint64
      /// 下位ビット（二進文字列的には右端）から0-indexed
      width: int }

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module BitSet =

    /// ビットが立っている数
    /// 参照元：https://qiita.com/zawawahoge/items/8bbd4c2319e7f7746266
    let inline popcount (x: ^a): int =
        let x = uint64 x
        let x = x - ((x >>> 1) &&& 0x5555555555555555UL)
        let x = (x &&& 0x3333333333333333UL) + ((x >>> 2) &&& 0x3333333333333333UL)
        let x = (x + (x >>> 4)) &&& 0x0f0f0f0f0f0f0f0fUL
        let x = x + (x >>> 8)
        let x = x + (x >>> 16)
        let x = x + (x >>> 32)
        x &&& 0x0000007fUL |> int

    /// 二進表現におけるxのposビット目が立っているか
    let inline test (x: ^a) (pos: int): bool =
        let x = uint64 x
        x &&& (1UL <<< pos) <> 0UL

    /// xのposビット目をvに変更したものを返す
    let inline set (x: ^a) (pos: int) (v: int) =
        let x = uint64 x
        match v with
        | 0 -> x &&& ~~~(1UL <<< pos)
        | _ -> x ||| (1UL <<< pos)

    let inline flipAll (x: ^a) (width: int) =
        let x = ~~~(uint64 x)
        x &&& (1UL <<< width) - 1UL

    let inline init (w: int) (v: ^a) =
        let v = uint64 v
        let v = v &&& (1UL <<< w) - 1UL // 指定したビット幅を超える分は切り捨てる
        { BitSet.value = uint64 v
          width = w }

    let inline ofInt (v: int) =
        let w =
            (log (float v)) / (log 2.)
            |> int
            |> (+) 1
        init w v

    /// 頭が"0b"始まりでもok
    let inline ofBin (str: string) =
        let str, width =
            match str.StartsWith("0b") with
            | true -> str.Substring(2), str.Length - 2
            | _ -> str, str.Length

        let v = Convert.ToUInt64(str, 2)
        init width v

    let inline toBin (x: BitSet) =
        let v = int64 x.value
        let width = x.width
        let res = Convert.ToString(v, 2).PadLeft(width, '0')
        match res.Length <= width with
        | true -> res
        | false -> res.Substring(res.Length - width)

    let inline toHex (x: ^a) = sprintf "0x%x" x

    /// O(n)
    let inline toArray (x: BitSet) =
        let res = Array.zeroCreate x.width
        for i in 0 .. x.width - 1 do
            res.[i] <- test x.value i
        res |> Array.rev

type BitSet with

    static member inline (+) (x: BitSet, y: BitSet) =
        do x.SameWidth(y)
        { x with value = x.value + y.value }

    static member inline (+) (x: BitSet, y: int) = { x with value = x.value + uint64 y }

    static member inline (<<<) (x: BitSet, shift: int) =
        let v = x.value <<< shift
        let v = v &&& ((1UL <<< x.width) - 1UL)
        { x with value = v }

    static member inline (>>>) (x: BitSet, shift: int) =
        let v = x.value >>> shift
        let v = v &&& ((1UL <<< x.width) - 1UL)
        { x with value = v }

    static member inline (~~~) (x: BitSet) = x.Flip()

    static member inline (&&&) (x: BitSet, y: BitSet) =
        do x.SameWidth(y)
        let v = x.value &&& y.value
        { x with value = v }

    static member inline (|||) (x: BitSet, y: BitSet) =
        do x.SameWidth(y)
        let v = x.value ||| y.value
        { x with value = v }

    static member inline (^^^) (x: BitSet, y: BitSet) =
        do x.SameWidth(y)
        let v = x.value ^^^ y.value
        { x with value = v }

    /// index access
    member self.Item
        with get (idx) =
            do self.Check(idx)
            BitSet.test self.value idx

    member self.Popcount() = BitSet.popcount self.value

    member self.Any(): bool = self.Popcount() > 0

    member self.None(): bool = self.Popcount() = 0

    member private self.Check(pos: int) =
        let width = self.width
        if width <= pos then
            let msg = sprintf "pos:(%d) >= width:(%d)" pos width
            invalidArg "pos" msg

    member self.SameWidth(other: BitSet) =
        if self.width <> other.width then
            let msg = sprintf "x.width:(%d) != y.width:(%d)" self.width other.width
            invalidArg "pos" msg

    member self.Set(pos: int, x: int) =
        do self.Check(pos)
        let v = BitSet.set self.value pos x
        { self with value = v }

    member self.Reset() = { self with value = 0UL }

    member self.Flip() =
        let v = BitSet.flipAll self.value self.width
        { self with value = v }

    member self.Flip(pos: int) =
        do self.Check(pos)
        let bit =
            if BitSet.test self.value pos then 0 else 1
        self.Set(pos, bit)

    member self.ToHex() = BitSet.toHex self.value

    member self.ToBin() = BitSet.toBin self

    member self.ToArray() = BitSet.toArray self

// -----------------------------------------------------------------------------------------------------

let main() =
    let N = read int

    let S =
        readChars()
        |> Array.map
            ((fun c -> int c - int 'a')
             >> (fun x -> 1 <<< x)
             >> (fun x -> BitSet.init 26 x))

    let seg = SegTree.build S (BitSet.init 26 0) (|||) (fun _ y -> y)
    let Q = read int
    for _ in 0 .. Q - 1 do
        let [| q; i; c |] = reada string
        match q with
        | "1" ->
            let i = Convert.ToInt32(i)
            let c = c.[0]
            let y = int c - int 'a'
            let y = BitSet.init 26 (1 <<< y)
            seg.Update(i - 1, y)
        | _ ->
            let l, r = Convert.ToInt32(i), Convert.ToInt32(c)
            let bs = seg.Fold(l - 1, r)
            bs.Popcount() |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
