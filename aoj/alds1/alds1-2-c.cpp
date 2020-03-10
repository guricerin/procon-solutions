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
void dump_vector(const vector<T> &v) {
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

template <class T>
int selection_sort(vector<T> &vec) {
    auto len = vec.size();
    int cnt  = 0;
    rep(i, 0, len) {
        auto minj = i;
        rep(j, i, len) {
            if (vec[j] < vec[minj]) {
                minj = j;
            }
        }
        if (i != minj) {
            swap(vec[i], vec[minj]);
            cnt++;
        }
    }
    return cnt;
}

template <class T>
int bubble_sort(vector<T> &vec) {
    auto len = vec.size();
    int cnt  = 0;    // スワップ回数
    bool flg = true; // 逆の隣接要素が存在する
    while (flg) {
        flg = false;
        for (int i = len - 1; i >= 1; i--) {
            if (vec[i] < vec[i - 1]) {
                swap(vec[i], vec[i - 1]);
                flg = true;
                cnt++;
            }
        }
    }
    return cnt;
}

struct Card {
    string value;
    int idx;

    bool operator<(const Card &rhs) const {
        return value[1] < rhs.value[1];
    }
    bool operator==(const Card &rhs) const {
        return value == rhs.value;
    }
    bool operator!=(const Card &rhs) const {
        return value != rhs.value;
    }
    friend ostream &operator<<(ostream &os, const Card &x) {
        return os << x.value;
    }
};

//---------------------------------------------------------------------------------------------------

bool is_stable(const vector<Card> &sorted) {
    const auto len = sorted.size();
    bool ok        = true;
    rep(i, 0, len - 1) {
        if (sorted[i].value[1] == sorted[i + 1].value[1] && sorted[i].idx > sorted[i + 1].idx) {
            ok = false;
        }
    }
    return ok;
}

signed main() {
    int N;
    cin >> N;
    vector<Card> ls(N);
    rep(i, 0, N) {
        string s;
        cin >> s;
        ls[i].value = s;
        ls[i].idx   = i;
    }

    vector<Card> x(N);
    copy(all(ls), x.begin());
    bubble_sort(x);
    dump_vector(x);
    cout << (is_stable(x) ? "Stable" : "Not stable") << "\n";

    copy(all(ls), x.begin());
    selection_sort(x);
    dump_vector(x);
    cout << (is_stable(x) ? "Stable" : "Not stable") << "\n";
}
