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

mod util {
    #[allow(dead_code)]
    pub fn chmin<T>(x: &mut T, y: T) -> bool
    where
        T: PartialOrd + Copy,
    {
        *x > y && {
            *x = y;
            true
        }
    }

    #[allow(dead_code)]
    pub fn chmax<T>(x: &mut T, y: T) -> bool
    where
        T: PartialOrd + Copy,
    {
        *x < y && {
            *x = y;
            true
        }
    }

    /// 整数除算切り上げ
    #[allow(dead_code)]
    pub fn roundup(a: i64, b: i64) -> i64 {
        (a + b - 1) / b
    }

    #[allow(dead_code)]
    pub fn ctoi(c: char) -> i64 {
        c as i64 - 48
    }
}

#[allow(unused_imports)]
use util::*;

#[allow(unused_imports)]
use std::cmp::{max, min};
#[allow(unused_imports)]
use std::collections::{BTreeMap, BTreeSet, BinaryHeap, VecDeque};

fn main() {
    input! {
        n:usize,m:usize,k:usize,e:i64,
        mut tasks:[i64;n],
        mut bs:[i64;m]
    }

    // ソートしておく
    tasks.sort();
    bs.sort();
    bs.reverse();

    let task_total = tasks.iter().map(|a| *a).sum::<i64>();
    let e_total = bs.iter().take(k).map(|a| *a).sum::<i64>() + e;
    if task_total > e_total {
        // エナドリをk本飲んでもすべてのタスクを完了できない場合
        // タスクの累積和をとって何件までなら完了できるかを貪欲に調べる
        let mut acc = vec![0i64; n + 1];
        for i in 0..n {
            acc[i + 1] = acc[i] + tasks[i];
        }
        let mut done = 0;
        for i in 1..n + 1 {
            done = i;
            if acc[i] > e_total {
                done -= 1;
                break;
            }
        }
        println!("No");
        println!("{}", done);
    } else {
        // エナドリをk本飲んだらすべてのタスクを完了できる場合
        // 最小で何本飲んだらすべてのタスクを完了できるかを貪欲に調べる
        let mut drink = 0;
        let mut cur_e = e;
        for i in 0..m {
            if cur_e >= task_total {
                break;
            }
            cur_e += bs[i];
            drink += 1;
        }
        println!("Yes");
        println!("{}", drink);
    }
}
