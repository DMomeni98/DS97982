#include <iostream>
using namespace std;
void Hanoi(int n,char beg,char aux, char end){
	if(n==1)
		cout<<beg<<"-->"<<end<<endl;
	else{
		Hanoi(n-1,beg,end,aux);
		Hanoi(1,beg,aux,end);
		Hanoi(n-1,aux,beg,end);
	}
}
int main(){
	int n=0;
	cin>>n;
	Hanoi(n,'s','a','d');
	return 0;
}
