#include <iostream>

/*
This question is asked by Google. Given a string, return whether or not it uses capitalization correctly. A string correctly uses capitalization if all letters are capitalized, no letters are capitalized, or only the first letter is capitalized.
*/

//  TEST CASES
//  "USA", return true
//  "Calvin", return true
//  "compUter", return false
//  "coding", return true
//  "Hello World", return true
//  "HeLLo wOrld", return false

std::string DisplayValues[] = { "Not Correct", "Correct"};

bool IsUpper(char c) {
  return (int)c >= 65 && (int)c <= 90;
}

bool IsLower(char c) {
  return (int)c >= 97 && (int)c <= 122;
}

bool hasCorrectCapitalization(std::string testString);

int main() {
  std::cout << "Hello World!\n";

  std::string s = "USA";
  bool correct = hasCorrectCapitalization(s);
  std::cout << s << " : the result was " << DisplayValues[(int)correct] << std::endl;

  s = "Calvin";
  correct = hasCorrectCapitalization(s);
  std::cout << s << " : the result was " << DisplayValues[(int)correct] << std::endl;

  s = "compUter";
  correct = hasCorrectCapitalization(s);
  std::cout << s << " : the result was " << DisplayValues[(int)correct] << std::endl;

  s = "coding";
  correct = hasCorrectCapitalization(s);
  std::cout << s << " : the result was " << DisplayValues[(int)correct] << std::endl;

  s = "Hello World";
  correct = hasCorrectCapitalization(s);
  std::cout << s << " : the result was " << DisplayValues[(int)correct] << std::endl;

  s = "HeLLo wOrld";
  correct = hasCorrectCapitalization(s);
  std::cout << s << " : the result was " << DisplayValues[(int)correct] << std::endl;

}

bool hasCorrectCapitalization(std::string testString) {

  int capCount = 0;
  int letters = 0;
  bool firstLetterCapped = false;
  
  for(int i = 0; i < testString.size(); ++i) {
    char c = testString[i];

    if(c == ' ') {
      if(capCount == 0 || (capCount == 1 && firstLetterCapped) || capCount == letters) {
        capCount = 0;
        letters = 0;
        firstLetterCapped = false;
        continue;
      }
      return false;
    }

    ++letters;

    if(IsUpper(c)) {
      ++capCount;
      if(letters==1) {
        firstLetterCapped = true;
      }
      else {
        firstLetterCapped = false;
      }
    }
  }

  return (capCount == 1 && firstLetterCapped) || capCount == 0 || capCount == letters;

}