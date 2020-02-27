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
        // cin.tie(0);
        // ios::sync_with_stdio(false);
        // cout << fixed << setprecision(20);
    }
} SETUP;

void solve() {
    i64 s;
    cin >> s;
    i64 ans = 0L;
    while (s > 0L) {
        i64 a = s / 10L;
        i64 c = a * 10l;
        i64 b = s % 10L;
        if (c <= 0L) {
            ans += c + b;
            break;
        }
        ans += c;
        s = a + b;
    }
    cout << ans << "\n";
}

signed main() {
    i32 t;
    cin >> t;
    rep(i, 0, t) solve();
}
