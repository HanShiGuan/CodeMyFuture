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
            //EnumReflector();
            string assembly = "Version=1.0.0.0,culture=zh-CN,PublicKeyToken=47887f89771bc57f";



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

                    var attribute = Attribute.GetCustomAttribute(field,typeof(DescriptionAttribute), false);
                    if(attribute != null)
                    {
                        descriptionMap.Add(field.Name, ((DescriptionAttribute)attribute).Description);
                    }
                    
                }
            }
        }
        private static string GetDescription(FieldInfo field)
        {
            var att = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return att == null ? field.Name : ((DescriptionAttribute)att).Description;
        }
        //EF:就是让EF上下文保存了一下。不适合于集群(后期使用key，value保存在分布式缓存中，key为guid)
        //public int SaveChanges()
        //{
        //    //return DbContextFactory.GetCurrentDbContext().SaveChanges();


        //    string strAssembly = ConfigurationManager.AppSettings["DalAssembly"];
        //    string strDbContextFactoryclassFulleName = ConfigurationManager.AppSettings["DbContextFactoryclassFulleName"];


        //    Assembly assembly = Assembly.Load(strAssembly);
        //    Type type = assembly.GetType(strDbContextFactoryclassFulleName);
        //    MethodInfo methodInfo = type.GetMethod("GetCurrentDbContext");
        //    return ((DbContext)methodInfo.Invoke(null, null)).SaveChanges();
        //}
    }

    enum reflectorEnum
    {
        [Description("订单")]
        order = 1,

        [Description("产品")]
        product = 2,
    }

}
