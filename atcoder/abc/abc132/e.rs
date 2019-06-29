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
use std::collections::{BTreeMap, BTreeSet, BinaryHeap, VecDeque};

const INF: usize = 1e9 as usize;

fn main() {
    input!(
        n: usize,
        m: usize,
        uv: [(usize1, usize1); m],
        s_pos: usize1,
        g_pos: usize1
    );

    let mut edges = vec![vec![]; n];
    for i in 0..m {
        edges[uv[i].0].push(uv[i].1);
    }

    // ゴールへの距離が3の倍数出ないとゴールできない幅優先探索
    // -> 頂点数を3倍する
    let mut dist = vec![vec![INF; n]; 3];
    dist[0][s_pos] = 0;
    let mut que = VecDeque::new();
    que.push_back((s_pos, 0));
    while let Some((from, step)) = que.pop_front() {
        if from == g_pos && step == 0 {
            println!("{}", dist[step][g_pos] / 3);
            return;
        }

        let next_step = (step + 1) % 3;
        for &to in edges[from].iter() {
            let next_d = dist[step][from] + 1;
            if dist[next_step][to] > next_d {
                dist[next_step][to] = next_d;
                que.push_back((to, next_step));
            }
        }
    }

    println!("{}", -1);
}
