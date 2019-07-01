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

pub trait ChgMinMax {
    fn chmin(&mut self, other: Self) -> bool;
    fn chmax(&mut self, other: Self) -> bool;
}

impl<T> ChgMinMax for T
where
    T: PartialOrd,
{
    fn chmin(&mut self, other: Self) -> bool {
        *self > other && {
            *self = other;
            true
        }
    }

    fn chmax(&mut self, other: Self) -> bool {
        *self < other && {
            *self = other;
            true
        }
    }
}

use std::cmp::Ordering;

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

#[allow(unused_imports)]
use std::cmp::{max, min};
#[allow(unused_imports)]
use std::collections::{BTreeMap, BTreeSet, BinaryHeap, VecDeque};

fn main() {
    input!(n: usize, m: usize, ftc: [(usize, usize, usize); m]);

    let mut edges = vec![vec![]; n];
    for &(f, t, c) in ftc.iter() {
        edges[f].push((t, c));
        edges[t].push((f, c));
    }

    const INF: usize = 1e9 as usize;
    let mut dist4 = vec![vec![INF; 4]; n];
    let mut dist7 = vec![vec![INF; 7]; n];

    let mut heap = BinaryHeap::new();
    heap.push(Rev((0, 0)));
    while let Some(Rev((cost, from))) = heap.pop() {
        let step4 = cost % 4;
        let step7 = cost % 7;
        if dist4[from][step4] <= cost && dist7[from][step7] <= cost {
            continue;
        }

        if dist4[from][step4] > cost {
            dist4[from][step4] = cost;
        }
        if dist7[from][step7] > cost {
            dist7[from][step7] = cost;
        }

        if from == n - 1 {
            continue;
        }

        for &(to, c) in edges[from].iter() {
            let next_cost = cost + c;
            if dist4[to][next_cost % 4] > next_cost || dist7[to][next_cost % 7] > next_cost {
                heap.push(Rev((next_cost, to)));
            }
        }
    }

    println!("{}", min(dist4[n - 1][0], dist7[n - 1][0]));
}
