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
    int N, T;
    cin >> N >> T;
    vector<P> abs(N);
    rep(i, 0, N) {
        int a, b;
        cin >> a >> b;
        abs[i].first = a;
        abs[i].second = b;
    }
    sort(all(abs), [](auto x, auto y) { return x.second - x.first < y.second - y.first; });
    // rep(i, 0, N) {
    //     cout << abs[i].first << ",";
    // }
    // cout << "\n";
    int sum = 0;
    rep(i, 0, N) {
        sum += abs[i].first;
    }
    int ans = 0;
    rep(i, 0, N) {
        if (sum > T) {
            sum -= abs[i].first;
            sum += abs[i].second;
            ans++;
        }
    }
    if (sum > T) {
        cout << "-1\n";
    } else {
        cout << ans << "\n";
    }
}
