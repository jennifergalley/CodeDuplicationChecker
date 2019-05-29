namespace CodeDuplicationCheckerTests.DemoTests
{
    /// <summary>
    /// Examples for demo
    /// This examples are adapted from:
    /// Comparison and Evaluation of Code Clone Detection Techniques and Tools: A Qualitative Approach
    /// By:Chanchal K.Roy∗,a, James R.Cordya, Rainer Koschkeb
    /// http://research.cs.queensu.ca/home/cordy/Papers/RCK_SCP_Clones.pdf
    /// According the paper, there are 4 type of clone and each 4 have four sub category. There are 16 examples in this class
    /// to cover all the scenarios.
    /// </summary>
    public class DuplicateMethods
    {
        public double SumProduct_Original(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    prod = prod * i;
                    Foo(sum, prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_I_ExactMatch(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    prod = prod * i;
                    Foo(sum, prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_I_CommentsAdded(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;//More comments are added here
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    prod = prod * i;
                    Foo(sum, prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_I_ChangeInSpacing(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)       {
                for (int j = 0; j <= n; j++)    {
                    sum = sum + i;
                    prod = prod * i;
                    Foo(sum, prod);              }
            }

            return sum;
        }

        public double SumProduct_Type_II_VariableRename(int n)
        {
            double s = 0.0;//Original comments
            double p = 1.0;
            for (int k = 1; k <= n; k++)
            {
                for (int j = 0; j <= n; j++)
                {
                    s = s + k;
                    p = p * k;
                    Foo(s, p);
                }
            }

            return s;
        }

        public double SumProduct_Type_II_RenameAndMethodCallSwapped(int n)
        {
            double s = 0.0;//Original comments
            double p = 1.0;
            for (int k = 1; k <= n; k++)
            {
                for (int j = 0; j <= n; j++)
                {
                    s = s + k;
                    p = p * k;
                    Foo(p, s);
                }
            }

            return s;
        }

        public double SumProduct_Type_II_DataTypeChanged(int n)
        {
            int sum = 0;//Original comments
            int prod = 1;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    prod = prod * i;
                    Foo(sum, prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_II_ExpressionAdded(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + (i * i);
                    prod = prod * (i * i);
                    Foo(sum, prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_III_InvokeWithMoreParameter(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    prod = prod * i;
                    Foo(sum, prod, n);
                }
            }

            return sum;
        }

        public double SumProduct_Type_III_InvokeWithLessParameter(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    prod = prod * i;
                    Foo(prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_III_MethodCallInIfStatementInserted(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    prod = prod * i;
                    if (n % 2 == 0)
                    {
                        Foo(prod);
                    }
                }
            }

            return sum;
        }

        public double SumProduct_Type_III_LineDeleted(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                }
            }

            return sum;
        }

        public double SumProduct_Type_III_StatementInIfInserted(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (n % 2 == 0)
                    {
                        sum = sum + i;
                    }
                    prod = prod * i;
                    Foo(prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_IV_VariableDeclarationReOrdered(int n)
        {
            double prod = 1.0;
            double sum = 0.0;//Original comments
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    prod = prod * i;
                    Foo(sum, prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_IV_StatementReOrdered(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    prod = prod * i;
                    sum = sum + i;
                    Foo(sum, prod);
                }
            }

            return sum;
        }

        public double SumProduct_Type_IV_MoreStatementReOrdered(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    Foo(sum, prod);
                    prod = prod * i;
                }
            }

            return sum;
        }
        
        public double SumProduct_Type_IV_ForToWhileChange(int n)
        {
            double sum = 0.0;//Original comments
            double prod = 1.0;
            int i = 0;
            while (i <= n)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum = sum + i;
                    Foo(sum, prod);
                    prod = prod * i;
                }

                i++;
            }

            return sum;
        }

        private void Foo(double sum = 0.0, double prod = 0.0, int n = 0)
        {

        }
    }
}
