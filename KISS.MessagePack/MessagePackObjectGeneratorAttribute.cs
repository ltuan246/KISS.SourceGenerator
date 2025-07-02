using System;

namespace KISS.MessagePack
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MessagePackObjectGeneratorAttribute : Attribute
    {
    }
}
