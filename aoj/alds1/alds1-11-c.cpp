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

//---------------------------------------------------------------------------------------------------

signed main() {
    int N;
    cin >> N;
    vector<vector<int>> G(N);
    rep(i, 0, N) {
        int id, k;
        cin >> id >> k;
        id--;
        rep(j, 0, k) {
            int u;
            cin >> u;
            u--;
            G[id].push_back(u);
        }
    }

    const auto inf = 1 << 30;
    vector<int> dist(N, inf);
    dist[0] = 0;
    queue<P> q;
    q.push(make_pair(0, 0));
    while (!q.empty()) {
        auto pi = q.front();
        q.pop();
        auto u = pi.first, cost = pi.second;
        for (auto& to : G[u]) {
            if (chmin(dist[to], cost + 1)) {
                q.push(make_pair(to, dist[to]));
            }
        }
    }

    rep(i, 0, N) {
        cout << i + 1 << " " << (dist[i] == inf ? -1 : dist[i]) << "\n";
    }
}
