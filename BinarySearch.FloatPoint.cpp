// Binary search.  Write a function that takes as input a sorted array of floating point 
// numbers and a target floating point number.  
// It should return the position of the target, if found, or indicate that the target was not found.

#include <iostream>
#include <cassert>

// this is a relative epsilon approach
inline bool rEqual(double a, double b) {
	if(a==b) return true;
	double rDiff = fabs((a-b)/b);
	return rDiff<=1E-9;
}

int bsearch(double A[], int len, double V) {
	assert(len>=0); // release should ignore assert
	int l=0,r=len-1;

	while(l<=r) {
		int m=l+(r-l)/2;
		if(rEqual(A[m],V)) return m;
		else if(A[m]<V) l=m+1;
		else r=m-1;
	}
	//return negative int if not found.
	return -1;	
}

// Microsoft Visual Studio 2012 C++ compiler
// cl /W3 /Zi /TP /EHsc
void main() {
	double A[] = {1.23454545, 1.23454546, 2.34, 3.45, 4.56, 100.023};
	int len = sizeof(A)/sizeof(A[0]);

	assert(4 == bsearch(A,len,4.56));
	assert(-1 == bsearch(A,len,2.33));

	assert(0 == bsearch(A,len,1.23454545));
	assert(1 == bsearch(A,len,1.23454546));

	// this return A[0] since rDiff with A[0] <=1E-9
	assert(0 == bsearch(A,len,1.2345454501));

	// this return A[0] since rDiff with A[0] >1E-9
	assert(-1 == bsearch(A,len,1.234545452));


	// only one element
	double B[] = {100.23};
	len = sizeof(B)/sizeof(B[0]);

	assert(-1 == bsearch(B,len,4.56));
	assert(0 == bsearch(B,len,100.23));
}

