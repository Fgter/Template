using QFramework;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Define;
using System;
using System.Reflection;
using UnityEngine;

namespace Models
{
    class DefineModel : AbstractModel
    {
        public const string DataPath = "Data/Data/";
        protected override void OnInit()
        {
            Load();
        }
        public Dictionary<Type, dynamic> allDefines = new Dictionary<Type, dynamic>();
        public Dictionary<int, IIconItemDefine> iconItemDefines = new Dictionary<int, IIconItemDefine>();

        Dictionary<int, ShopDefine> ShopDefines = new Dictionary<int, ShopDefine>();
        Dictionary<int, Dictionary<int, ShopItemDefine>> ShopItemDefines = new Dictionary<int, Dictionary<int, ShopItemDefine>>();

        void Load()
        {
            string json = File.ReadAllText(DataPath + "ShopDefine.txt");
            ShopDefines = JsonConvert.DeserializeObject<Dictionary<int, ShopDefine>>(json);

            json = File.ReadAllText(DataPath + "ShopItemDefine.txt");
            ShopItemDefines = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, ShopItemDefine>>>(json);

            CollectDefines();
        }

        void CollectDefines()  //性能:1000多个物品耗时0.00969s(抽象方法前)  0.0104661(抽象方法后)
        {
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                var dictInstance = field.GetValue(this);//字典实例
                var dictType = field.FieldType;//字典类型
                PropertyInfo dicProp = field.FieldType.GetProperty("Values", BindingFlags.Instance | BindingFlags.Public);
                Type keyValueType = dicProp.PropertyType;
                Type valueType = keyValueType.GetGenericArguments()[1];//value的类型
                var convertDict = Convert.ChangeType(dictInstance, dictType);//转换类型后的字典
                allDefines.Add(valueType, convertDict);///////////////////////////////////////添加allDefines字典
                #region
                //////////添加iconItemDefines字典
                //if (Array.Exists(valueType.GetInterfaces(), i => i == typeof(IIconItemDefine)))
                //{
                //    MethodInfo getEnumeratorMethod = dictType.GetMethod("GetEnumerator");
                //    var enumerator = getEnumeratorMethod.Invoke(dictInstance, null) as System.Collections.IEnumerator;
                //    while (enumerator.MoveNext())
                //    {
                //        object current = enumerator.Current;//可以改为dynamic但是需要.net4.0

                //        int key = (int)current.GetType().GetProperty("Key").GetValue(current);
                //        IIconItemDefine value = current.GetType().GetProperty("Value").GetValue(current) as IIconItemDefine;
                //        if (iconItemDefines.ContainsKey(key))
                //            Debug.LogError(string.Format("itmId冲突:Name:{0} Id:{1} Type:{2}----Name:{3} Id:{4} Type:{5}",
                //                            iconItemDefines[key].Name, key, iconItemDefines[key].GetType(), value.Name, key, value.GetType()));
                //        iconItemDefines.Add(key, value);
                //    }
                //}
                #endregion
                //////////添加iconItemDefines字典
                CollectFlagDefines<int, IIconItemDefine>(iconItemDefines, valueType, dictType, dictInstance);
            }
        }


        #region CollectFlagDefines(Dictionary<T1,T2> storageDict,Type valueType,Type dictType,object dictInstance)
        /// <summary>
        /// 将存储的类实现了对应的接口的字典收集并放到一个字典里
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="storageDict">收集后存放的字典</param>
        /// <param name="valueType">将要收集的字典value的类型</param>
        /// <param name="dictType">将要收集的字典的类型</param>
        /// <param name="dictInstance">将要收集的字典的实例</param>
        void CollectFlagDefines<T1,T2>(Dictionary<T1,T2> storageDict,Type valueType,Type dictType,object dictInstance) where T2:class,IDefine
        {

            if (Array.Exists(valueType.GetInterfaces(), i => i == typeof(IIconItemDefine)))
            {
                MethodInfo getEnumeratorMethod = dictType.GetMethod("GetEnumerator");
                var enumerator = getEnumeratorMethod.Invoke(dictInstance, null) as System.Collections.IEnumerator;
                while (enumerator.MoveNext())
                {
                    object current = enumerator.Current;//可以改为dynamic但是需要.net4.0

                    T1 key = (T1)current.GetType().GetProperty("Key").GetValue(current);
                    T2 value = current.GetType().GetProperty("Value").GetValue(current) as T2;
                    if (storageDict.ContainsKey(key))
                        Debug.LogError(string.Format("itmId冲突: Id:{0} Type:{1}----Id:{2} Type:{3}",
                                         key, storageDict[key].GetType(), key, value.GetType()));
                    storageDict.Add(key, value);
                }
            }
        }
    }
    #endregion
}
