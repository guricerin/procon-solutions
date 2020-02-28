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
    int n, x;
    cin >> n >> x;
    // 小さい奇数を消す。これをn回繰り返す
    // x < n より、x番目の数は単に偶数のうちx番目に小さい数と考えてよい
    auto ans = x * 2;
    cout << ans << "\n";
}

signed main() {
    int t;
    cin >> t;
    while (t--)
        solve();
}
