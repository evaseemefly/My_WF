using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace t10_WF2
{

    class Program
    {
        static void Main(string[] args)
        {
            //Activity workflow1 = new BaoxiaoFlowChart();

            Activity workflow1=new BaoxiaoStateMachine();

            WorkflowInvoker.Invoke(workflow1);

            Console.ReadKey();
        }
    }
}
