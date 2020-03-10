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
void dump_vector(const vector<T> &v) {
    auto len = v.size();
    rep(i, 0, len) {
        cout << v[i] << (i == len - 1 ? "\n" : " ");
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

bool is_prime(i64 n) {
    if (n <= 1) return false;
    for (i64 p = 2; p * p <= n; p++) {
        if (n % p == 0) return false;
    }
    return true;
}
//---------------------------------------------------------------------------------------------------

signed main() {
    int N;
    cin >> N;
    int cnt = 0;
    rep(i, 0, N) {
        int a;
        cin >> a;
        if (is_prime(a)) cnt++;
    }
    cout << cnt << "\n";
}
