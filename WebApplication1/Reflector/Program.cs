using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflector
{
    class Program
    {
        static Dictionary<string, int> map = new Dictionary<string, int>();
        static Dictionary<string, string> descriptionMap = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            // enum reflector demon
            EnumReflector();

            // dll reflector demon
            DllReflector();

        }


        private static void DllReflector()
        {
            string assembly = "BeReflector,Version=1.0.0.0,Culture=neutral,PublicKeyToken=d810d128ae130572";
            var assem = Assembly.Load(assembly);

            // type
            Type type = assem.GetType("BeReflector.Class1");

            // instance
            var instance = assem.CreateInstance("BeReflector.Class1");
            var instance1 = Activator.CreateInstance(type);

            // getMethod
            var result = type.GetMethod("TestMethod").Invoke(instance, null);

            
            // paramers
            Type[] param_types = new Type[1];
            param_types[0] = Type.GetType("System.Boolean");

            object[] objs = new object[1];
            objs[0] = false;

            var flag = type.GetMethod("TestMethod1", param_types).Invoke(instance1, objs);

        }


        private static void EnumReflector()
        {
            Type type = typeof(reflectorEnum);

            foreach (var field in type.GetFields())
            {
                if (!field.IsSpecialName)
                {
                    int value = 0;
                    if (int.TryParse(field.GetRawConstantValue().ToString(), out value))
                    {
                        map.Add(field.Name, value);
                    }

                    var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
                    if (attribute != null)
                    {
                        descriptionMap.Add(field.Name, ((DescriptionAttribute)attribute).Description);
                    }

                }
            }

        }

    }

    enum reflectorEnum
    {
        [Description("订单")]
        order = 1,

        [Description("产品")]
        product = 2,
    }

}
