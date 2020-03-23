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
    int N;
    cin >> N;
    vector<i64> A(N);
    map<i64, i64> mp;
    rep(i, 0, N) {
        i64 a;
        cin >> a;
        A[i] = a;
        mp[a]++;
    }

    // 全事象を求めておく n_C_2
    i64 total = 0;
    for (const auto& p : mp) {
        auto n = p.second;
        total += n * (n - 1) / 2;
    }

    rep(i, 0, N) {
        auto a = A[i];
        auto cnt = mp[a];
        auto org = cnt * (cnt - 1) / 2;
        auto tmp = total - org;
        // A[i]を1つ減らした場合の事象 (k-1)_C_2
        auto subed = (cnt - 1) * (cnt - 2) / 2;
        auto ans = tmp + subed;
        cout << ans << "\n";
    }
}
