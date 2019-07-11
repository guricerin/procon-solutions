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

#[allow(dead_code)]
fn chmin<T>(x: &mut T, y: T) -> bool
where
    T: PartialOrd + Copy,
{
    *x > y && {
        *x = y;
        true
    }
}

#[allow(dead_code)]
fn chmax<T>(x: &mut T, y: T) -> bool
where
    T: PartialOrd + Copy,
{
    *x < y && {
        *x = y;
        true
    }
}

#[allow(unused_imports)]
use std::cmp::Ordering;
#[allow(unused_imports)]
use std::cmp::{max, min};
#[allow(unused_imports)]
use std::collections::{BTreeMap, BTreeSet, BinaryHeap, VecDeque};

fn main() {
    input!(
        n: usize,
        abc: [(usize1, usize1, usize); n - 1],
        q: usize,
        k: usize1,
        xy: [(usize1, usize1); q]
    );

    // kを経由する場合のxからyへの最短距離
    // => kからの全点最短距離を求めておき、dist[x] + dist[y]を答える
    let mut edges = vec![vec![]; n];
    for &(a, b, c) in abc.iter() {
        edges[a].push((b, c));
        edges[b].push((a, c));
    }
    let dist = dijkstra(&edges, k);

    for &(x, y) in xy.iter() {
        let ans = dist[x] + dist[y];
        println!("{}", ans);
    }
}

///  負辺のない単一始点全点間最短路を求める
///  O(ElogV)
fn dijkstra(weighted_edges: &Vec<Vec<(usize, usize)>>, start_pos: usize) -> Vec<usize> {
    const INF: usize = 1e18 as usize;
    let n = weighted_edges.len();
    let mut dist = vec![INF; n];
    dist[start_pos] = 0;
    let mut heap = BinaryHeap::new();
    heap.push(Rev((dist[start_pos], start_pos)));
    while let Some(Rev((cost, from))) = heap.pop() {
        if dist[from] < cost {
            continue;
        }

        for &(to, c) in weighted_edges[from].iter() {
            let next_cost = cost + c;
            if dist[to] <= next_cost {
                continue;
            }
            dist[to] = next_cost;
            heap.push(Rev((next_cost, to)));
        }
    }
    dist
}

#[derive(Eq, PartialEq, Clone, Debug)]
struct Rev<T>(pub T);

impl<T: PartialOrd> PartialOrd for Rev<T> {
    fn partial_cmp(&self, other: &Self) -> Option<Ordering> {
        other.0.partial_cmp(&self.0)
    }
}

impl<T: Ord> Ord for Rev<T> {
    fn cmp(&self, other: &Self) -> Ordering {
        other.0.cmp(&self.0)
    }
}
