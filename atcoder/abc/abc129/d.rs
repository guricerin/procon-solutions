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
    input!(h: usize, w: usize, graph: [chars; h]);

    let mut table = vec![vec![0i64; w]; h];

    for y in 0..h {
        let mut done = vec![false; w];
        for x in 0..w {
            if !check(h, w, y, x, &graph) {
                continue;
            }
            if done[x] {
                continue;
            }
            let mut nx = x;
            let mut len = 0;
            while nx < w {
                if !check(h, w, y, nx, &graph) {
                    break;
                } else {
                    nx += 1;
                    len += 1;
                }
            }
            for k in x..nx {
                table[y][k] += len;
                done[k] = true;
            }
        }
    }
    for x in 0..w {
        let mut done = vec![false; h];
        for y in 0..h {
            if !check(h, w, y, x, &graph) {
                continue;
            }
            if done[y] {
                continue;
            }
            let mut ny = y;
            let mut len = 0;
            while ny < h {
                if !check(h, w, ny, x, &graph) {
                    break;
                } else {
                    ny += 1;
                    len += 1;
                }
            }
            for k in y..ny {
                table[k][x] += len;
                done[k] = true;
            }
        }
    }

    let mut ans = 0;
    for y in 0..h {
        for x in 0..w {
            ans = max(ans, table[y][x] - 1);
        }
    }
    println!("{}", ans);
}

fn check(h: usize, w: usize, y: usize, x: usize, graph: &Vec<Vec<char>>) -> bool {
    if !(0 <= y && y < h && 0 <= x && x < w) {
        return false;
    } else if graph[y][x] == '#' {
        return false;
    } else {
        return true;
    }
}
