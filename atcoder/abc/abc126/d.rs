// Original: https://github.com/tanakh/competitive-rs
#[allow(unused_macros)]
macro_rules! input {
    (source = $s:expr, $($r:tt)*) => {
        let mut iter = $s.split_whitespace();
        let mut next = || { iter.next().unwrap() };
        input_inner!{next, $($r)*}
    };
    ($($r:tt)*) => {
        let stdin = std::io::stdin();
        let mut bytes = std::io::Read::bytes(std::io::BufReader::new(stdin.lock()));
        let mut next = move || -> String{
            bytes
                .by_ref()
                .map(|r|r.unwrap() as char)
                .skip_while(|c|c.is_whitespace())
                .take_while(|c|!c.is_whitespace())
                .collect()
        };
        input_inner!{next, $($r)*}
    };
}

#[allow(unused_macros)]
macro_rules! input_inner {
    ($next:expr) => {};
    ($next:expr, ) => {};

    ($next:expr, $var:ident : $t:tt $($r:tt)*) => {
        let $var = read_value!($next, $t);
        input_inner!{$next $($r)*}
    };

    ($next:expr, mut $var:ident : $t:tt $($r:tt)*) => {
        let mut $var = read_value!($next, $t);
        input_inner!{$next $($r)*}
    };
}

#[allow(unused_macros)]
macro_rules! read_value {
    ($next:expr, ( $($t:tt),* )) => {
        ( $(read_value!($next, $t)),* )
    };

    ($next:expr, [ $t:tt ; $len:expr ]) => {
        (0..$len).map(|_| read_value!($next, $t)).collect::<Vec<_>>()
    };

    ($next:expr, [ $t:tt ]) => {
        {
            let len = read_value!($next, usize);
            (0..len).map(|_| read_value!($next, $t)).collect::<Vec<_>>()
        }
    };

    ($next:expr, chars) => {
        read_value!($next, String).chars().collect::<Vec<char>>()
    };

    ($next:expr, bytes) => {
        read_value!($next, String).into_bytes()
    };

    ($next:expr, usize1) => {
        read_value!($next, usize) - 1
    };

    ($next:expr, $t:ty) => {
        $next().parse::<$t>().expect("Parse error")
    };
}

#[allow(unused_imports)]
use std::cmp::{max, min};
#[allow(unused_imports)]
use std::collections::BTreeMap;

fn main() {
    input!(n: usize, uvw: [(usize1, usize1, usize); n - 1]);

    let mut graph = vec![vec![]; n];
    for &(u, v, w) in uvw.iter() {
        graph[u].push((v, w));
        graph[v].push((u, w));
    }

    // 適当に0番目のノードを根とする
    let mut color = vec![0usize; n];
    color[0] = 0;
    dfs(&graph, &mut color, 0, 0, 0);

    for &c in color.iter() {
        println!("{}", c);
    }
}

fn dfs(
    graph: &Vec<Vec<(usize, usize)>>,
    color: &mut Vec<usize>,
    cur_node: usize,
    par: usize,
    depth: usize,
) {
    // 根から頂点iへの距離をdiと置く
    // 任意の2頂点u,vについて、その最小共通祖先をxとすると、uとvの距離は du + dv - 2dx と書ける
    // 今回はu-v間の距離のパリティ（偶奇）だけ見ればいいので、偶数である -2dx は無視できる
    // よって、根からの距離のパリティが一致するかどうかだけ判定すれば良い
    color[cur_node] = depth % 2;
    for &(next, w) in graph[cur_node].iter() {
        if next == par {
            continue;
        }
        let next_depth = w + depth;
        dfs(graph, color, next, cur_node, next_depth);
    }
}
