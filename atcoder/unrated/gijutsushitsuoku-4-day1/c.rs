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
        n:i64,x:chars
    }

    for m in 2..11 {
        let s = convert(n, m);
        if f(&s,&x) {
            println!("{}", m);
        }
    }
}

fn f(a:&[char], b:&[char]) -> bool {
    let al = a.len();
    let bl = b.len();
    if al != bl {
        return false;
    }
    let mut ok = true;
    for i in 0..min(al, bl) {
        if a[i] != b[i] {
            ok = false;
        }
    }
    ok
}

fn convert(n: i64, base: i64) -> Vec<char> {
    let mut res = vec![];
    let mut n = n;

    loop {
        let rem = (n % base) as u8;
        let c: char = std::char::from_digit(rem as u32, 10).unwrap();
        res.push(c);
        n /= base;
        if n <= 0 {
            break;
        }
    }
    // res.iter().rev().collect::<String>()
    res.iter().rev().map(|c| *c).collect::<Vec<char>>()
}
