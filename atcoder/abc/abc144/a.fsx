let [| a; b |] = stdin.ReadLine().Split(' ') |> Array.map int

if a > 9 || b > 9 then printfn "-1"
else printfn "%d" (a * b)
