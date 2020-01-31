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

let dxy = [|(0,-1);(-1,0);(1,0);(0,1)|]

let isInner (x,y) w h =
    0 <= x && x < w && 0 <= y && y < h

let isNoVisited (graph: (int * bool) [,]) nx ny =
    let (_, visited) = graph.[ny, nx]
    not visited

let check (graph: (int * bool) [,]) (x,y) (nx,ny) =
    let (h,_) = graph.[y,x]
    let (nh,_) = graph.[ny,nx]
    h > nh

let rec dfs (graph: (int * bool) [,]) (sx,sy) w h =
    let (t,v) = graph.[sy,sx]
    if v then 0
    else
        let mutable ans = 1
        graph.[sy,sx] <- (t,true)
        for (dx,dy) in dxy do
            let nx,ny = dx+sx, dy+sy
            if isInner (nx,ny) w h && isNoVisited graph nx ny then
                if check graph (sx,sy) (nx,ny) then
                    ans <- ans + (dfs graph (nx,ny) w h)
        ans


let solve() =
    let mutable buf = ""
    while true do
        let [|W;H;P|] = reada int
        if W = 0 && H = 0 && P = 0 then
            printf "%s" buf
            exit(0)
        else
            let graph = Array2D.zeroCreate H W
            for i in 0..H-1 do
                let zs = reada int
                for j in 0..W-1 do
                    graph.[i, j] <- (zs.[j], false)
            let mutable ans = 0
            for i in 0..P-1 do
                let [|x;y|] = reada int
                ans <- ans + (dfs graph (x,y) W H)
            buf <- sprintf "%s%d\n" buf ans
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> failwithf "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
