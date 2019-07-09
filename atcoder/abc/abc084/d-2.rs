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
    input!(q: usize, lr: [(usize, usize); q]);

    // 累積和
    let lim = 1e5 as usize + 1;
    let sieve = eratosthenes_sieve(lim);
    let mut vs = vec![0usize; lim];
    for i in 0..lim {
        if sieve[i] && sieve[(i + 1) / 2] {
            vs[i] = 1;
        }
    }
    let mut acc = vec![0usize; lim + 1];
    for i in 0..lim {
        acc[i + 1] = acc[i] + vs[i];
    }

    for &(l, r) in lr.iter() {
        // 累積和では半開区間で考えるクセを身に付けること
        let ans = acc[r + 1] - acc[l];
        println!("{}", ans);
    }
}

/// vec[idx]がtrue -> idxが素数である配列を返す
/// O(n log log n)
fn eratosthenes_sieve(n: usize) -> Vec<bool> {
    let n = n + 1;
    let mut res = vec![true; n];
    res[0] = false;
    res[1] = false;
    for i in 2..n {
        if !res[i] {
            continue;
        }
        for j in 2..n {
            if i * j >= n {
                break;
            }
            // 素数の倍数は素数ではない
            res[i * j] = false;
        }
    }
    res
}
