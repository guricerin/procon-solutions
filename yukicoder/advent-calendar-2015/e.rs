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
use std::cmp::{max, min};
#[allow(unused_imports)]
use std::collections::{BTreeMap, BTreeSet, BinaryHeap, VecDeque};

fn main() {
    input! {
        n:i64
    }

    let ps = prime_factorize(n);
    let mut ans = 1 << 50;
    for (k, v) in ps.iter() {
        let k = *k;
        let v = *v;
        match (k, v) {
            (2, 1) => {
                continue;
            }
            (2, _) => {
                chmin(&mut ans, 4);
            }
            _ => {
                chmin(&mut ans, k);
            }
        }
    }

    println!("{}", ans);
}

pub fn prime_factorize(x: i64) -> BTreeMap<i64, usize> {
    let mut map = BTreeMap::new();
    let mut x = x;
    for prime in 2.. {
        if prime * prime > x {
            break;
        }

        while x % prime == 0 {
            *map.entry(prime).or_insert(0) += 1;
            x /= prime;
        }
    }
    if x != 1 {
        *map.entry(x).or_insert(0) += 1;
    }
    map
}
