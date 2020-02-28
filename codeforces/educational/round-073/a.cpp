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

bool check(map<int, int> &mp) {
    for (auto [k, v] : mp) {
        if (k < 2048 && v > 1) return true;
    }
    return false;
}

void solve() {
    int N;
    cin >> N;
    map<int, int> mp;
    rep(i, 0, N) {
        int a;
        cin >> a;
        if (a <= 2048)
            mp[a]++;
    }

    while (check(mp)) {
        vector<int> del;
        vector<int> add;
        for (auto [k, v] : mp) {
            if (k < 2048 && v >= 2) {
                del.push_back(k);
                add.push_back(k * 2);
            }
        }
        for (auto d : del) {
            mp[d] -= 2;
        }
        for (auto k : add) {
            mp[k]++;
        }
    }
    if (mp[2048] > 0)
        cout << "YES\n";
    else
        cout << "NO\n";
}

signed main() {
    int q;
    cin >> q;
    while (q--) {
        solve();
    }
}
