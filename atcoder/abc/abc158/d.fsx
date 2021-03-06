open System
open System.Collections
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

type VecDeque<'a> =
    {
      /// 先頭要素の位置
      mutable head: int
      /// 末尾要素の位置
      mutable tail: int
      mutable nelem: int
      mutable buf: RingBuffer<'a> }

    /// 実際の要素数
    member inline self.Count = self.nelem

    /// リングバッファの容量が2の冪前提ならば、添字計算を剰余演算から高速な論理積に置き換えられる
    member inline self.Mask = self.buf.Length - 1

    member inline self.IsEmpty() = self.nelem = 0

    member inline self.Any() = self.IsEmpty() |> not

    member inline self.Front =
        match self.Any() with
        | true -> self.buf.[self.head]
        | _ -> raise <| SystemException("deque is empty")

    member inline self.Back =
        match self.Any() with
        | true -> self.buf.[self.tail]
        | _ -> raise <| SystemException("deque is empty")

    member inline private self.PrevIdx(k: int) = (k - 1) &&& self.Mask

    member inline private self.NextIdx(k: int) = (k + 1) &&& self.Mask

    member inline private self.NeedToExtend() = self.Count >= self.buf.Length

    /// O(n)
    member inline private self.Extend() =
        let buf' = Array.zeroCreate (self.buf.Length <<< 1)
        let mutable i = 0
        for e in self do
            buf'.[i] <- e
            i <- i + 1
        do self.head <- 0
           self.tail <- self.nelem - 1
           self.buf <- buf'

    /// O(1)
    member inline self.PushFront(x: 'a) =
        if self.NeedToExtend() then do self.Extend()
        let head = self.PrevIdx(self.head)
        do self.nelem <- self.nelem + 1
           self.head <- head
           self.buf.[head] <- x

    /// O(1)
    member inline self.PushBack(x: 'a) =
        if self.NeedToExtend() then do self.Extend()
        let tail = self.NextIdx(self.tail)
        do self.nelem <- self.nelem + 1
           self.tail <- tail
           self.buf.[tail] <- x

    /// O(1)
    member inline self.PopFront() =
        let res = self.Front
        do self.nelem <- self.nelem - 1
           self.head <- self.NextIdx(self.head)
        res

    /// O(1)
    member inline self.PopBack() =
        let res = self.Back
        do self.nelem <- self.nelem - 1
           self.tail <- self.PrevIdx(self.tail)
        res

    member inline self.Dump() = seq { yield! self } |> fun x -> String.Join(" ", x)

    interface IEnumerable<'a> with
        member self.GetEnumerator() =
            seq {
                let mask = self.Mask
                let mutable i = self.head
                while i <> self.tail do
                    yield self.buf.[i]
                    i <- (i + 1) &&& mask
                yield self.buf.[self.tail]
            }
            |> fun x -> x.GetEnumerator()

    interface System.Collections.IEnumerable with
        member self.GetEnumerator() = (self :> IEnumerable<'a>).GetEnumerator() :> System.Collections.IEnumerator

and RingBuffer<'a> = 'a array

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module VecDeque =
    [<Literal>]
    let InitialLength = 65536

    let inline nextPow2 n =
        let rec loop acc =
            if acc < n then loop (acc <<< 1) else acc
        loop 1

    let inline init() =
        let buf = Array.zeroCreate InitialLength
        { VecDeque.head = 0
          tail = InitialLength - 1
          nelem = 0
          buf = buf }

    /// O(n)
    let inline build (src: 'a seq) =
        let src = Array.ofSeq src
        let len = Array.length src
        let buf = Array.zeroCreate (nextPow2 len)
        src.CopyTo(buf, 0)
        { VecDeque.head = 0
          tail = len - 1
          nelem = len
          buf = buf }

    let inline ofSeq (que: VecDeque< ^a >) = seq { yield! que }

// -----------------------------------------------------------------------------------------------------

let main() =
    let strRev (s: string): string =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

    let S = read string
    let Q = read int
    let que = VecDeque.build S
    let mutable front = 1
    for i in 0 .. Q - 1 do
        let q = reada string
        match q.[0] with
        | "1" -> front <- front ^^^ 1
        | _ ->
            let c = q.[2].[0]
            match q.[1] with
            | "1" ->
                if front = 1 then que.PushFront(c) else que.PushBack(c)
            | _ ->
                if front = 1 then que.PushBack(c) else que.PushFront(c)

    let ans = VecDeque.ofSeq que
    let mutable ans = String.Join("", ans)
    if front = 0 then ans <- strRev ans
    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
