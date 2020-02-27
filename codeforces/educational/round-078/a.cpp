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
    string A, B;
    cin >> A >> B;
    i32 alen = (i32)A.size();
    i32 blen = (i32)B.size();
    bool ok  = false;
    rep(l, 0, blen) {
        auto b = B.substr(l, alen);
        sort(all(b));
        auto a = A;
        sort(all(a));
        if (a == b) ok = true;
    }
    cout << (ok ? "YES" : "NO") << "\n";
}

signed main() {
    i32 t;
    cin >> t;
    while (t--)
        solve();
}
