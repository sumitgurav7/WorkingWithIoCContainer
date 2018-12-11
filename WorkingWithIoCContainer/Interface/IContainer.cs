namespace WorkingWithIoCContainer.Interface
{
    interface IContainer
    {
        void RegisterInstanceType<TInterface, TClass>()
            where TInterface : class
            where TClass : class;

        void RegisterWithSingletonType<TInterface, TClass>()
            where TInterface : class
            where TClass : class;

        T Resolve<T>();
    }
}
