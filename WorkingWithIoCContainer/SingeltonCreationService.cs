using System;
using System.Collections.Generic;

namespace WorkingWithIoCContainer
{
    internal class SingeltonCreationService
    {
        private static SingeltonCreationService _instance;
        static Dictionary<string,object> _objectPool = new Dictionary<string, object>();

        static SingeltonCreationService()
        {
            _instance = new SingeltonCreationService();
        }

        private SingeltonCreationService() { }
        internal static SingeltonCreationService GetInstance()
        {
            return _instance;
        }

        public object GetSingleton(Type typeToCreate, object[] arguments)
        {
            object objectToCreate;

            try
            {
                if (_objectPool.ContainsKey(typeToCreate.Name))
                {
                    objectToCreate = _objectPool[typeToCreate.Name];
                }
                else
                {
                    objectToCreate = InstanceCreationService.GetInstance().GetNewObject(typeToCreate, arguments);
                    _objectPool.Add(typeToCreate.Name,objectToCreate);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return objectToCreate;
        }
    }
}