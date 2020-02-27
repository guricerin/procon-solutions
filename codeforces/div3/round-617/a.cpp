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
    i32 N;
    cin >> N;
    auto A  = vector<i32>(N);
    i32 odd = 0;
    rep(i, 0, N) {
        i32 a;
        cin >> a;
        if (a & 1) odd++;
        A[i] = a;
    }
    i32 even = N - odd;
    // odd‚ðŠï”ŒÂ‚É‚Å‚«‚é‚È‚çok
    if (odd > 0 && even > 0) {
        cout << "YES\n";
    } else if (odd & 1 && even == 0) {
        cout << "YES\n";
    } else {
        cout << "NO\n";
    }
}

signed main() {
    i32 t;
    cin >> t;
    rep(i, 0, t) solve();
}
