using System;
using System.Collections.Generic;

namespace Dedup
{
    class DupClass
    {
        public static int DupMethod1(int x, int y)
        {
            var m = 66;
            var total = 0;
            object xm = null;
            Employee employee1 = new Employee();
            employee1.FirstName = "A";
            employee1.FirstName = employee1.FirstName + "B";
            employee1.LastName = "Z";
            employee1.Salary = 53635;
            employee1.Salary = employee1.Salary + 353;
            employee1.Salary += x + y;
            employee1.Salary *= x + y;
            var max = Math.Max(x, y);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    total = i + j;
                    var xf = "";
                    xf = "xf" + 10;
                }

                i = i + 4;
            }

            for (int i = 10; i < 10; i++)
            {
                for (int j = 10; j < 100; j++)
                {
                    total = i + j;
                }
                i = i + 4;
                i = i + 4;
                i = i + 4;
            }

            var l = new List<string>();
            foreach (var v in l)
            {
                
            }

            return total;
        }
        
        public static int DupMethod2(int x, int y)
        {
            var total = 0;
            object xm = null;
            Employee employee1 = new Employee();
            employee1.FirstName = "A";
            employee1.FirstName = employee1.FirstName + "B";
            employee1.LastName = "Z";
            employee1.Salary = 53635;
            employee1.Salary = employee1.Salary + 353;
            employee1.Salary += x + y;
            employee1.Salary *= x + y;
            var m = 66;
            var max = Math.Max(x, y);
            for (int k = 0; k < 10; k++)
            {
                for (int j = 0; j < 10; j++)
                {
                    total = k + j;
                    var xf = "";
                    xf = "xf" + 10;
                }

                k = k + 4;
            }

            var l = new List<string>();
            for (int i = 10; i < 10; i++)
            {
                for (int j = 10; j < 100; j++)
                {
                    total = i + j;
                }
                i = i + 4;
                i = i + 4;
                i = i + 4;
            }

         
            foreach (var v in l)
            {

            }

            return total;
        }

    }

    public class Employee
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Salary { get; set; }

        public Company Company { get; set; }
    }

    public class Company
    {
        public string Name { get; set; }
    }
}
