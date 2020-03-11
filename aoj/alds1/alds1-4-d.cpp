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

bool isok(const vector<i64> &ws, int k, i64 upper) {
    int j   = 0;
    int len = ws.size();
    bool ok = false;
    rep(i, 0, k) {
        i64 sum = 0;
        while (sum + ws[j] <= upper && j < len) {
            sum += ws[j];
            j++;
            // 上限を超えることなくn個の荷物を割り当てられた
            if (j == len) ok = true;
        }
    }
    return ok;
}

signed main() {
    int N, K;
    cin >> N >> K;
    vector<i64> ws(N);
    rep(i, 0, N) {
        i64 w;
        cin >> w;
        ws[i] = w;
    }

    i64 ng = -1, ok = 1e10;
    while (abs(ok - ng) > 1) {
        auto mid = (ok + ng) / 2;
        if (isok(ws, K, mid))
            ok = mid;
        else
            ng = mid;
    }
    cout << ok << "\n";
}
