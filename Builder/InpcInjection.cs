using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Builder
{
    public static class InpcInjection
    {
        public static void Inject(string assemblyFullName, string attrFullName, string methodName)
        {
            var isDirty = false;
            var replaceProperties = new List<PropertyDefinition>();

            var sourceAssembly = AssemblyDefinition.ReadAssembly(assemblyFullName);
            foreach (var type in sourceAssembly.MainModule.Types)
            {
                foreach (var prop in type.Properties)
                {
                    foreach (var attribute in prop.CustomAttributes)
                    {
                        if (attribute.AttributeType.FullName == attrFullName)
                        {
                            var MSILWorker = prop.SetMethod.Body.GetILProcessor();
                            MethodDefinition propertyChanged = null;

                            // Check the current type and all base types
                            var currType = type;
                            while (currType.Methods != null)
                            {
                                foreach (var method in currType.Methods)
                                {
                                    if (method.Name == methodName &&
                                        method.Parameters.Count == 1 &&
                                        method.Parameters[0].ParameterType.FullName == "System.String")
                                    {
                                        propertyChanged = method;
                                        break;
                                    }
                                }

                                if (propertyChanged != null || currType.BaseType == null)
                                    break;

                                currType = currType.BaseType.Resolve();
                            }
                            
                            if (propertyChanged == null)
                                continue;

                            if (replaceProperties.Any(p => p.DeclaringType.FullName == prop.DeclaringType.FullName && p.Name == prop.Name))
                            {
                                sourceAssembly.Write(assemblyFullName);
                                continue;
                            }

                            Instruction ldarg0 = MSILWorker.Create(OpCodes.Ldarg_0);
                            Instruction propertyName = MSILWorker.Create(OpCodes.Ldstr, prop.Name);
                            Instruction callRaisePropertyChanged = MSILWorker.Create(OpCodes.Call, propertyChanged);
                            MSILWorker.InsertBefore(prop.SetMethod.Body.Instructions[0], MSILWorker.Create(OpCodes.Nop));
                            MSILWorker.InsertBefore(prop.SetMethod.Body.Instructions[prop.SetMethod.Body.Instructions.Count - 1], ldarg0);
                            MSILWorker.InsertAfter(ldarg0, propertyName);
                            MSILWorker.InsertAfter(propertyName, callRaisePropertyChanged);
                            MSILWorker.InsertAfter(callRaisePropertyChanged, MSILWorker.Create(OpCodes.Nop));
                            
                            isDirty = true;
                        }
                    }
                }
            }

            if (isDirty)
            {
                sourceAssembly.Write(assemblyFullName);
            }
        }

        public static void Inject(string directory, string fileMask, string attrFullName, string methodName)
        {
            foreach (string assembly in Directory.GetFiles(directory, fileMask, SearchOption.AllDirectories))
            {
                // Skip self injection
                if (assembly.Contains("Builder.dll"))
                    continue;

                Inject(assembly, attrFullName, methodName);
            }
        }
    }
}
