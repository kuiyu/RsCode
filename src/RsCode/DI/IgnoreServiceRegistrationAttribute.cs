
using System;
using Microsoft.Extensions.DependencyInjection;

namespace RsCode
{
    /// <summary>
    /// The services containing this <c>Attribute</c> will be ignored during service registration in <see cref="IServiceCollection"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public class IgnoreServiceRegistrationAttribute : Attribute
    {
    }
}
