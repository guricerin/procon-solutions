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
    input!(n: usize, vs: [usize; n]);

    let nax = vs.iter().max().unwrap();
    let nax = *nax;
    let mut cs = vec![0usize; nax + 1];
    // cs[i]にvs[i]の出現数を記録
    for i in 0..n {
        cs[vs[i]] += 1;
    }

    // cs[i]にi以下の数の出現数を記録
    for i in 1..nax + 1 {
        cs[i] += cs[i - 1];
    }

    // 出力用配列bsにおけるvs[i]の位置を求める
    // ex.1が3個、2が5個、3が4個出現している場合、
    // 1は結果列の0番から、2は3番から、3は8番から始まることがわかる。
    let mut bs = vec![0usize; n];
    for i in (0..n).rev() {
        bs[cs[vs[i]] - 1] = vs[i];
        // 同じ数の要素が複数ある場合を考慮
        cs[vs[i]] -= 1;
    }

    for i in 0..n {
        if i != n - 1 {
            print!("{} ", bs[i])
        } else {
            println!("{}", bs[i]);
        }
    }
}
