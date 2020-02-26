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
    i32 t;
    cin >> t;
    rep(i, 0, t) {
        i64 a, b;
        cin >> a >> b;
        if (a == b) {
            cout << 0 << endl;
        } else if (a > b) {
            if (abs(a - b) & 1) {
                cout << 2 << endl;
            } else {
                cout << 1 << endl;
            }
        } else {
            if (abs(a - b) & 1) {
                cout << 1 << endl;
            } else {
                cout << 2 << endl;
            }
        }
    }
}
