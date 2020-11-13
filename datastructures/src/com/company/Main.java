package com.company;

public class Main {

    public static void main(String[] args) {

        Array numbers = new Array(3);

        numbers.insert(10);
        numbers.insert(20);
        numbers.insert(30);
        numbers.insert(40);

        numbers.removeAt(0);

        numbers.print();

        int index = numbers.indexOf(20);

        System.out.println(index);
    }
}
