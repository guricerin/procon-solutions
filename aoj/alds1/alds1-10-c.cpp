#include <bits/stdc++.h>
using namespace std;

#define rep(i, a, b) for (int i = (a); i < (int)(b); i++)
#define rrep(i, a, b) for (int i = (a); i >= (int)(b); i--)
#define all(x) (x).begin(), (x).end()
using i32 = int32_t;
using i64 = int64_t;
using f32 = float;
using f64 = double;
using P   = pair<int, int>;

template <class T>
bool chmin(T& a, T b) {
    if (a > b) {
        a = b;
        return true;
    } else {
        return false;
    }
}
template <class T>
bool chmax(T& a, T b) {
    if (a < b) {
        a = b;
        return true;
    } else {
        return false;
    }
}

template <class T>
void dump_vec(const vector<T>& v) {
    auto len = v.size();
    rep(i, 0, len) {
        cout << v[i] << (i == (int)len - 1 ? "\n" : " ");
    }
}

struct FastIO {
    FastIO() {
        cin.tie(0);
        ios::sync_with_stdio(false);
        cout << fixed << setprecision(20);
    }
} FASTIO;

//---------------------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------------------

void solve() {
    string S, T;
    cin >> S >> T;
    int lim = 1005;
    vector<vector<int>> dp(lim, vector<int>(lim, 0));
    int slen = S.size(), tlen = T.size();
    rep(i, 0, slen) rep(j, 0, tlen) {
        if (S[i] == T[j]) {
            dp[i + 1][j + 1] = max(dp[i + 1][j + 1], dp[i][j] + 1);
        }
        dp[i + 1][j + 1] = max(dp[i + 1][j + 1], dp[i + 1][j]);
        dp[i + 1][j + 1] = max(dp[i + 1][j + 1], dp[i][j + 1]);
    }
    cout << dp[slen][tlen] << "\n";
}

signed main() {
    int n;
    cin >> n;
    rep(i, 0, n) solve();
}
