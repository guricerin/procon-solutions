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
use std::collections::HashMap;

fn main() {
    input!(n: usize, d: usize, ss: [chars; d]);
    // Reference: https://www.hamayanhamayan.com/entry/2019/05/06/124530
    // 集合をビット列で表現する
    // 「下位iビット目が1である→i番目が集合に含まれる」というルールでビットを立てる
    // ex.
    // {0,1,2} -> 000111
    // {1,3,4} -> 011010
    // {5} -> 100000
    // {} -> 000000
    // 「ビット列bit[d] := d日目に参加できる人の集合」を作る
    let mut bits = vec![0u32; d];
    for i in 0..d {
        for j in 0..n {
            if ss[i][j] == 'o' {
                bits[i] |= 1 << j;
            }
        }
    }

    let mut ans = 0;
    for i in 0..d {
        for j in i + 1..d {
            // i日目とj日目の少なくともいずれかに出席可能な人の集合 => OR演算
            // u64.count_ones(): ビットが立っている個数(u32型)を返す
            let tmp = (bits[i] | bits[j]).count_ones();
            ans = std::cmp::max(ans, tmp);
        }
    }
    println!("{}", ans);
}
