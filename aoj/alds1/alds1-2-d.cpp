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

template <class T>
int insertion_sort(vector<T> &A, const int n, const int g) {
    int cnt = 0;
    rep(i, g, n) {
        auto v = A[i];
        auto j = i - g;
        while (j >= 0 && A[j] > v) {
            A[j + g] = A[j];
            j        = j - g;
            cnt++;
        }
        A[j + g] = v;
    }
    return cnt;
}

template <class T>
void shell_sort(vector<T> &A, int n) {
    vector<int> G;
    auto gap = n - 1;
    while (gap > 1) {
        G.push_back(gap);
        gap /= 2;
    }
    G.push_back(1);

    const auto m = G.size();
    cout << m << "\n";
    dump_vec(G);

    int cnt = 0;
    rep(i, 0, m) {
        cnt += insertion_sort(A, n, G[i]);
    }
    cout << cnt << "\n";
}

signed main() {
    int N;
    cin >> N;
    vector<int> A(N);
    rep(i, 0, N) {
        cin >> A[i];
    }
    shell_sort(A, N);
    rep(i, 0, N) {
        cout << A[i] << "\n";
    }
}
