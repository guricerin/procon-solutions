/**
 * Definition for singly-linked list.
 * struct ListNode {
 *     int val;
 *     ListNode *next;
 *     ListNode(int x) : val(x), next(NULL) {}
 * };
 */
class Solution {
public:
    ListNode* addTwoNumbers(ListNode* l1, ListNode* l2) {
        auto res = new ListNode(0);
        auto cur = res;
        int carry = 0;
        while (l1 || l2) {
            cur->next = new ListNode(0);
            cur = cur->next;
            int v = carry;
            if (l1) v += l1->val;
            if (l2) v += l2->val;
            cur->val = v % 10;
            carry = v / 10;
            if (l1) l1 = l1->next;
            if (l2) l2 = l2->next;
        }
        if (carry) {
            cur->next = new ListNode(carry);
        }
        return res->next;
    }
};