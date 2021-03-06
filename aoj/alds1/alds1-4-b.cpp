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

signed main() {
    int N;
    cin >> N;
    vector<int> S(N);
    rep(i, 0, N) cin >> S[i];
    int Q;
    cin >> Q;
    vector<int> T(Q);
    rep(i, 0, Q) cin >> T[i];

    int ans = 0;
    sort(all(S));
    rep(i, 0, Q) {
        auto t = T[i];
        int ok = N;
        int ng = -1;
        while (abs(ok - ng) > 1) {
            auto mid = (ok + ng) / 2;
            if (S[mid] >= t)
                ok = mid;
            else
                ng = mid;
        }
        if (S[ok] == t) ans++;
    }
    cout << ans << "\n";
}
