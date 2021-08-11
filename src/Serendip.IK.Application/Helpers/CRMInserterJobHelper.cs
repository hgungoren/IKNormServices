using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Serendip.IK.Helpers
{
    //TODO : Custom Map için değiştirilip IInserter'lar içinde kullanılacak.
    public class CRMInserterJobHelper
    {
        public static Result<T> InsertTData<T>(Dictionary<string,string> Data)where T:new()
        {
            var result =  new Result<T>();

            T obj = Activator.CreateInstance<T>();

            if (obj == null || Data == null)
            {
                result.ErrorMessage= "EntityName or Data Null";
                return result;
            }

            Type type = typeof(T);
            var entity = obj;
            var propInfos = entity.GetType().GetProperties();

            foreach (var prop in propInfos)
            {
                try
                {
                    var propName = prop.Name;
                    var propType = prop.PropertyType;

                        var getValue = Data.FirstOrDefault(x=>x.Key == propName).Value;
                        if (getValue != null && propType.IsSerializable)
                        {
                            PropertyInfo instance = type.GetProperty(propName);
                            if (instance == null)
                            {
                                result.Item = entity;
                                result.ErrorMessage= "instance Null";
                                return result;
                            }
                            if(instance.CanWrite)
                                {
                                if (instance.PropertyType.Name == "Guid")
                                {
                                    instance.SetValue(entity, long.Parse(getValue)); 
                                }
                                else if (propType.IsGenericType && propType.GetGenericTypeDefinition()== typeof(Nullable<>))
                                {
                                    if (propType.GetGenericArguments()[0].Name == "Guid")
                                    {
                                        Guid gid = Guid.Empty;
                                        Guid? nullableGuid = null;
                                        if (Guid.TryParse(getValue, out gid))
                                        {
                                            nullableGuid = gid;
                                        }
                                        instance.SetValue(entity,nullableGuid); 
                                    }
                                }                                               
                                else
                                {
                                    instance.SetValue(entity,Convert.ChangeType(getValue, instance.PropertyType)); 
                                }
                            }
                        }                                
                    
                }
                catch (System.Exception ex)
                {
                    result.Success = false;
                    result.Item = entity;
                    result.ErrorMessage= ex.Message;
                    return result;
                }                
            }
            result.Success = true;
            result.Item = entity;
            result.ErrorMessage ="";
            return result;
            }

        public class Result<T> where T : new()
        {
            public bool Success { get; set; }
            public T Item { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
