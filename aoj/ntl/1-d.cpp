#include <bits/stdc++.h>
using namespace std;

#define rep(i, a, b) for (int i = (a); i < (int)(b); i++)
#define rrep(i, a, b) for (int i = (a); i >= (int)(b); i--)
#define all(x) (x).begin(), (x).end()
using i32 = int32_t;
using i64 = int64_t;
using f32 = float;
using f64 = double;
using P   = pair<int, int>;

template <class T>
bool chmin(T& a, T b) {
    if (a > b) {
        a = b;
        return true;
    } else {
        return false;
    }
}
template <class T>
bool chmax(T& a, T b) {
    if (a < b) {
        a = b;
        return true;
    } else {
        return false;
    }
}

template <class T>
void dump_vec(const vector<T>& v) {
    auto len = v.size();
    rep(i, 0, len) {
        cout << v[i] << (i == (int)len - 1 ? "\n" : " ");
    }
}

struct FastIO {
    FastIO() {
        cin.tie(0);
        ios::sync_with_stdio(false);
        cout << fixed << setprecision(20);
    }
} FASTIO;

//---------------------------------------------------------------------------------------------------

map<i64, i64> prime_factors(i64 n) {
    map<i64, i64> res; // factor, num
    for (i64 p = 2; p * p <= n; p++) {
        while (n % p == 0) {
            res[p]++;
            n /= p;
        }
    }
    if (n != 1) res[n] = 1;
    return res;
}

i64 eulers_phy_function(i64 n) {
    auto mp = prime_factors(n);
    f64 res = n;
    for (auto p : mp) {
        res *= 1.0 - (1.0 / p.first);
    }
    return (i64)res;
}
//---------------------------------------------------------------------------------------------------

// オイラーのファイ関数：https://mathtrain.jp/phi
signed main() {
    i64 N;
    cin >> N;
    auto ans = eulers_phy_function(N);
    cout << ans << "\n";
}
