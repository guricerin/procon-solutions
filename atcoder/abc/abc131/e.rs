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
    input!(n: usize, k: usize);

    // N頂点の連結グラフには、少なくともN-1の辺がある
    // 全頂点の組み:N(N-1)/2
    // 辺で繋がれた頂点の組み:N-1
    // 最短距離が2の頂点の組み:N(N-1)/2 - (N-1) = (N-1)(N-2)/2 以下になる
    // -> K > N(N-1)/2 の時、構築できない
    // else 最短距離が2の頂点の組みがK個になるまで、スターグラフの任意の頂点を繋げればいい

    // 下限 完全グラフ(全ての頂点同士が辺で繋がっている)の時:0
    // 上限 スターグラフ(N-1個の頂点が真ん中の頂点とだけ辺で繋がっている)の時:(N-1)(N-2)/2

    let lim_node = (n - 1) * (n - 2) / 2;
    if k > lim_node {
        println!("-1");
    } else {
        // スターグラフ構築
        let mut graph = vec![];
        for i in 0..n - 1 {
            graph.push((i + 1, n));
        }

        // 新たに繋げる辺の候補を列挙
        let mut edge = vec![];
        for v in 0..n - 1 {
            for u in 0..v {
                edge.push((v + 1, u + 1));
            }
        }

        // 繋げる
        let add_num = lim_node - k;
        for i in 0..add_num {
            graph.push(edge[i]);
        }

        println!("{}", graph.len());
        for &(v, u) in graph.iter() {
            println!("{} {}", v, u);
        }
    }
}
