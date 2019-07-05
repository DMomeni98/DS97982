#include <iostream>
using namespace std;
int GCD(int a, int b)
{
	if(int r=a%b==0)
		return b;
	else{
		a=b;
		b=r;
		return GCD(a,b);
	}
}
int main(){
	int a,b;
	cin>>a;
	cin>>b;
	cout<<GCD(a,b);
	return 0;
}
	
