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
use std::collections::VecDeque;

const DXY: [(i64, i64); 4] = [(-1, 0), (0, -1), (0, 1), (1, 0)];
const INF: i64 = 1 << 30;

fn main() {
    input!(h: usize, w: usize, graph: [chars; h]);

    // 幅優先探索
    // 問題を読み替えると、'#'マスから最も遠い'.'マスへの最短路を求める問題に落とし込める
    let mut dist = vec![vec![INF; w]; h];
    let mut que = VecDeque::new();
    for y in 0..h {
        for x in 0..w {
            if graph[y][x] == '#' {
                que.push_back((y, x));
                dist[y][x] = 0;
            }
        }
    }

    while let Some((y, x)) = que.pop_front() {
        for &(dy, dx) in DXY.iter() {
            let ny = y as i64 + dy;
            let nx = x as i64 + dx;

            if !(0 <= ny && ny < h as i64 && 0 <= nx && nx < w as i64) {
                continue;
            }

            let ny = ny as usize;
            let nx = nx as usize;
            if dist[ny][nx] > dist[y][x] + 1 {
                dist[ny][nx] = dist[y][x] + 1;
                que.push_back((ny, nx));
            }
        }
    }

    let ans = dist.iter().map(|x| x.iter().max().unwrap()).max().unwrap();
    println!("{}", ans);
}
