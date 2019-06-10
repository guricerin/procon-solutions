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
use std::collections::BTreeMap;

fn main() {
    input!(n: usize, k: usize);

    let n_primes = prime_factorize(n as i64);
    let mut ans_n = 0;
    let mut ans_c = 0; // 約数の個数の最大
    for m in 1..n {
        let m_primes = prime_factorize(m as i64);
        // 共通の素因数の個数を保持
        let mut same = 0;
        for (p, q) in m_primes.iter() {
            if let Some(n_p_num) = n_primes.get(p) {
                same += min(q, n_p_num);
            }
        }
        if same < k {
            continue;
        }
        // 約数の個数の最大を更新
        let m_divs = divisor(m as i64);
        if ans_c < m_divs.len() {
            ans_c = m_divs.len();
            ans_n = m;
        }
    }

    println!("{}", ans_n);
}

/// 素因数分解
pub fn prime_factorize(x: i64) -> BTreeMap<i64, usize> {
    let mut map = BTreeMap::new();
    let mut x = x;
    for prime in 2.. {
        if prime * prime > x {
            break;
        }

        while x % prime == 0 {
            *map.entry(prime).or_insert(0) += 1;
            x /= prime;
        }
    }
    if x != 1 {
        *map.entry(x).or_insert(0) += 1;
    }
    map
}

pub fn is_prime(n: i64) -> bool {
    if n <= 1 {
        return false;
    }
    for prime in 2.. {
        if prime * prime > n {
            break;
        }
        if n % prime == 0 {
            return false;
        }
    }
    return true;
}

/// n の約数の個数
pub fn divisor_num(n: i64) -> usize {
    let primes = prime_factorize(n);
    let mut res = 1;
    for (_p, q) in primes.iter() {
        res *= q + 1;
    }
    res
}

/// n の約数の列挙
pub fn divisor(n: i64) -> Vec<usize> {
    let mut res = vec![];
    let n = n as usize;
    for i in 1.. {
        if i * i > n {
            break;
        }
        if n % i == 0 {
            res.push(i);
            if i * i != n {
                res.push(n / i);
            }
        }
    }
    res.sort();
    res
}
