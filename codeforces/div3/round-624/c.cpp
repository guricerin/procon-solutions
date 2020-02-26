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
    string s;
    cin >> s;
    auto cnts = vector<vector<i32>>(n + 10, vector<i32>(26));
    rep(i, 0, n) {
        rep(j, 0, 26) {
            cnts[i + 1][j] += cnts[i][j];
        }
        i32 c = s[i] - 'a';
        cnts[i + 1][c]++;
    }
    auto ps  = vector<i32>(m);
    auto ans = vector<i32>(26);
    rep(i, 0, m) {
        i32 p;
        cin >> p;
        rep(j, 0, 26) {
            ans[j] += cnts[p][j];
        }
    }
    rep(i, 0, 26) {
        ans[i] += cnts[n][i];
    }
    rep(i, 0, 26) {
        cout << ans[i] << (i != 25 ? " " : "\n");
    }
}

signed main() {
    auto t = 0;
    cin >> t;
    rep(i, 0, t) {
        solve();
    }
}
