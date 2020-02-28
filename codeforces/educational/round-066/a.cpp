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

signed main() {
    int t;
    cin >> t;
    while (t--) {
        i64 n, x;
        cin >> n >> x;
        i64 ans = 0;
        while (n > 1) {
            auto nx = n / x * x;
            ans += abs(n - nx);
            n = nx;
            while (n % x == 0 && n > 1) {
                ans++;
                n /= x;
            }
        }
        if (n == 1) ans++;
        cout << ans << "\n";
    }
}
