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
    i64 N, S;
    cin >> N >> S;
    auto A  = vector<i64>(N);
    i64 sum = 0L;
    rep(i, 0, N) {
        i64 a;
        cin >> a;
        A[i] = a;
        sum += a;
    }
    if (sum <= S) {
        cout << 0 << "\n";
        return;
    }

    i64 nax = A[0];
    i32 pos = 0;
    sum     = 0L;
    rep(i, 0, N) {
        sum += A[i];
        if (chmax(nax, A[i])) {
            pos = i;
        }
        if (sum > S) {
            cout << pos + 1 << "\n";
            return;
        }
    }
}

signed main() {
    i32 t;
    cin >> t;
    rep(i, 0, t) {
        solve();
    }
}
