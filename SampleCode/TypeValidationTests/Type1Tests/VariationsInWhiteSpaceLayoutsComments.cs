using System;

public class Type1Tests
{
	public int OriginalDoubledSumFunction()
	{
        int sum = 0;

        for(int i = 0; i < 100; i++)
        {
            sum = sum + i;
        }

        int doubledSum = sum * 2;
        return doubledSum;
	}

    public int DoubledSumFunctionWithWhitespace()
    {
        int sum = 0;





        for (int i = 0; i < 100; i++)
        {
            sum =       sum + i;
        }




        int doubledSum = sum * 2;
        return doubledSum;
    }

    public int DoubledSumFunctionWithComments()
    {
        int sum = 0;

        // This is a for loop to calculate sum
        for (int i = 0; i < 100; i++)
        {
            sum = sum + i;
        }

        // Sum is being doubled here
        int doubledSum = sum * 2;
        return doubledSum;
    }

    public int DoubledSumFunctionWithLayoutChange()
    {
        int sum = 0;
        int doubledSum = sum * 2;

        for (int i = 0; i < 100; i++)
        {
            sum = sum + i;
        }

        return doubledSum;
    }
}
