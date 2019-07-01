using System;
using System.Text;

namespace CodeGeneratorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var strCode = GetMapCode<TestClass>();
            Console.WriteLine("代码生成成功");
            Console.ReadKey();
        }

        /// <summary>
        /// Map代码生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetMapCode<T>()
        {
            var type = typeof(T);
            StringBuilder strBuilder = new StringBuilder();
            foreach (var pro in type.GetProperties())
            {
                var proName = pro.Name;
                var proType = pro.PropertyType;
                Console.WriteLine(pro.Name + "---" + proType);
                if (proName == "Id")
                {
                    strBuilder.AppendLine($"builder.HasKey(x => x.Id);");
                    strBuilder.AppendLine($"builder.Property(x => x.Id).HasMaxLength(36);");
                }
                else if (proType == typeof(object))
                {
                    strBuilder.AppendLine($"builder.Ignore(x => x.{proName});");
                }
                else
                {
                    strBuilder.AppendLine($"builder.Property(x => x.{proName});");
                }
            }
            var str = strBuilder.ToString();
            return str;
        }
    }

    public class TestClass
    {

        public object obj { get; set; }
        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 参加次数
        /// </summary>
        public int UseCount { get; set; } = 0;

        /// <summary>
        /// 购买商品数量
        /// </summary>
        public int BuyGoodsCount { get; set; } = 0;

        /// <summary>
        /// 优惠总金额
        /// </summary>
        public decimal DiscountTotalCount { get; set; } = 0;
    }
}
