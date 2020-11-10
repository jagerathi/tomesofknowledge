#include <iostream>

/*
This question is asked by Facebook. Given a string, return whether or not it forms a palindrome ignoring case and non-alphabetical characters.
Note: a palindrome is a sequence of characters that reads the same forwards and backwards.

Ex: Given the following strings...

"level", return true
"algorithm", return false
"A man, a plan, a canal: Panama.", return true
*/

bool isValidPalindrome(std::string s);
bool isValidCharacter(char c);
char getNextFromLeft(std::string s, int index);
char getNextFromRight(std::string s, int index);

int main() {

  std::string s = "level";
  
  bool check = isValidPalindrome(s);
  std::cout << s << " : Is it a valid Palindrome? " << check << std::endl;

  s = "algorithm";
  
  check = isValidPalindrome(s);
  std::cout << s << " : Is it a valid Palindrome? " << check << std::endl;

  s = "A man, a plan, a canal: Panama.";
  
  check = isValidPalindrome(s);
  std::cout << s << " : Is it a valid Palindrome? " << check << std::endl;

}

bool isValidPalindrome(std::string s) {

  int i = 0;
  int j = s.size() - 1;

  while(i < j) {
    char left = ' ';
    while(i < s.size()) {
      left = tolower(s[i]);
      if(isValidCharacter(left)) {
        break;
      }
      ++i;
    }

    char right = '0';
    while( j > 0) {

      right = tolower(s[j]);
      if(isValidCharacter(right)) {
        break;
      }
      --j;
    }

    if(left != right) return false;

    ++i;
    --j;
  }
  return true;
}

bool isValidCharacter(char c) {
  return c >= 97 && c <= 122;
}