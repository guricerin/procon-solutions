#include <bits/stdc++.h>
using namespace std;

#define rep(i, a, b) for (int i = (a); i < (int)(b); ++i)
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

template <class T>
void dump_vector(const vector<T> &v) {
    auto len = v.size();
    rep(i, 0, len) {
        cout << v[i] << (i == len - 1 ? "\n" : " ");
    }
}

template <class T>
void insertion_sort(vector<T> vec) {
    auto len = vec.size();
    rep(i, 1, len) {
        auto a = vec[i];
        auto j = i - 1;
        while (j >= 0 && vec[j] > a) {
            vec[j + 1] = vec[j];
            j--;
        }
        vec[j + 1] = a;
        dump_vector(vec);
    }
}

signed main() {
    int N;
    cin >> N;
    vector<int> A(N);
    rep(i, 0, N) {
        cin >> A[i];
    }
    dump_vector(A);
    insertion_sort(A);
}
