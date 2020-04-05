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

set<i64> memo;

void dfs(i64 cur) {
    rep(i, 0, 10) {
        auto a = cur % 10L;
        if (abs(a - i) > 1) {
            continue;
        }
        auto next = cur * 10L + i;
        // max-caseがサンプルになかったら糞むずい
        if (next > 3234566667L) {
            return;
        }
        memo.insert(next);
        dfs(next);
    }
}

signed main() {
    int K;
    cin >> K;
    rep(i, 1, 10) {
        memo.insert(i);
        dfs(i);
    }
    int cnt = 0;
    for (const auto& a : memo) {
        cnt++;
        if (cnt == K) {
            cout << a << "\n";
            return 0;
        }
    }
}
