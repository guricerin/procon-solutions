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

fn main() {
    input!(n: usize, a: usize, xs: [usize; n]);

    let nax = n * 50 + 10;
    // dp[j][k][s]: x_1 .. x_j からk枚選んだときの合計がsになるような選び方の総数
    let mut dp = vec![vec![vec![0usize; nax]; n + 10]; n + 10];

    for j in 0..n + 1 {
        for k in 0..n + 1 {
            for s in 0..nax {
                if j == 0 && k == 0 && s == 0 {
                    dp[j][k][s] = 1;
                } else if j >= 1 && s < xs[j - 1] {
                    dp[j][k][s] = dp[j - 1][k][s];
                } else if j >= 1 && k >= 1 && s >= xs[j - 1] {
                    dp[j][k][s] = dp[j - 1][k][s] + dp[j - 1][k - 1][s - xs[j - 1] as usize];
                } else {
                    dp[j][k][s] = 0;
                }
            }
        }
    }

    let mut ans = 0usize;
    for k in 1..n + 1 {
        // k枚えらんだときの合計の平均がa => 合計がk * a
        ans += dp[n][k][k * a as usize];
    }
    println!("{}", ans);
}
