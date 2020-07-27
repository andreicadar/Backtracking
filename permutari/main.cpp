#include <iostream>

using namespace std;

int x[30],n;
int y=0;

void afisare2()
{
    for(int i = 1;i<=n;i++)
        cout<<x[i]<<" ";
    cout<<endl;
}

void BK(int k)
{
    int i;
    for(i=v[k-1]; i<=n; i++)
    {
        v[k]=i;
        if(valid(k))
            if(k==x)
                {
                    afisare(k);
                }
            else
                BK(k+1);
    }
}

int valid(int k)
{
    int i;
    for(i=0;i<k;i++)
    {
        if(v[i]==v[k])
            return 0;
    }
    return 1;
}

void afisare(int k)
{
    int i;
    for(i=1; i<=k; i++)
        cout<<v[i]<<" ";
    cout<<endl;
}

void BK_nerecursiv()
{
    int k = 0;
    for(int i=0; i<n; i++)
        x[i]=0;
    while(k > -1)
    {
        if (k==n)
        {
            afisare();
            k--;
        }
        else if(x[k]<n)
        {
            x[k]++;
            if (valid(k))
                k++;
        }
        else
        {
            x[k]=0;
            k--;
        }
    }
}


int main()
{
    n=5;
    BK(1);
    cout<<y;
}
