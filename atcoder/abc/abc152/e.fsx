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

module Gcd =

    let inline gcd (x: ^a) (y: ^a): ^a =
        let zero = LanguagePrimitives.GenericZero

        let rec loop x y =
            if y = zero then x else loop y (x % y)
        loop x y

    let inline lcm (x: ^a) (y: ^a): ^a =
        let g = gcd x y
        x / g * y

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
        let l, r = ModInt.value2 lhs rhs
        let x = l + r
        ModInt.init x

    static member inline (-) (lhs: ModInt, rhs: ModInt): ModInt =
        let l, r = ModInt.value2 lhs rhs
        let x = l - r
        ModInt.init x

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
    static member inline Pow(a: ModInt, e: int64): ModInt =
        let mutable (res, a, e) = (ModInt.one, a, e)
        while e > 0L do
            if (e &&& 1L) <> 0L then res <- res * a
            a <- a * a
            e <- e >>> 1
        res

    /// 符号反転
    static member inline (~-) (x: ModInt): ModInt =
        let v = ModInt.value x
        ModInt.init (-v)

module Prime =

    let inline isPrime (n: int) =
        let limit =
            n
            |> float
            |> sqrt
            |> int
        seq {
            for p in 2 .. limit do
                if n % p = 0 then yield ()
        }
        |> Seq.isEmpty

    /// nを素因数分解した結果を返す
    let primeFactors (n: int64): Map<int64, int64> =
        let limit =
            n
            |> float
            |> sqrt
            |> int64
        let rec count x p acc =
            if x % p = 0L then count (x / p) p (acc + 1L) else acc

        let mutable n = n

        let res =
            seq {
                for p in 2L .. limit + 1L do
                    let c = count n p 0L
                    if c <> 0L then
                        let div = (float p) ** (float c) |> int64
                        n <- n / div
                        yield (p, c)
            }
            |> Map.ofSeq
        if n = 1L then res else res.Add(n, 1L)

    /// nの約数の個数
    let divisorsCount (n: int64): int64 = primeFactors n |> Map.fold (fun acc k v -> acc * (v + 1L)) 1L

    /// upper以下の素数を列挙
    let sieveToUpper upper =
        seq {
            yield 2
            let knownComposites = System.Collections.Generic.SortedSet<int>()
            for i in 3 .. 2 .. upper do
                let found = knownComposites.Contains(i)
                if not found then yield i
                for j in i .. i .. upper do
                    knownComposites.Add(j) |> ignore
        }

    /// 添え字が素数かどうかを表す配列を返す
    let eraSieve upper =
        let upper = int upper + 1
        let res = Array.init upper (fun _ -> true)
        res.[0] <- false
        res.[1] <- false
        let rec loop a b =
            let c = a * b
            if c >= upper then
                ()
            else
                res.[c] <- false
                loop a (b + 1)
        for i in 2 .. upper - 1 do
            if res.[i] then loop i 2
        res

    /// nの約数を列挙(n自身を含む)
    /// O(log n)
    let divisors (n: int64): int64 array =
        let lim =
            n
            |> float
            |> sqrt
            |> int64
        seq {
            for i in 1L .. lim do
                if n % i = 0L then
                    yield i
                    if i * i <> n then yield n / i
        }
        |> Array.ofSeq
        |> Array.sort

let main() =
    let n = read int
    let ass = reada int64

    // 任意のi,jについて、A_i * B_i = A_j * B_j を満たす A_i * B_i の値はAの公倍数
    // Bの総和を最小化するにはAの最小公倍数を求める
    // ただし、愚直に最小公倍数を求めるとint64でもオーバーフロー（Aの要素数がMAXで10^4, Aの最大値が10^6なので、）
    // 各A_iの最小公倍数は、各A_iを素因数分解しておき、各素因数について指数が最大のもの同士を掛け合わせたもの
    // なので、これを利用して mod m での最小公倍数を求められる
    let mutable mp = Dictionary<int64, int64>()
    for a in ass do
        let ps = Prime.primeFactors a
        for p in ps do
            if mp.ContainsKey(p.Key) |> not then mp.Add(p.Key, p.Value)
            mp.[p.Key] <- max mp.[p.Key] p.Value

    let lcm =
        seq {
            for p in mp do
                let k = ModInt.init p.Key
                let v = p.Value
                yield k ** v
        }
        |> Seq.fold (*) ModInt.one

    // sprintf "lcm: %A" lcm |> puts

    let bs = ass |> Array.map (ModInt.init >> (fun x -> lcm / x))

    let mutable ans = ModInt.zero
    for b in bs do
        ans <- ans + b
    ans
    |> ModInt.value
    |> puts
    ()

main()
writer.Close()
