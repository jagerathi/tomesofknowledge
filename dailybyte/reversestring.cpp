#include <iostream>

std::string reverse(std::string s);

int main() {
  
  std::string toReverse = "Hello, World";

  std::cout << toReverse << std::endl;

  std::string reversed = reverse(toReverse);

  std::cout << reversed << std::endl;

  return 0;
  
}

std::string reverse(std::string s) {

  int i = 0;
  int j = s.length() - 1;

  while(i < j) {
    char c = s[i];
    s[i] = s[j];
    s[j] = c;

    i++;
    j--;
  }

  return s;

}
