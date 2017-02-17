using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcDbFrame.Singleton
{
    /// <summary>
    /// 这种模式调用起来是不是很方便呀
    /// </summary>
    public class SingletonDemoTest
    {
        public void Run()
        {
            SingletonDemo.Instance.SayHello();
        }
    }

    /// <summary>
    /// 单例模式泛型类-测试
    /// </summary>
    public class SingletonDemo
    {

        public static SingletonDemo Instance
        {
            get
            {
                return Singleton<SingletonDemo>.Instance;
            }
        }

        /// <summary>
        /// 你好独生子女
        /// </summary>
        public void SayHello()
        {
            Console.WriteLine("Hello Singleton");
        }
    }



}
