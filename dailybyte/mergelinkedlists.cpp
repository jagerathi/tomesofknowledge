#include <iostream>

/*
This question is asked by Apple. Given two sorted linked lists, merge them together in ascending order and return a reference to the merged list

Ex: Given the following lists...

list1 = 1->2->3, list2 = 4->5->6->null, return 1->2->3->4->5->6->null
list1 = 1->3->5, list2 = 2->4->6->null, return 1->2->3->4->5->6->null
list1 = 4->4->7, list2 = 1->5->6->null, return 1->4->4->5->6->7->null
*/

class Node {
public:
  Node() {

  }
  Node(int v) {
    value = v;
  }
  int value;
  Node *next;
};

Node* MergeLists(Node *root1, Node *root2);

void PrintList(Node *root);

Node *BuildList(int values[], int len);

int main() {
  std::cout << "Hello World!\n";

  int values1[] { 1,2,3 };
  Node *root1 = BuildList(values1, 3);

  PrintList(root1);

  int values2[] { 4,5,6 };
  Node *root2 = BuildList(values2, 3);

  PrintList(root2);
  
  Node *combined = MergeLists(root1, root2);

  PrintList(combined);

  int values3[] { 1,5,7 };
  Node *root3 = BuildList(values3, 3);

  PrintList(root3);

  int values4[] { 3,9,11 };
  Node *root4 = BuildList(values4, 3);

  PrintList(root4);
  
  Node *combined2 = MergeLists(root3, root4);

  PrintList(combined2);

}

Node* MergeLists(Node *root1, Node *root2) {

  if(root1==NULL) return root2;
  if(root2==NULL) return root1;

  Node *list1current = root1;
  Node *list2current = root2;
  Node *mergedListCurrent = NULL;
  Node *newRoot = NULL;

  if(list1current->value < list2current->value) {
    newRoot = new Node(list1current->value);
    mergedListCurrent = newRoot;
    list1current = list1current->next;
  }
  else {
    newRoot = new Node(list2current->value);
    mergedListCurrent = newRoot;
    list2current = list2current->next;
  }

  while(list1current != NULL || list2current != NULL) {
    if(list1current == NULL) {
      mergedListCurrent->next = new Node(list2current->value);
      mergedListCurrent = mergedListCurrent->next;
      list2current = list2current->next;
    }
    else if(list2current == NULL) {
      mergedListCurrent->next = new Node(list1current->value);
      mergedListCurrent = mergedListCurrent->next;
      list1current = list1current->next;
    }
    else {
      if(list1current->value < list2current->value) {
        mergedListCurrent->next = new Node(list1current->value);
        mergedListCurrent = mergedListCurrent->next;
        list1current = list1current->next;
      }
      else {
        mergedListCurrent->next = new Node(list2current->value);
        mergedListCurrent = mergedListCurrent->next;
        list2current = list2current->next;
      }
    }
  }
  return newRoot;
}

void PrintList(Node *root) {
  Node *node = root;
  while(node != NULL) {
    std::cout << node->value << "->";

    node = node->next;
  }

  std::cout << "null" << std::endl;
}

Node *BuildList(int values[], int len) {

  Node *root = NULL;
  Node *current = NULL;

  for(int i = 0; i < len; ++i) {

    Node *next = new Node(values[i]);

    if(root == NULL) {
      root = next;
      current = next;
    }
    else {
      current->next = next;
      current = next;
    }
  }

  return root;
};