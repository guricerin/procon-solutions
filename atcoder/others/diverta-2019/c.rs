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
    input!(n: usize, ss: [chars; n]);

    let mut head_b = 0;
    let mut tail_a = 0;
    let mut tot = 0;
    let mut ba = 0;
    for i in 0..n {
        let s = &ss[i];
        for j in 0..s.len() - 1 {
            if s[j] == 'A' && s[j + 1] == 'B' {
                tot += 1;
            }
        }
        if s[0] == 'B' && s.last().unwrap() == &'A' {
            ba += 1;
        } else if s[0] == 'B' && s.last().unwrap() != &'A' {
            head_b += 1;
        } else if s[0] != 'B' && s.last().unwrap() == &'A' {
            tail_a += 1;
        }
    }

    let mut ans = tot;
    if ba == 0 {
        ans += min(tail_a, head_b);
    } else if ba > 0 {
        if head_b + tail_a == 0 {
            ans += ba - 1;
        } else {
            ans += ba + min(tail_a, head_b);
        }
    }
    println!("{}", ans);
}
