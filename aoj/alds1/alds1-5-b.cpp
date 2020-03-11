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

int merge(vector<int> &as, int l, int mid, int r) {
    const auto n1  = mid - l;
    const auto n2  = r - mid;
    const auto lim = numeric_limits<int>::max();
    vector<int> L(n1 + 1);
    rep(i, 0, n1) {
        L[i] = as[l + i];
    }
    L[n1] = lim;
    vector<int> R(n2 + 1);
    rep(i, 0, n2) {
        R[i] = as[mid + i];
    }
    R[n2] = lim;
    int i = 0, j = 0;
    rep(k, l, r) {
        if (L[i] <= R[j]) {
            as[k] = L[i];
            i++;
        } else {
            as[k] = R[j];
            j++;
        }
    }
    return r - l;
}

int merge_sort(vector<int> &as, int l, int r) {
    if (abs(l - r) <= 1) return 0;

    auto mid = (l + r) / 2;
    int res  = 0;
    res += merge_sort(as, l, mid);
    res += merge_sort(as, mid, r);
    res += merge(as, l, mid, r);
    return res;
}

signed main() {
    int N;
    cin >> N;
    vector<int> S(N);
    rep(i, 0, N) cin >> S[i];
    int ans = merge_sort(S, 0, S.size());

    dump_vec(S);
    cout << ans << "\n";
}
