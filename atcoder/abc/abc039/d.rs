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

const DXY: [(i64, i64); 8] = [
    (-1, -1),
    (-1, 0),
    (-1, 1),
    (0, -1),
    (0, 1),
    (1, -1),
    (1, 0),
    (1, 1),
];

fn main() {
    input!(h: usize, w: usize, graph: [chars; h]);

    let mut ans_graph = vec![vec!['#'; w]; h];
    let mut que = VecDeque::new();
    for y in 0..h {
        for x in 0..w {
            if graph[y][x] == '.' {
                que.push_back((y, x));
                ans_graph[y][x] = '.';
            }
        }
    }

    // 白マスを復元
    while let Some((y, x)) = que.pop_front() {
        for &(dy, dx) in DXY.iter() {
            let ny = y as i64 + dy;
            let nx = x as i64 + dx;
            if !(0 <= ny && ny < h as i64 && 0 <= nx && nx < w as i64) {
                continue;
            }
            let ny = ny as usize;
            let nx = nx as usize;
            ans_graph[ny][nx] = '.';
        }
    }

    let mut b_que = VecDeque::new();
    for y in 0..h {
        for x in 0..w {
            if ans_graph[y][x] == '#' {
                b_que.push_back((y, x));
            }
        }
    }

    // 再度、画像処理
    let mut ans2graph = ans_graph.clone();
    while let Some((y, x)) = b_que.pop_front() {
        for &(dy, dx) in DXY.iter() {
            let ny = y as i64 + dy;
            let nx = x as i64 + dx;
            if !(0 <= ny && ny < h as i64 && 0 <= nx && nx < w as i64) {
                continue;
            }
            let ny = ny as usize;
            let nx = nx as usize;
            ans2graph[ny][nx] = '#';
        }
    }

    let ok = graph == ans2graph;
    if ok {
        println!("possible");
        for a in ans_graph.into_iter() {
            for c in a.iter() {
                print!("{}", c);
            }
            println!("");
        }
    } else {
        println!("impossible");
    }
}
