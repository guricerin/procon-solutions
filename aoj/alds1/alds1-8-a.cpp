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

struct Node {
    i64 val;
    Node *p, *l, *r;
    Node() {}
    Node(i64 v) : val(v), p(nullptr), l(nullptr), r(nullptr) {}
};

/// 二分探索木（平衡ではないので、最悪計算量はO(n)）
struct BinarySearchTree {
    Node* root;

    BinarySearchTree() : root(nullptr) {}

    void insert(i64 val) {
        auto cur = root;
        Node* p  = nullptr;
        Node* x  = new Node(val);
        while (cur) {
            p = cur;
            if (x->val < cur->val) {
                cur = cur->l; // 左に移動
            } else {
                cur = cur->r; // 右に移動
            }
        }
        x->p = p;

        if (!p) {
            root = x;
        } else if (x->val < p->val) {
            p->l = x;
        } else {
            p->r = x;
        }
    }

    void dump_preorder(const Node* cur) const {
        if (!cur) {
            return;
        }
        cout << " " << cur->val; // 自身
        dump_preorder(cur->l);   // 左
        dump_preorder(cur->r);   // 右
    }

    /// 先行順巡回（dfs）
    void dump_preorder() const {
        dump_preorder(root);
    }

    void dump_inorder(const Node* cur) const {
        if (!cur) {
            return;
        }
        dump_inorder(cur->l);    // 左
        cout << " " << cur->val; // 自身
        dump_inorder(cur->r);    // 右
    }

    /// 中間順巡回（dfs）
    void dump_inorder() const {
        dump_inorder(root);
    }
};

//---------------------------------------------------------------------------------------------------

signed main() {
    int N;
    cin >> N;
    auto tree = BinarySearchTree();
    rep(i, 0, N) {
        string s;
        cin >> s;
        if (s == "insert") {
            i64 v;
            cin >> v;
            tree.insert(v);
        } else {
            tree.dump_inorder();
            cout << "\n";
            tree.dump_preorder();
            cout << "\n";
        }
    }
}
