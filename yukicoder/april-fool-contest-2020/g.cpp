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
int dx[] = {0, -1, 1, 0};
int dy[] = {-1, 0, 0, 1};
//---------------------------------------------------------------------------------------------------

signed main() {
    int R, C;
    cin >> R >> C;
    int sx, sy, gx, gy;
    cin >> sy >> sx >> gy >> gx;
    sy--;
    sx--;
    gy--;
    gx--;
    vector<string> maze(R);
    rep(i, 0, R) {
        string s;
        cin >> s;
        maze[i] = s;
    }

    int inf = (int)(1e9);
    vector<vector<int>> dist(R, vector<int>(C, inf));
    dist[sy][sx] = 0;
    queue<P> q;
    q.push(P(sy, sx));
    while (!q.empty()) {
        auto p = q.front();
        q.pop();
        int y = p.first, x = p.second;
        if (y == gy && x == gx) break;
        rep(i, 0, 4) {
            int ny = y + dy[i], nx = x + dx[i];
            if (!(0 <= ny && ny < R && 0 <= nx && nx < C)) {
                continue;
            }
            // 壁マスを通ってはいけない、とは書いていない
            // if (maze[ny][nx] == '#') continue;
            int cost = dist[y][x] + 1;
            if (chmin(dist[ny][nx], cost)) {
                q.push(P(ny, nx));
            }
        }
    }
    cout << dist[gy][gx] << "\n";
}
