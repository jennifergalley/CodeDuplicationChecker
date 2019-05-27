using System;

public class Type4Tests
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

    public int DoubledSumFunctionWithInitSyntacticVariants()
    {
        int sum = 0, doubledSum = 0;

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

    public int DoubledSumFunctionWithSyntacticVariantsInLoopDefinition()
    {
        int sum = 0;
        int doubledSum = 0;

        //i++ => ++i
        for (int i = 0; i < 100; ++i)
        {
            sum = sum + i;
            doubledSum = sum * i;
        }

        doubledSum = doubledSum + 5;
        doubledSum = doubledSum + 1;
        doubledSum = sum * 2;
        return doubledSum;
    }
}
