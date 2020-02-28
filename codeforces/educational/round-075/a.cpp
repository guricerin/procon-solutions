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
    string S;
    cin >> S;
    vector<bool> memo(26, false);
    int len = (int)S.size();
    // 連続した区間に奇数個ならんでいる文字ならok
    for (int i = 0; i < len;) {
        auto c = S[i];
        int j  = i;
        while (j < len && S[j] == c) {
            j++;
        }
        auto cnt = j - i;
        if (cnt & 1) memo[c - 'a'] = true;
        i = (i == j ? j + 1 : j);
    }
    rep(i, 0, 26) {
        if (memo[i]) {
            cout << (char)(i + 'a');
        }
    }
    cout << "\n";
}

signed main() {
    i32 t;
    cin >> t;
    while (t--)
        solve();
}
