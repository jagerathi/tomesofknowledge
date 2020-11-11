#include <iostream>
#include <vector>

/*
This question is asked by Microsoft. Given an array of strings, return the longest common prefix that is shared amongst all strings.
Note: you may assume all strings only contain lowercase alphabetical characters.

Ex: Given the following arrays...

["colorado", "color", "cold"], return "col"
["a", "b", "c"], return ""
["spot", "spotty", "spotted"], return "spot"
*/

std::string LongestCommonPrefix(std::vector<std::string> strings);

int main() {
  std::cout << "Hello World!\n";

  std::vector<std::string> strings = { "colorado", "color", "cold"};
  std::string longest = LongestCommonPrefix(strings);

  std::cout << "Longest Common Prefix is " << longest << std::endl;

  strings = { "a", "b", "c"};
  longest = LongestCommonPrefix(strings);

  std::cout << "Longest Common Prefix is " << longest << std::endl;

  strings = { "spot", "spotty", "spotted"};
  longest = LongestCommonPrefix(strings);

  std::cout << "Longest Common Prefix is " << longest << std::endl;

}

std::string LongestCommonPrefix(std::vector<std::string> strings) {

  std::string result = "";
  int index = 0;

  char currentChar = '0';
  do {
    for(int i = 0; i < strings.size(); ++i) {

      std::string s = strings[i];
      if(s.size() <= index) return result;

      char current = s[index];

      if(i==0) {
        currentChar = current;
      }
      else {
        if(currentChar != current) return result;
      }
    }

    result = result + currentChar;
    ++index;
  }
  while(currentChar != '0');

  return result;
}