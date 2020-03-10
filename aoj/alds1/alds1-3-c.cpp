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

signed main() {
    int N;
    cin >> N;
    list<int> ls;
    rep(i, 0, N) {
        string s;
        cin >> s;
        if (s == "insert") {
            int x;
            cin >> x;
            ls.push_front(x);
        } else if (s == "delete") {
            int x;
            cin >> x;
            for (auto it = ls.begin(); it != ls.end(); it++) {
                if (*it == x) {
                    it = ls.erase(it);
                    break;
                }
            }
        } else if (s == "deleteFirst") {
            ls.pop_front();
        } else if (s == "deleteLast") {
            ls.pop_back();
        }
    }

    vector<int> ans;
    for (auto l : ls)
        ans.push_back(l);
    dump_vec(ans);
}
