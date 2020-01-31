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
        s:chars
    }

    let mut ss = vec![];
    let mut l = 0;
    let len = s.len();
    while l < len {
        let mut r = l + 1;
        while r < len && s[r].is_lowercase() {
            r += 1;
        }
        r += 1;
        let mut buf = String::new();
        for i in l..min(r, len) {
            buf = format!("{}{}", buf, s[i]);
        }
        ss.push(buf);
        l = r;
    }

    // 普通のsortでは、文字列の長さも考慮されてしまい、辞書順にならない
    ss.sort_by(|a, b| {
        let a = a.chars().map(|x| to_upper(x)).collect::<String>();
        let b = b
            .chars()
            // .map(|x| x.to_ascii_uppercase())
            .map(|x| to_upper(x))
            .collect::<String>();
        a.cmp(&b)
    });
    let ans = ss.iter().cloned().collect::<String>();
    println!("{}", ans);
}

fn to_upper(c: char) -> char {
    if 'A' <= c && c <= 'Z' {
        c
    } else if 'a' <= c && c <= 'z' {
        let c = b'A' + (c as u8 - b'a');
        c as char
    } else {
        unimplemented!()
    }
}
