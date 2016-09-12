using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace WF
{

    class Program
    {
        static void Main(string[] args)
        {
            //MoneyActivity workflow1 = new MoneyActivity();
            VacateActivity workflow_vacate = new VacateActivity();
            WorkflowInvoker.Invoke(workflow_vacate);
            Console.ReadLine();
        }
    }
}
