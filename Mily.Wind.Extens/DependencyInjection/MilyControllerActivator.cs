using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.DependencyInjection
{
    public class MilyControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            //获得框架得服务提供对象,用于实列化控制器
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            //获取控制器类型
            Type type = context.ActionDescriptor.ControllerTypeInfo.AsType();
            //实列化控制器
            object Context = serviceProvider.GetService(type);
            //属性注入
            PropertyInjection(type, serviceProvider, Context);
            //方法注入
            MethodInjection(type, serviceProvider, Context);

            return Context;//把实列化的控制器返回
        }

        /// <summary>
        /// 属性注入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        public void PropertyInjection(Type type, IServiceProvider serviceProvider, object Context)
        {
            //根据自定义特性确定要属性注入的属性。
            //利用反射获取控制器中应用了PropertyInjectionAttribute的属性。
            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).Where(m => m.IsDefined(typeof(PropertyInjectionAttribute), true)))
            {
                Type PropertyType = propertyInfo.PropertyType;//获取当前属性的类型。注：GetType()获取的是当前对象的类型
                object property = serviceProvider.GetService(PropertyType);//利用框架的服务提供对象 创建对象

                PropertyInjection(PropertyType, serviceProvider, property);//如果有多层注入需要递归创建对象,把当前对象传入。
                MethodInjection(PropertyType, serviceProvider, property);//如果有多层注入需要递归创建对象,把当前对象传入。

                propertyInfo.SetValue(Context, property);//给属性赋值
            }
        }

        /// <summary>
        /// 方法注入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="Context"></param>
        public void MethodInjection(Type type, IServiceProvider serviceProvider, object Context)
        {
            //根据自定义特性确定要方法注入的方法。
            //利用反射获取控制器中应用了MethodInjectionAttribute特性的方法。
            foreach (MethodInfo memberInfo in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(m => m.IsDefined(typeof(MethodInjectionAttribute))))
            {
                ParameterInfo[] parameterInfos = memberInfo.GetParameters();//获取方法参数列表
                object[] Parameters = new object[parameterInfos.Length];
                //循环实列化方法参数
                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    Type ParameterType = parameterInfos[i].ParameterType;//获取当前方法参数的类型
                    object Parameter = serviceProvider.GetService(ParameterType);//利用框架的服务提供对象 创建对象


                    MethodInjection(ParameterType, serviceProvider, Parameter);//如果有多层注入需要递归创建对象,把当前对象传入。
                    PropertyInjection(ParameterType, serviceProvider, Parameter);//如果有多层注入需要递归创建对象,把当前对象传入。

                    Parameters[i] = Parameter;//添加到参数实列数组中去
                }
                memberInfo.Invoke(Context, Parameters);//执行方法
            }
        }

        public void Release(ControllerContext context, object controller)
        {
           
        }
    }
}
