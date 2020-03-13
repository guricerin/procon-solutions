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

/// 素集合データ構造: Union Find Tree (Disjoint Set)
struct UnionFind {
    vector<int> _ids; // 添字iが属する集合のID

    UnionFind(int n) {
        _ids.assign(n, -1);
    }

    int root(int x) {
        const auto rx = _ids[x];
        return (rx < 0 ? x : _ids[x] = root(rx));
    }

    // 要素xとyは同じ連結成分に属するか？
    bool connected(int x, int y) {
        return root(x) == root(y);
    }

    // 要素xが属する集合の要素数
    int size(int x) {
        return -_ids[root(x)];
    }

    // 要素xとyを連結
    bool unite(int x, int y) {
        auto rx = root(x), ry = root(y);
        if (rx == ry) return false;

        if (_ids[rx] > _ids[ry]) swap(rx, ry);
        _ids[rx] += _ids[ry];
        _ids[ry] = rx;
        return true;
    }
};

//---------------------------------------------------------------------------------------------------

signed main() {
    int N, Q;
    cin >> N >> Q;
    UnionFind uf(N);
    rep(i, 0, Q) {
        int c, x, y;
        cin >> c >> x >> y;
        if (c == 0) {
            uf.unite(x, y);
        } else {
            cout << (uf.connected(x, y) ? 1 : 0) << "\n";
        }
    }
}
