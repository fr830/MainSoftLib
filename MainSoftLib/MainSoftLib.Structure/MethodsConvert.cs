using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Structure
{
    public class MethodsConverter<TInput, TOutput> where TOutput : new()
    {
        public TOutput Convert(TInput entity)
        {
            try
            {
                if (entity is Enum)
                    throw new NotImplementedException("Entity is an enumeration - Use ConvertNum!");

                TOutput output = new TOutput();

                Type fromType = entity.GetType();
                Type toType = output.GetType();

                PropertyInfo[] props = fromType.GetProperties();

                PropertyInfo[] props2 = toType.GetProperties();

                foreach (PropertyInfo prop in props)
                {
                    PropertyInfo outputProp = toType.GetProperty(prop.Name);

                    if (outputProp != null && outputProp.CanWrite)
                    {
                        string propertyTypeFullName = prop.PropertyType.FullName;

                        object value = prop.GetValue(entity, null);
                        outputProp.SetValue(output, value, null);
                    }
                }

                return output;
            }
            catch (Exception ex)
            {
                return default(TOutput);
            }
        }

        public TOutput[] Convert(TInput[] entity)
        {
            try
            {
                if (entity is Enum)
                    throw new NotImplementedException("Entity is an enumeration - Use ConvertNum!");

                var output = entity.Select(Convert).ToArray();

                return output;
            }
            catch (Exception)
            {
                return default(TOutput[]);
            }
        }

        public List<TOutput> Convert(List<TInput> entity)
        {
            try
            {
                if (entity is Enum)
                    throw new NotImplementedException("Entity is an enumeration - Use ConvertNum!");

                var output = entity.Select(Convert).ToList();

                return output;
            }
            catch (Exception)
            {
                return default(List<TOutput>);
            }
        }
    }
}
