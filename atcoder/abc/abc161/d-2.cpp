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
    int K;
    cin >> K;
    // 今回の場合、dfsではなくbfsならmax-caseが示されてなくても答えられる
    queue<i64> q;
    rep(i, 1, 10) q.push(i);
    int k = 0;
    while (!q.empty()) {
        i64 x = q.front();
        q.pop();
        k++;
        if (k == K) {
            cout << x << "\n";
            return 0;
        }
        i64 a = x % 10;
        if (a != 0) q.push(x * 10 + a - 1);
        q.push(x * 10 + a);
        if (a != 9) q.push(x * 10 + a + 1);
    }
}
