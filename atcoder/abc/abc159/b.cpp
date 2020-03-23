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
    // やるだけ（だが汚ねえコード）
    string S;
    cin >> S;
    auto T = S;
    reverse(all(T));
    bool ok1 = T == S;

    int len = S.size();
    string a = "";
    rep(i, 0, (len - 1) / 2) {
        a += S[i];
    }
    string b = a;
    reverse(all(b));
    bool ok2 = a == b;

    string c = "";
    rep(i, (len + 3) / 2 - 1, len) {
        c += S[i];
    }
    string d = c;
    reverse(all(d));
    bool ok3 = c == d;
    // cout << a << " " << b << " " << c << " " << d << "\n";
    cout << (ok1 && ok2 && ok3 ? "Yes" : "No") << "\n";
}
