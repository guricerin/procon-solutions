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

// vs[p..r]において、vs[r]以下の要素をvs[p..q-1]に、
// vs[r]より大きい要素をvs[q..r]に区分けする。
fn partition(vs: &mut [usize], p: usize, r: usize) -> usize {
    let x = vs[r];
    let mut i: i64 = p as i64 - 1;
    for j in p..r {
        if vs[j] <= x {
            i += 1;
            vs.swap(i as usize, j);
        }
    }
    let q = (i + 1) as usize;
    vs.swap(q, r);
    return q;
}

fn main() {
    input!(n: usize, mut vs: [usize; n]);

    let r = n - 1;
    let q = partition(&mut vs, 0, r);

    for i in 0..n {
        if i != n - 1 {
            if i == q {
                print!("[{}] ", &vs[i]);
            } else {
                print!("{} ", &vs[i]);
            }
        } else {
            if i == q {
                println!("[{}]", &vs[i]);
            } else {
                println!("{}", &vs[i]);
            }
        }
    }
}
