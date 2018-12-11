using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkingWithIoCContainer.Enum;
using WorkingWithIoCContainer.Interface;

namespace WorkingWithIoCContainer
{
    class Container : IContainer
    {
        Dictionary<Type, RegistrationModel> _instanceRegistry = new Dictionary<Type, RegistrationModel>();
        public void RegisterInstanceType<TInterface, TClass>() where TInterface : class where TClass : class
        {
            RegisterType<TInterface, TClass>(RegType.Instance);
        }

        private void RegisterType<TInterface, TClass>(RegType instance) where TInterface : class where TClass : class
        {
            if (_instanceRegistry.ContainsKey(typeof(TInterface)))
            {
                _instanceRegistry.Remove(typeof(TInterface));
            }
            else
            {
                
            }

            _instanceRegistry.Add(
                typeof(TInterface),
                new RegistrationModel(
                    instance,
                    typeof(TClass)
                    )
                );
        }

        public void RegisterWithSingletonType<TInterface, TClass>() where TInterface : class where TClass : class
        {
            RegisterType<TInterface, TClass>(RegType.Singleton);
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        private object Resolve(Type type)
        {
            Object obj = null;
            if (_instanceRegistry.ContainsKey(type))
            {
                RegistrationModel model = _instanceRegistry[type];
                if (model!=null)
                {
                    Type typeToCreate = model.ObjectType;
                    ConstructorInfo[] constructorInfo = typeToCreate.GetConstructors();
                    var dependentCtor = constructorInfo.FirstOrDefault(
                        item=>item.CustomAttributes.FirstOrDefault(
                            att=> att.AttributeType == typeof(MyTestingAttribute)) !=null
                            );

                    if (dependentCtor == null)
                    {
                        obj = CreateInstance(model);
                    }
                    else
                    {
                        ParameterInfo[] parameterInfos = dependentCtor.GetParameters();

                        if (parameterInfos.Length == 0)
                        {
                            obj = CreateInstance(model);
                        }
                        else
                        {
                            List<object> arguments = new List<object>();
                            foreach (var parameterInfo in parameterInfos)
                            {
                                Type typeHere = parameterInfo.ParameterType;
                                arguments.Add(Resolve(typeHere));
                            }

                            obj = CreateInstance(model, arguments.ToArray());
                        }
                    }
                }
            }

            return obj;
        }

        private object CreateInstance(RegistrationModel model, object[] arguments = null)
        {
            object returnedObject = null;
            Type typeToCreate = model.ObjectType;

            if (model.RType == RegType.Instance)
            {
                returnedObject = InstanceCreationService.GetInstance().GetNewObject(typeToCreate, arguments);
            }
            else if (model.RType == RegType.Singleton)
            {
                returnedObject = SingeltonCreationService.GetInstance().GetSingleton(typeToCreate, arguments);
            }

            return returnedObject;
        }
    }
}
