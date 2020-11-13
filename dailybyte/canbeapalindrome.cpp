#include <iostream>

/*
This question is asked by Facebook. Given a string and the ability to delete at most one character, return whether or not it can form a palindrome.
Note: a palindrome is a sequence of characters that reads the same forwards and backwards.

Ex: Given the following strings...

"abcba", return true
"foobof", return true (remove the first 'o', the second 'o', or 'b')
"abccab", return false
*/

bool canFormAPalindrome(std::string s);

int main() {
  std::cout << "Hello World!\n";

  std::string s = "abcba";
  bool result = canFormAPalindrome(s);

  std::cout << s << " result was " << result << std::endl;

  s = "foobof";
  result = canFormAPalindrome(s);

  std::cout << s << " result was " << result << std::endl;

  s = "abccab";
  result = canFormAPalindrome(s);

  std::cout << s << " result was " << result << std::endl;

  s = "doggosd";
  result = canFormAPalindrome(s);

  std::cout << s << " result was " << result << std::endl;

  s = "dogsffgiod";
  result = canFormAPalindrome(s);

  std::cout << s << " result was " << result << std::endl;
}

bool canFormAPalindrome(std::string s) {

  int skipCount = 0;
  int i = 0;
  int j = s.size() - 1;

  while( i < j) {

    char left = s[i];
    char right = s[j];

    if(left != right && skipCount > 0) {
      return false;
    }
    else if(left != right) {

      if(i+1 ==j) return true;

      char left2 = s[i+1];
      if(left2 == right) {
        skipCount++;
        i++;
      }
      else {
        char right2 = s[j-1];
        if(right2 == left) {
          skipCount++;
          i--;
        }
        else {
          return false;
        }
      }
    }

    i++;
    j--;
  }

  return true;
}