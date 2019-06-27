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
use std::collections::BTreeMap;

fn main() {
    input!(n: usize, k: usize, s: chars);

    // 連続する0の区間をk個選び、1の区間と合わせた長さのうちで最大のものが答え

    // 区間の長さを記録
    // 偶数番目が0区間の、奇数番目が1区間の長さという風に統一しておく
    let mut nums = vec![];
    if s[0] == '0' {
        nums.push(0);
    }
    let mut l = 0;
    while l < n {
        let mut r = l;
        while r < n && s[l] == s[r] {
            r += 1;
        }
        nums.push(r - l);
        l = r;
    }
    if s[n - 1] == '0' {
        nums.push(0);
    }

    let mut acc = vec![0usize; nums.len() + 1];
    for i in 0..nums.len() {
        acc[i + 1] = acc[i] + nums[i];
    }

    let mut ans = 0;
    let mut l = 0;
    while l < acc.len() {
        let mut r = l + 2 * k + 1;
        if r >= acc.len() {
            r = acc.len() - 1;
        }
        // 偶数番目から始まる連続する 2k+1 個（以下）の整数の和の最大値
        ans = max(ans, acc[r] - acc[l]);
        l += 2;
    }
    println!("{}", ans);
}
