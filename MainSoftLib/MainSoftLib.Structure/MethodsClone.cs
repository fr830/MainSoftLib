using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Structure
{
    public class MethodsClone
    {
        public T CloneStructure<T>(T Structure) where T : new()
        {
            T StructureClone = new T();

            Type fromType = Structure.GetType();
            Type toType = StructureClone.GetType();

            PropertyInfo[] props = fromType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                PropertyInfo outputProp = toType.GetProperty(prop.Name);

                if (outputProp != null && outputProp.CanWrite)
                {
                    string propertyTypeFullName = prop.PropertyType.FullName;

                    object value = prop.GetValue(Structure, null);
                    outputProp.SetValue(StructureClone, value, null);
                }
            }

            return StructureClone;
        }
    }
}
