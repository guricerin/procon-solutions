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

type ModInt = MVal of int64

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>] // 型名とモジュール名の重複を許す
module ModInt =
    let modulo = (int64 1e9) + 7L

    let inline init (x: ^a): ModInt =
        let x = (int64 x) % modulo
        match x with
        | _ when x < 0L -> MVal(x + modulo)
        | _ when x >= modulo -> MVal(x - modulo)
        | _ -> MVal x

    let zero = init 0
    let one = init 1

    let value (MVal x) = x

    let value2 (x: ModInt) (y: ModInt) = (value x, value y)

    let toString (MVal v): string = sprintf "%d" v

    /// 拡張ユークリッドの互除法
    /// a (mod m) における逆元 a^-1
    let inline inverse (MVal a): ModInt =
        let mutable (a, b, u, v) = (a, modulo, 1L, 0L)
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
        let m = ModInt.modulo
        let l, r = ModInt.value2 lhs rhs
        let x = l + r
        match x with
        | _ when x >= m -> ModInt.init (x - m)
        | _ -> ModInt.init x

    static member inline (-) (lhs: ModInt, rhs: ModInt): ModInt =
        let m = ModInt.modulo
        let l, r = ModInt.value2 lhs rhs
        let x = l - r
        match x with
        | _ when x < 0L -> ModInt.init (x + m)
        | _ -> ModInt.init x

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
    static member inline Pow(a: ModInt, n: int64): ModInt =
        let mutable (res, a, n) = (ModInt.init 1, a, n)
        while n > 0L do
            if (n &&& 1L) = 1L then res <- res * a
            a <- a * a
            n <- n >>> 1
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
        let one = ModInt.init 1
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

let main() =
    let [| N; K |] = reada int

    let A =
        reada int
        |> Array.sort
        |> Array.map ModInt.init

    let bc = BiCoef.init (N + 10) ModInt.modulo
    let mutable ans = ModInt.zero
    for i in 0 .. N - 2 do
        let fac = (BiCoef.com N K bc) - (BiCoef.com (i + 1) K bc) - (BiCoef.com (N - (i + 1)) K bc)
        ans <- ans + fac * (A.[i + 1] - A.[i])
    ans
    |> ModInt.value
    |> puts
    ()

main()
writer.Close()
writer.Close()
