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
struct Process {
    string name;
    int time;
    Process(string n, int t) : name(n), time(t) {}
};

signed main() {
    int N, Q;
    cin >> N >> Q;
    queue<Process> que;
    rep(i, 0, N) {
        string name;
        cin >> name;
        int time;
        cin >> time;
        que.push(Process(name, time));
    }

    vector<Process> ans;
    int now = 0;
    while (!que.empty()) {
        auto p = que.front();
        que.pop();
        if (p.time <= Q) {
            now += p.time;
            ans.push_back(Process(p.name, now));
        } else {
            now += Q;
            p.time -= Q;
            que.push(p);
        }
    }
    for (const auto &p : ans) {
        cout << p.name << " " << p.time << "\n";
    }
}
