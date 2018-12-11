using System;
using WorkingWithIoCContainer.Enum;

namespace WorkingWithIoCContainer
{
    internal class RegistrationModel
    {
        public RegistrationModel(RegType rType, Type objectType)
        {
            RType = rType;
            ObjectType = objectType ?? throw new ArgumentNullException(nameof(objectType));
        }

        internal Type ObjectType { get; set; }
        internal RegType RType { get; set; }
    }
}
