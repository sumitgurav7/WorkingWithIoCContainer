using System;

namespace WorkingWithIoCContainer
{
    internal class InstanceCreationService
    {
        private static InstanceCreationService _instance;

        static InstanceCreationService()
        {
            _instance = new InstanceCreationService();
        }

        private InstanceCreationService()
        {
            
        }
        internal static InstanceCreationService GetInstance()
        {
            return _instance;
        }

        public object GetNewObject(Type typeToCreate, object[] arguments = null)
        {
            object objectToCreate;
            try
            {
                objectToCreate = Activator.CreateInstance(typeToCreate, arguments);
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