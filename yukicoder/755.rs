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

// Ref http://perogram.hateblo.jp/entry/2019/06/21/052134
// 二次元累積和は、1-indexで考えたほうがバグりにくい
fn main() {
    input!(
        n: usize,
        m: usize,
        a: [[i64; m]; m],
        xy: [(usize, usize); n]
    );

    let mut acc = vec![vec![0i64; m + 10]; m + 10];
    for y in 0..m {
        for x in 0..m {
            acc[y + 1][x + 1] = a[y][x];
        }
    }

    // 縦
    for y in 0..m + 1 {
        for x in 0..m + 1 {
            acc[y][x + 1] += acc[y][x];
        }
    }
    // 横
    for x in 0..m + 1 {
        for y in 0..m + 1 {
            acc[y + 1][x] += acc[y][x];
        }
    }

    for &(x, y) in xy.iter() {
        let mut count = 0;
        for y0 in 1..m + 1 {
            for y1 in y0..m + 1 {
                for x0 in 1..m + 1 {
                    for x1 in x0..m + 1 {
                        if !(x0 <= x && x <= x1 && y0 <= y && y <= y1) {
                            continue;
                        }

                        let sum =
                            acc[x1][y1] - acc[x0 - 1][y1] - acc[x1][y0 - 1] + acc[x0 - 1][y0 - 1];
                        if sum == 0 {
                            count += 1;
                        }
                    }
                }
            }
        }
        println!("{}", count);
    }
}
