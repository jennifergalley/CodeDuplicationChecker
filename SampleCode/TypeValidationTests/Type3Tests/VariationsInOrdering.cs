using System;

public class Type3Tests
{
	public int OriginalDoubledSumFunction()
	{
        int sum = 0;
        int doubledSum = 0;

        for(int i = 0; i < 100; i++)
        {
            sum = sum + i;
            doubledSum = sum * i;
        }

        doubledSum = doubledSum + 5;
        doubledSum = doubledSum + 1;
        doubledSum = sum * 2;
        return doubledSum;
	}

    public int DoubledSumFunctionWithReorderingOutside()
    {
        int doubledSum = 0;
        int sum = 0;

        for (int i = 0; i < 100; i++)
        {
            sum = sum + i;
            doubledSum = sum * i;
        }

        doubledSum = doubledSum + 1;
        doubledSum = doubledSum + 5;
        doubledSum = sum * 2;
        return doubledSum;
    }

    public int DoubledSumFunctionWithReorderingInsideLoop()
    {
        int sum = 0;
        int doubledSum = 0;

        for (int i = 0; i < 100; i++)
        {
            doubledSum = sum * i;
            sum = sum + i;
        }

        doubledSum = doubledSum + 5;
        doubledSum = doubledSum + 1;
        doubledSum = sum * 2;
        return doubledSum;
    }

    public int DoubledSumFunctionWithLineAdditions()
    {
        int sum = 0;
        int doubledSum = 0;
        //Line added here
        sum = sum + 5;

        for (int i = 0; i < 100; i++)
        {
            sum = sum + i;
            doubledSum = sum * i;
        }

        doubledSum = doubledSum + 5;
        doubledSum = doubledSum + 1;
        doubledSum = sum * 2;
        return doubledSum;
    }

    public int DoubledSumFunctionWithLineSubtractions()
    {
        int sum = 0;
        int doubledSum = 0;

        for (int i = 0; i < 100; i++)
        {
            sum = sum + i;
            doubledSum = sum * i;
        }

        // Line removed
        //doubledSum = doubledSum + 5;
        doubledSum = doubledSum + 1;
        doubledSum = sum * 2;
        return doubledSum;
    }
}
