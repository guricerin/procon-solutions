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

type UnionFind =
    {
      /// 添字iが属するグループID
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

    /// x: 0-indexed
    let rec root (x: int) (uf: UnionFind): int =
        let par = uf.par
        match x = par.[x] with
        | true -> x
        | false ->
            let px = par.[x]
            par.[x] <- root px uf
            par.[x]

    /// 連結判定
    let find (x: int) (y: int) (uf: UnionFind) = (root x uf) = (root y uf)

    let unite (x: int) (y: int) (uf: UnionFind): bool =
        let par, size = uf.par, uf.size
        let rx, ry = root x uf, root y uf
        match rx = ry with
        | true -> false
        | _ ->
            // マージテク(大きい方に小さい方を併合)
            let large, small =
                if size.[rx] < size.[ry] then ry, rx else rx, ry
            par.[small] <- large
            size.[large] <- size.[large] + size.[small]
            size.[small] <- 0
            true

    /// 素集合のサイズ
    /// x: 0-indexed
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

let main() =
    let n = read int
    let uf = UnionFind.init n
    let degs = Array.zeroCreate n
    for i in 0 .. n - 2 do
        let [| u; v |] = reada int
        UnionFind.unite u v uf |> ignore
        degs.[u] <- degs.[u] + 1
        degs.[v] <- degs.[v] + 1

    // アリスが爆破する橋と、ボブが作る橋は同じじゃなくて良い
    // 爆破する橋と作る橋はそれぞれ1本だけ
    // 連結成分を1つだけにできればボブの勝ち
    let num = uf |> UnionFind.treeNum
    let A = "Alice"
    let B = "Bob"
    if num >= 3 then
        A
    elif num = 2 then
        // 爆破すると連結成分が増える辺があればアリスの勝ち
        let ok = degs |> Array.tryFind (fun x -> x = 1)
        match ok with
        | Some _ -> A
        | _ -> B
    else
        B
    |> puts
    ()

main()
writer.Close()
