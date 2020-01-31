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

mod util {
    #[allow(dead_code)]
    pub fn chmin<T>(x: &mut T, y: T) -> bool
    where
        T: PartialOrd + Copy,
    {
        *x > y && {
            *x = y;
            true
        }
    }

    #[allow(dead_code)]
    pub fn chmax<T>(x: &mut T, y: T) -> bool
    where
        T: PartialOrd + Copy,
    {
        *x < y && {
            *x = y;
            true
        }
    }

    /// 整数除算切り上げ
    #[allow(dead_code)]
    pub fn roundup(a: i64, b: i64) -> i64 {
        (a + b - 1) / b
    }

    #[allow(dead_code)]
    pub fn ctoi(c: char) -> i64 {
        c as i64 - 48
    }
}

#[allow(unused_imports)]
use util::*;

#[allow(unused_imports)]
use std::cmp::{max, min};
#[allow(unused_imports)]
use std::collections::{BTreeMap, BTreeSet, BinaryHeap, VecDeque};

fn main() {
    let mut buf = String::new();
    std::io::stdin().read_line(&mut buf).unwrap();
    let mut itr = buf.split_whitespace();
    let n: usize = itr.next().unwrap().parse().unwrap();
    let q: usize = itr.next().unwrap().parse().unwrap();

    let mut ss = vec![vec![]; q];
    for i in 0..q {
        let mut buf = String::new();
        std::io::stdin().read_line(&mut buf).unwrap();
        let mut itr = buf.split_whitespace();
        let s: usize = itr.next().unwrap().parse().unwrap();
        ss[i].push(s - 1);
        let cnt = if s == 1 { 2 } else { 1 };
        for _ in 0..cnt {
            let s: usize = itr.next().unwrap().parse().unwrap();
            ss[i].push(s - 1);
        }
    }

    // graph[a][b] = true : a が b をフォローしている
    let mut graph = vec![vec![false; n]; n];
    for s in ss.iter() {
        match s[0] {
            0 => {
                let a = s[1];
                let b = s[2];
                graph[a][b] = true;
            }
            1 => {
                let a = s[1];
                for b in 0..n {
                    if graph[b][a] {
                        graph[a][b] = true;
                    }
                }
            }
            2 => {
                let a = s[1];
                let mut x_follows = BTreeSet::new();
                for x in 0..n {
                    if !graph[a][x] {
                        continue;
                    }
                    for y in 0..n {
                        if !graph[x][y] {
                            continue;
                        }
                        x_follows.insert(y);
                    }
                }
                for y in x_follows.iter() {
                    graph[a][*y] = true;
                }
                graph[a][a] = false;
            }
            _ => unreachable!(),
        }
    }

    for v in graph.iter() {
        let ans = v
            .iter()
            .map(|b| if *b { "Y" } else { "N" })
            .collect::<String>();
        println!("{}", ans);
    }
}
