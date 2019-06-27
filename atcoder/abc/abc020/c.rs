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

fn main() {
    input!(h: usize, w: usize, t: i64, graph: [chars; h]);

    let mut start = (0, 0);
    let mut goal = (0, 0);
    for y in 0..h {
        for x in 0..w {
            if graph[y][x] == 'S' {
                start = (y, x);
            }
            if graph[y][x] == 'G' {
                goal = (y, x);
            }
        }
    }

    // low以上hi未満の範囲を二分探索で絞り込む
    let mut low = 1;
    let mut hi = t + 1;
    while (hi - low).abs() > 1 {
        let mid = low + (hi - low) / 2;
        if bfs(mid, t, &graph, start, goal) {
            low = mid;
        } else {
            hi = mid;
        }
    }
    let ans = low;
    println!("{}", ans);
}

const D4: [(i64, i64); 4] = [(-1, 0), (0, -1), (0, 1), (1, 0)];
const INF: i64 = 1 << 40;
fn bfs(key: i64, t: i64, graph: &Vec<Vec<char>>, s: (usize, usize), g: (usize, usize)) -> bool {
    let h = graph.len();
    let w = graph[0].len();
    let mut dist = vec![vec![INF; w]; h];
    dist[s.0][s.1] = 0;

    let mut que = VecDeque::new();
    que.push_back(s);
    while let Some((y, x)) = que.pop_front() {
        // ゴールだからといってループを抜けてはいけない
        // 最短路は更新される可能性があるから。これのせいでハマってた
        // if (y, x) == g {
        //     break;
        // }
        for &(dy, dx) in D4.iter() {
            let ny = y as i64 + dy;
            let nx = x as i64 + dx;
            if !(0 <= ny && ny < h as i64 && 0 <= nx && nx < w as i64) {
                continue;
            }
            let ny = ny as usize;
            let nx = nx as usize;

            let d = dist[y][x] + if graph[ny][nx] == '#' { key } else { 1 };

            if dist[ny][nx] > d {
                dist[ny][nx] = d;
                que.push_back((ny, nx));
            }
        }
    }
    // t以内でゴールにたどり着けるか
    dist[g.0][g.1] <= t
}
