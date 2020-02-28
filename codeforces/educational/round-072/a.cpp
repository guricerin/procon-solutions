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
    int S, I, E;
    cin >> S >> I >> E;
    // Sにぶっぱしなければならない最低値を二分探索
    int ok = E + 1, ng = -1;
    while (abs(ok - ng) > 1) {
        auto mid = (ok + ng) / 2;
        if (S + mid > I + (E - mid)) {
            ok = mid;
        } else {
            ng = mid;
        }
    }
    auto ans = E - ok + 1;
    cout << max(0, ans) << "\n";
}

signed main() {
    int t;
    cin >> t;
    while (t--)
        solve();
}
