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
    int N;
    cin >> N;
    string s;
    cin >> s;
    bool ok = false;
    // 好きなだけ文字は消去できる
    // 最初の8の出現位置から末尾まで11文字以上あればok
    rep(i, 0, N) {
        if (s[i] == '8' && N - i >= 11) ok = true;
    }

    cout << (ok ? "YES" : "NO") << "\n";
}

signed main() {
    int t;
    cin >> t;
    while (t--)
        solve();
}
