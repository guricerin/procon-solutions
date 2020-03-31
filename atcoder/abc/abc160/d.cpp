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
    int N, X, Y;
    cin >> N >> X >> Y;
    i64 inf = (i64)(1 << 40);
    vector<i64> dist(N + 10, inf);
    rep(i, 1, N + 1) rep(j, i, N + 1) {
        // 最短距離は、iからjへ直接行くか、i-X X-Y Y-jか、i-Y Y-X X-j かのうちのどれか
        i64 d = min({abs(i - j), abs(i - X) + 1 + abs(j - Y), abs(i - Y) + 1 + abs(j - X)});
        dist[d]++;
    }
    rep(i, 1, N) {
        cout << dist[i] << "\n";
    }
}
