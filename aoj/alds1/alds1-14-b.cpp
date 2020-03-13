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
/// 最長共通接頭辞
/// 文字列Sの左からi番目について、SとS[i..|S|]が何文字が一致しているかを処理
/// O(|S|)
namespace z_algorithm {

vector<int> run(const string& s) {
    const int len = s.size();
    vector<int> prefix(len);
    for (int i = 1, j = 0; i < len; i++) {
        if (i + prefix[i - j] < j + prefix[j]) {
            prefix[i] = prefix[i - j];
        } else {
            int k = max(0, j + prefix[j] - i);
            while (i + k < len && s[k] == s[i + k]) {
                k++;
            }
            prefix[i] = k;
            j         = i;
        }
    }
    prefix[0] = len;
    return prefix;
}

/// Tと一致する部分についてのSの左端の位置
vector<int> search(const string& s, const string& t) {
    const auto zs  = run(t + s);
    const int tlen = t.size();
    vector<int> res;
    for (int i = 0; i < (int)s.size(); i++) {
        if (zs[tlen + i] >= tlen) {
            res.push_back(i);
        }
    }
    return res;
}

}; // namespace z_algorithm
//---------------------------------------------------------------------------------------------------

signed main() {
    string S, T;
    cin >> S >> T;

    auto ans = z_algorithm::search(S, T);
    rep(i, 0, ans.size()) {
        cout << ans[i] << "\n";
    }
}
