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
template <class Monoid>
struct SegTree {
    using F = function<Monoid(Monoid, Monoid)>;
    int _size;
    vector<Monoid> _nodes;
    const Monoid _unity;
    const F _merge;
    const F _update;

    int left_child(int k) const { return (k * 2); }
    int right_child(int k) const { return (k * 2) + 1; }
    int leaf_idx(int k) const { return k + _size; }

    SegTree() {}
    SegTree(Monoid unity, const F f, const F g) : _unity(unity), _merge(f), _update(g) {}

    void init(int n) {
        _size = 1;
        while (_size < n) _size <<= 1;
        _nodes.assign(2 * _size, _unity);
    }

    void build(const vector<Monoid>& seq) {
        const int len = seq.size();
        init(len);
        for (int i = 0; i < len; i++) {
            const auto li = leaf_idx(i);
            _nodes[li] = seq[i];
        }
        for (int i = _size - 1; i > 0; i--) {
            const auto lc = left_child(i), rc = right_child(i);
            _nodes[i] = _merge(_nodes[lc], _nodes[rc]);
        }
    }

    /// [l,r), 0-indexed
    Monoid fold(int a, int b) {
        if (a >= b) return _unity;
        Monoid lv = _unity, rv = _unity;
        for (int l = leaf_idx(a), r = leaf_idx(b); l < r; l >>= 1, r >>= 1) {
            if (l & 1) {
                lv = _merge(lv, _nodes[l++]);
            }
            if (r & 1) {
                rv = _merge(_nodes[--r], rv);
            }
        }
        return _merge(lv, rv);
    }

    /// 0-indexed
    void update(int k, Monoid x) {
        int p = leaf_idx(k);
        _nodes[p] = _update(_nodes[p], x);
        while (p >>= 1) {
            const auto lc = left_child(p), rc = right_child(p);
            _nodes[p] = _merge(_nodes[lc], _nodes[rc]);
        }
    }

    /// 0-indexed
    Monoid operator[](int k) const {
        return _nodes[leaf_idx(k)];
    }
};
//---------------------------------------------------------------------------------------------------

signed main() {
    int N, Q;
    cin >> N >> Q;
    auto f = [](int x, int y) { return min(x, y); };
    auto g = [](int x, int y) { return y; };
    const auto uni = numeric_limits<int>::max();
    SegTree<int> seg(uni, f, g);
    seg.init(N);
    rep(i, 0, Q) {
        int q, x, y;
        cin >> q >> x >> y;
        if (q == 0) {
            seg.update(x, y);
        } else {
            cout << seg.fold(x, y + 1) << "\n";
        }
    }
}
