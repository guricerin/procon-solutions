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
    string S;
    cin >> S;
    i32 len = (i32)S.size();
    i32 ans = 0;
    i32 l   = 0;
    // 1と1にはさまれている0を消せ。ぷよぷよのように。
    while (l < len) {
        i32 r = l + 1;
        while (r < len && S[r] == '0') {
            r++;
        }
        auto cnt = r - l - 1;
        if (S[l] == '1' && r < len && S[r] == '1') {
            ans += cnt;
        }
        l = r;
    }
    cout << ans << "\n";
}

signed main() {
    i32 t;
    cin >> t;
    while (t--)
        solve();
}
