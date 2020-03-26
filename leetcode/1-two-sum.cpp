class Solution {
public:
    vector<int> twoSum(vector<int>& nums, int target) {
        int len = nums.size();
        map<int,int> mp;
        for (int i = 0;i < len;i++) {
            auto n = nums[i];
            auto rem = target - n;
            if (mp.find(rem) != mp.end()) {
                vector<int> res({i,mp[rem]});
                return res;
            }
            mp[n] = i;
        }
        return vector<int>({-1,-1});
    }
};