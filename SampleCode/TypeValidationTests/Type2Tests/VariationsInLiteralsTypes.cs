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

    public int DoubledSumFunctionWithVariableRenaming()
    {
        int summm = 0;

        for (int j = 0; j < 100; j++)
        {
            summm = summm + j;
        }

        int doubledSummm = summm * 2;
        return doubledSummm;
    }

    public int DoubledSumFunctionWithDifferentLiterals()
    {
        int sum = 5;

        for (int i = 2; i < 200; i++)
        {
            sum = sum + i;
        }

        int doubledSum = sum * 5;
        return doubledSum;
    }
}
