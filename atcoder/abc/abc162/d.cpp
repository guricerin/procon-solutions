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

vector<int> cum(char c, string& s) {
    int n = s.size();
    vector<int> res(n + 1);
    rep(i, 1, n + 1) {
        res[i] = res[i - 1] + (s[i - 1] == c);
    }
    return res;
}

signed main() {
    int N;
    cin >> N;
    string S;
    cin >> S;
    vector<P> ab;
    rep(i, 0, N) rep(j, i + 1, N) {
        if (S[i] != S[j]) {
            ab.push_back(P(i, j));
        }
    }
    const auto R = cum('R', S);
    const auto G = cum('G', S);
    const auto B = cum('B', S);
    i64 ans = 0;
    for (const auto& p : ab) {
        auto i = p.first;
        auto j = p.second;
        i64 tmp = 0;
        if ((S[i] == 'R' && S[j] == 'G') || (S[i] == 'G' && S[j] == 'R')) {
            tmp += B[N] - B[j];
        } else if ((S[i] == 'R' && S[j] == 'B') || (S[i] == 'B' && S[j] == 'R')) {
            tmp += G[N] - G[j];
        } else if ((S[i] == 'G' && S[j] == 'B') || (S[i] == 'B' && S[j] == 'G')) {
            tmp += R[N] - R[j];
        }
        auto k = j + (j - i);
        if (k < N && S[k] != S[i] && S[k] != S[j]) tmp--;
        ans += tmp;
    }
    cout << ans << "\n";
}
