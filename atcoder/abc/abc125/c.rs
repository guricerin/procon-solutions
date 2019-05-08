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
use std::collections::HashMap;

// #[allow(unused_imports)]
// use std::cmp::*;
pub fn gcd<T>(x: T, y: T) -> T
where
    T: PartialEq + Copy + std::ops::Rem<Output = T> + std::convert::From<u8>,
{
    if y == T::from(0u8) {
        x
    } else {
        gcd(y, x % y)
    }
}

fn main() {
    input!(n: usize, vs: [usize; n]);
    let mut ls = vec![0usize; n + 1];
    let mut rs = vec![0usize; n + 1];

    for i in 0..n - 1 {
        ls[i + 1] = gcd(ls[i], vs[i]);
    }
    for i in (0..n).rev() {
        rs[i] = gcd(rs[i + 1], vs[i]);
    }

    let mut ans: usize = 0;
    for i in 0..n {
        let l = ls[i];
        let r = rs[i + 1];
        ans = max(ans, gcd(l, r));
    }
    println!("{}", ans);
}
