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
bool chmin(T &a, T b) {
    if (a > b) {
        a = b;
        return true;
    } else {
        return false;
    }
}
template <class T>
bool chmax(T &a, T b) {
    if (a < b) {
        a = b;
        return true;
    } else {
        return false;
    }
}

template <class T>
void dump_vec(const vector<T> &v) {
    auto len = v.size();
    rep(i, 0, len) {
        cout << v[i] << (i == (int)len - 1 ? "\n" : " ");
    }
}

struct Setup {
    Setup() {
        cin.tie(0);
        ios::sync_with_stdio(false);
        cout << fixed << setprecision(20);
    }
} SETUP;

//---------------------------------------------------------------------------------------------------

struct Edge {
    int from, to;
    i64 cost;
    Edge() {}
    Edge(int t, i64 c) : from(-1), to(t), cost(c) {}
    Edge(int f, int t, i64 c) : from(f), to(t), cost(c) {}

    bool operator<(const Edge &r) const {
        return cost < r.cost;
    }
};

using Vertex = vector<Edge>;
using Graph  = vector<Vertex>;

// 無向
void link(Graph &g, int from, int to, int cost = 1) {
    g[from].push_back(Edge(from, to, cost));
    g[to].push_back(Edge(to, from, cost));
    return;
}

// 有向
void connect(Graph &g, int from, int to, int cost = 1) {
    g[from].push_back(Edge(from, to, cost));
    return;
}

//---------------------------------------------------------------------------------------------------

// 木の高さ
int dfs(const Graph &g, int s) {
    if (g[s].empty()) {
        return 0;
    }

    // 部分木のうち、でかいほうを採用
    int res = 0;
    for (auto &e : g[s]) {
        chmax(res, dfs(g, e.to));
    }
    return res + 1;
}

signed main() {
    int N;
    cin >> N;
    Graph graph(N);
    vector<int> pars(N, -1);
    rep(i, 0, N) {
        int id, lnode, rnode;
        cin >> id >> lnode >> rnode;
        if (lnode >= 0) {
            pars[lnode] = id;
            connect(graph, id, lnode);
        }
        if (rnode >= 0) {
            pars[rnode] = id;
            connect(graph, id, rnode);
        }
    }

    rep(id, 0, N) {
        printf("node %d: ", id);

        const auto par = pars[id];
        printf("parent = %d, ", par);

        int sibling = -1;
        if (par != -1) {
            for (int ci = 0; ci < (int)graph[par].size(); ci++) {
                if (graph[par][ci].to != id) sibling = graph[par][ci].to;
            }
        }
        printf("sibling = %d, ", sibling);

        const int deg = graph[id].size();
        printf("degree = %d, ", deg);

        int depth = 0;
        for (int j = id; pars[j] != -1; j = pars[j])
            depth++;
        printf("depth = %d, ", depth);

        const int height = dfs(graph, id);
        printf("height = %d, ", height);

        string ty;
        if (par == -1) {
            ty = "root";
        } else if (!graph[id].empty()) {
            ty = "internal node";
        } else {
            ty = "leaf";
        }
        printf("%s\n", ty.c_str());
    }
}
