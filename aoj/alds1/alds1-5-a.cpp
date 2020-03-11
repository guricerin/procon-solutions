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

template <class T>
void dump_vec(const vector<T> &v) {
    auto len = v.size();
    rep(i, 0, len) {
        cout << v[i] << (i == (int)len - 1 ? "\n" : " ");
    }
}

struct Setup {
    Setup() {
        cin.tie(0);
        ios::sync_with_stdio(false);
        cout << fixed << setprecision(20);
    }
} SETUP;

//---------------------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------------------

bool dp[1 << 21];

void init(vector<int> as) {
    const int len = as.size();
    rep(bit, 0, 1 << len) {
        int sum = 0;
        rep(i, 0, len) {
            if (bit & (1 << i)) sum += as[i];
        }
        dp[sum] = true;
    }
}

signed main() {
    int N;
    cin >> N;
    vector<int> A(N);
    rep(i, 0, N) cin >> A[i];
    // Q(<=200)ごとに全探索(2^20)するのは無駄なのでメモ化する
    init(A);
    int Q;
    cin >> Q;
    rep(i, 0, Q) {
        int m;
        cin >> m;
        cout << (dp[m] ? "yes" : "no") << "\n";
    }
}
