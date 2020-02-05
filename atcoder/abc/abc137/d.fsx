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

type PriorityQueue<'T>(compare: 'T -> 'T -> int) =
    let _heap = ResizeArray<'T>() // 二分ヒープ
    let _compare = compare // 比較関数
    let parent n = (n - 1) / 2
    let leftChild n = (n <<< 1) + 1
    let rightChild n = (n <<< 1) + 2

    let swap x y =
        let tmp = _heap.[x]
        _heap.[x] <- _heap.[y]
        _heap.[y] <- tmp

    /// ここでの比較は昇順ソートを基準に考えている
    member self.Compare(x: int, y: int) = (_compare _heap.[x] _heap.[y]) < 0

    /// O(log n)
    member self.Enque(x: 'T) =
        let size = _heap.Count
        _heap.Add(x)
        // 親と値を入れ替えていく
        let rec loop k =
            match k > 0 with
            | true ->
                let p = parent k
                match self.Compare(k, p) with
                | true ->
                    swap k p
                    loop p
                | _ -> ()
            | _ -> ()
        loop size

    /// O(log n)
    member self.Deque() =
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
                    if right < size && self.Compare(right, left)
                    then right
                    else left
                match self.Compare(c, k) with
                | true ->
                    swap c k
                    loop c
                | _ -> ()
            | _ -> ()
        loop 0
        res

    member self.Any(): bool = _heap.Count > 0

    member self.Peek(): 'T = _heap.[0]

    member self.Dump() = String.Join(" ", _heap)

let greater (x: int64) (y: int64) = y.CompareTo(x)

let main() =
    let [| n; m |] = reada int
    let len = int 1e5 + 10
    // ab.[i] : i日後に振り込まれるアルバイト群
    let ab = Array.init len (fun _ -> ResizeArray<int64>())
    for i in 0 .. n - 1 do
        let [| a; b |] = reada int
        let b = int64 b
        ab.[a].Add(b)

    let mutable ans = 0L
    let que = PriorityQueue<int64>(greater)
    // スケジュールの後ろから、短期かつ報酬の良いバイトをつめていく
    for d in 1 .. m do
        // d日後に振り込まれるバイトを候補入りさせる
        for b in ab.[d] do
            que.Enque(b)
        // 報酬のよいバイトをこなす
        if que.Any() then
            let b = que.Deque()
            ans <- ans + b

    puts ans
    ()

main()
writer.Close()
