open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

let isOk txy =
    let t,x,y = txy
    t % 2 = (x+y) % 2

let isOk2 txy cur =
    let t,x,y = txy
    let ct,cx,cy = cur
    let dt,dx,dy = t-ct |> abs, x-cx |> abs, y-cy |> abs
    dt >= dx + dy

[<EntryPoint>]
let main _ =
    let N = read int
    let txy = Array.zeroCreate (N+1)
    txy.[0] <- (0,0,0)
    for i in 1..N do
        let [|t;x;y|] = reada int
        txy.[i] <- (t,x,y)

    let mutable ans = true
    for i in 0..N-1 do
        if isOk txy.[i] |> not || isOk txy.[i+1] |> not || isOk2 txy.[i] txy.[i+1] |> not then
            ans <- false

    if ans then "Yes" else "No"
    |> printfn "%s"
    0
