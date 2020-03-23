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
    // 面積を最大化するには立方体にしたほうが良い
    // a^3
    // これに対してある一片をa, ある一片を a-x にすると、最後の一辺は a+x
    // a * (a-x) * (a+x) = a(a^2 - x^2) = a^3 - ax^2 < a^3

    // 公式解説では相加相乗平均不等式を使っている
    // それぞれの辺の長さをa,b,cとする
    // (abc)^(1/3) <= (a+b+c)/3
    // 両辺を3乗して、abc <= ((a+b+c)/3)^3
    // 等号成立条件は a = b = c のときなので、辺の長さは3つとも L/3にしたとき, つまり立方体のときに面積が最大化する
    f64 a;
    cin >> a;
    auto b = a / 3.0;
    auto ans = b * b * b;
    cout << ans << "\n";
}
