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
    int X, Y, A, B, C;
    cin >> X >> Y >> A >> B >> C;
    vector<i64> red(A);
    rep(i, 0, A) cin >> red[i];
    vector<i64> green(B);
    rep(i, 0, B) cin >> green[i];
    vector<i64> cless(C);
    rep(i, 0, C) cin >> cless[i];
    sort(all(red));
    reverse(all(red));
    sort(all(green));
    reverse(all(green));
    sort(all(cless));
    reverse(all(cless));

    // ヒープを使っていい感じに貪欲させる
    priority_queue<i64> que;
    rep(i, 0, X) que.push(red[i]);
    rep(i, 0, Y) que.push(green[i]);
    rep(i, 0, C) que.push(cless[i]);

    i64 ans = 0;
    rep(i, 0, X + Y) {
        auto a = que.top();
        que.pop();
        ans += a;
    }
    cout << ans << "\n";
}
