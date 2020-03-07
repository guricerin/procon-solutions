#include <bits/stdc++.h>
using namespace std;

#define rep(i, a, b) for (int i = (a); i < (b); ++i)
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

struct Setup {
    Setup() {
        cin.tie(0);
        ios::sync_with_stdio(false);
        cout << fixed << setprecision(20);
    }
} SETUP;

signed main() {
    string s;
    cin >> s;
    int Q;
    cin >> Q;
    deque<char> que;
    int front = 1;
    for (auto c : s) {
        que.push_back(c);
    }
    rep(i, 0, Q) {
        int q;
        cin >> q;
        if (q == 1) {
            front ^= 1;
        } else {
            int f;
            cin >> f;
            char c;
            cin >> c;
            if (f == 1) {
                if (front)
                    que.push_front(c);
                else
                    que.push_back(c);
            } else {
                if (front)
                    que.push_back(c);
                else
                    que.push_front(c);
            }
        }
    }
    string ans = "";
    for_each(all(que), [&](char c) {
        ans += c;
    });
    if (!front) reverse(all(ans));
    cout << ans << "\n";
}
