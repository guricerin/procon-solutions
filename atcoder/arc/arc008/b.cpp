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
    int N, M;
    cin >> N >> M;
    string name;
    cin >> name;
    vector<int> need(26, 0);
    for (auto c : name) {
        need[c - 'A']++;
    }
    string S;
    cin >> S;
    vector<int> have(26, 0);
    for (auto c : S) {
        have[c - 'A']++;
    }

    int ans = 0;
    for (auto c : name) {
        int i = c - 'A';
        if (have[i] == 0) {
            cout << "-1\n";
            return 0;
        }
        auto tmp = (need[i] + have[i] - 1) / have[i];
        chmax(ans, tmp);
    }
    cout << ans << "\n";
}
