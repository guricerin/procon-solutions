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
    i32 n;
    cin >> n;
    vector<i32> ans;

    // 1と7がコスパ最強
    // 基本は桁数を盛りたいので1を使う
    // 7は先頭に挿入する
    while (n > 5) {
        ans.push_back(1);
        n -= 2;
    }
    if (n == 5) {
        ans.insert(ans.begin(), 7);
        ans.push_back(1);
    } else if (n == 4) {
        ans.push_back(1);
        ans.push_back(1);
    } else if (n == 3) {
        ans.insert(ans.begin(), 7);
    } else if (n == 2) {
        ans.push_back(1);
    }

    for (auto c : ans) {
        cout << c;
    }
    cout << "\n";
}

signed main() {
    i32 t;
    cin >> t;
    while (t--)
        solve();
}
