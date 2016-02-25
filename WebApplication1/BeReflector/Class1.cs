using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BeReflector
{
    public class Class1
    {

        public string Name { get; set; }


        public bool TestMethod()
        {
            return true;
        }

        public bool TestMethod1(bool flag)
        {
            return flag;
        }
    }
}
