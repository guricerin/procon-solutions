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

vector<int> counting_sort(const vector<int> &as) {
    const int len = as.size();
    const int nax = *max_element(all(as));
    vector<int> cnt(nax + 1, 0);
    rep(i, 0, len) {
        cnt[as[i]]++;
    }
    rep(i, 0, nax) {
        cnt[i + 1] += cnt[i];
    }
    vector<int> bs(len, 0);
    rrep(j, len - 1, 0) {
        bs[cnt[as[j]] - 1] = as[j];
        cnt[as[j]]--;
    }

    return bs;
}

signed main() {
    int N;
    cin >> N;
    vector<int> A(N);
    rep(i, 0, N) cin >> A[i];
    dump_vec(counting_sort(A));
}
