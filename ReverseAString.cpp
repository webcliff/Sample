// Reverse a string.  Write a function that, given a writeable C string, reverses the string in place.

#include <iostream>
#include <cassert>

inline void Swap(char *a, char *b) {
	char t=*a; *a=*b; *b=t;
}

// This implementation only handle the cases where input string is NULL terminated and single byte.
// Won't work with double bytes char (UTF8/UTF16...)
void Reverse(char *head) {
	// we return if NULL 
	if(head==NULL||*head=='\0') return;
	
	char *tail=head;
	while(tail!=NULL && *tail!='\0') ++tail;
	//back to the last char
	--tail;

	while(tail>head) {
		Swap(head,tail);
		--tail; ++head;
	}
}

// Microsoft Visual Studio 2012 C++ compiler
// cl /W3 /Zi /TP /EHsc
void main() {
	char buffer1[] = "ABCDE";
	Reverse(buffer1);
	assert(0==strcmp("EDCBA",buffer1));

	char buffer2[] = "A";
	Reverse(buffer2);
	assert(0==strcmp("A",buffer2));

	char buffer3[] = "";
	Reverse(buffer3);
	assert(0==strcmp("",buffer3));

	//confirm NULL is handled as expected.
	Reverse(NULL);
}

