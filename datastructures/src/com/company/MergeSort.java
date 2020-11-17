package com.company;

import java.util.ArrayList;

public class MergeSort {

    public int[] Sort(int[] input) {

        if(input.length==1) return input;

        int middle = input.length / 2;



        return new int[0];
    }

    private int[] sort(int[] input, int startingIndex, int count) {
        return null;
    }

    private int[] merge(int[] left, int[] right) {
        int i = 0;
        int j = 0;
        int[] mergedArray = new int[left.length + right.length];
        int index = 0;

        while(i < left.length && j < right.length) {
            if(left[i] < right[i]) {
                mergedArray[index] = left[i];
                index++;
                i++;
            }
            else {
                mergedArray[index] = right[j];
                index++;
                j++;
            }
        }

        return mergedArray;
    }

}
