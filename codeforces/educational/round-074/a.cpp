#include <bits/stdc++.h>
using namespace std;

#define rep(i, a, b) for (int i = (a); i < (b); ++i)
#define all(x) (x).begin(), (x).end()
using i32 = int32_t;
using i64 = int64_t;
using f32 = float;
using f64 = double;
using P   = pair<int, int>;

template <class T>
bool chmin(T &a, T b) {
    if (a > b) {
        a = b;
        return true;
    } else {
        return false;
    }
}
template <class T>
bool chmax(T &a, T b) {
    if (a < b) {
        a = b;
        return true;
    } else {
        return false;
    }
}

struct Setup {
    Setup() {
        cin.tie(0);
        ios::sync_with_stdio(false);
        cout << fixed << setprecision(20);
    }
} SETUP;

void solve() {
    i64 X, Y;
    cin >> X >> Y;
    auto d = abs(X - Y);
    // 1より大きい整数は素数の約数をもつ -> 差が1の場合だけNO
    if (d == 0 || d == 1)
        cout << "NO\n";
    else
        cout << "YES\n";
}

signed main() {
    int t;
    cin >> t;
    while (t--)
        solve();
}
