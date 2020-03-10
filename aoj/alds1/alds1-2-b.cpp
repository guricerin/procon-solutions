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
//---------------------------------------------------------------------------------------------------

signed main() {
    int N;
    cin >> N;
    vector<int> A(N);
    rep(i, 0, N) {
        cin >> A[i];
    }
    auto ans = selection_sort(A);

    dump_vector(A);
    cout << ans << "\n";
}
