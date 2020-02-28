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
    int B, P, F;
    cin >> B >> P >> F;
    int H, C;
    cin >> H >> C;
    B /= 2;
    int ans = 0;
    // 全探索でok
    rep(t, 0, B + 1) {
        auto l = min(t, P) * H;
        auto r = min(B - t, F) * C;
        chmax(ans, l + r);
    }
    cout << ans << "\n";
}

signed main() {
    int t;
    cin >> t;
    while (t--)
        solve();
}
