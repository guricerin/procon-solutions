#include <bits/stdc++.h>
using namespace std;

#define rep(i, a, b) for (int i = (a); i < (int)(b); i++)
#define rrep(i, a, b) for (int i = (a); i >= (int)(b); i--)
#define all(x) (x).begin(), (x).end()
using i32 = int32_t;
using i64 = int64_t;
using f32 = float;
using f64 = double;
using P = pair<int, int>;

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

signed main() {
    i64 K, N;
    cin >> K >> N;
    vector<i64> A(N);
    rep(i, 0, N) cin >> A[i];
    // 周Kメートルのドーナツからある1片を取り除いていくイメージ
    auto ans = K - (A[0] + abs(K - A[N - 1]));
    rep(i, 0, N - 1) {
        auto d = abs(A[i] - A[i + 1]);
        auto t = K - d;
        chmin(ans, t);
    }
    cout << ans << "\n";
}
