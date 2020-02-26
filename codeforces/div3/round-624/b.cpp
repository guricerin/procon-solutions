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
    i32 n, m;
    cin >> n >> m;
    auto as = vector<i32>(n);
    rep(i, 0, n) {
        cin >> as[i];
    }
    auto ps = set<i32>();
    rep(i, 0, m) {
        i32 p;
        cin >> p;
        ps.insert(p);
    }

    i32 l = 0;
    rep(r, 1, n + 1) {
        if (!ps.count(r)) {
            sort(as.begin() + l, as.begin() + r);
            l = r;
        }
    }

    if (is_sorted(all(as))) {
        cout << "YES" << endl;
    } else {
        cout << "NO" << endl;
    }
}

signed main() {
    i32 t;
    cin >> t;
    rep(i, 0, t) {
        solve();
    }
}
