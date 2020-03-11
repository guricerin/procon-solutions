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
void undirected_connect(Graph &g, int from, int to, int cost = 1) {
    g[from].push_back(Edge(from, to, cost));
    g[to].push_back(Edge(to, from, cost));
    return;
}

// 有向
void directed_connect(Graph &g, int from, int to, int cost = 1) {
    g[from].push_back(Edge(from, to, cost));
    return;
}

signed main() {
    int N;
    cin >> N;
    Graph graph(N);
    // 親ノード
    vector<int> pars(N, -1);
    rep(i, 0, N) {
        int id;
        cin >> id;
        int K;
        cin >> K;
        rep(j, 0, K) {
            int c;
            cin >> c;
            // 親ノードを別のデータで管理しているから、子供しか見なくて良い -> 有向グラフで考えてよい
            directed_connect(graph, id, c);
            pars[c] = id;
        }
    }

    rep(i, 0, N) {
        const auto par = pars[i];
        printf("node %d: parent = %d, ", i, par);

        int depth = 0;
        // 根方向に巡回
        for (int j = i; pars[j] != -1; j = pars[j])
            depth++;
        printf("depth = %d, ", depth);

        string s;
        if (depth == 0) {
            s = "root";
        } else if (graph[i].empty()) {
            s = "leaf";
        } else {
            s = "internal node";
        }
        printf("%s, ", s.c_str());

        printf("[");
        vector<int> cs;
        rep(j, 0, graph[i].size()) {
            auto c = graph[i][j].to;
            printf("%d", c);
            if (j < (int)graph[i].size() - 1) {
                printf(", ");
            }
        }
        printf("]\n");
    }
}
