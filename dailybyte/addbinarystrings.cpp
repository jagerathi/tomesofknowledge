#include <iostream>

/*
This question is asked by Apple. Given two binary strings (strings containing only 1s and 0s) return their sum (also as a binary string).
Note: neither binary string will contain leading 0s unless the string itself is 0

Ex: Given the following binary strings...

"100" + "1", return "101"
"11" + "1", return "100"
"1" + "0", return  "1"
*/

std::string addBinaryStrings(std::string s1, std::string s2);

int main() {

  std::string s1 = "100";
  std::string s2 = "1";

  std::string result = addBinaryStrings(s1, s2);

  std::cout << s1 << " + " << s2 << " = " << result << std::endl;

  s1 = "11";
  s2 = "1";

  result = addBinaryStrings(s1, s2);

  std::cout << s1 << " + " << s2 << " = " << result << std::endl;

  s1 = "1";
  s2 = "0";

  result = addBinaryStrings(s1, s2);

  std::cout << s1 << " + " << s2 << " = " << result << std::endl;

}

std::string addBinaryStrings(std::string s1, std::string s2) {

  std::string result;
  int s = 0;

  int i = s1.size() - 1;
  int j= s2.size() - 1;

  while( i >= 0 || j >= 0 || s == 1) {

    s += (i >= 0) ? s1[i] - '0' : 0;
    s += (j >= 0) ? s2[j] - '0' : 0;

    result = char(s % 2 + '0') + result;

    s /= 2;

    --i;
    --j;
  }

  return result;
}