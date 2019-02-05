using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Diagnostics.Runtime;

namespace ClrMD.Extensions.Core
{
    public static class ClrTypeExtensions
    {
        private static readonly Dictionary<string, Type> s_dumpToClrTypes;

        static ClrTypeExtensions()
        {
            s_dumpToClrTypes = new Dictionary<string, Type>();

            var basicTypes = new[]
            {
                typeof(object),
                typeof(string),
                typeof(void),
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(Guid),
                typeof(DateTime),
                typeof(TimeSpan),
                typeof(IPAddress),
                typeof(IPEndPoint),
                typeof(DnsEndPoint),
                typeof(X509Certificate),
                typeof(X509Certificate2)
            };

            foreach (var t in basicTypes)
            {
                s_dumpToClrTypes.Add(t.FullName, t);
            }
        }

        public static Type GetRealType(this ClrType type)
        {
            Type t;
            lock (s_dumpToClrTypes)
            {
                if (s_dumpToClrTypes.TryGetValue(type.Name, out t))
                    return t;
            }

            try
            {
                t = Type.GetType(type.Name);
            }
            catch
            {
                t = null;
            }

            lock (s_dumpToClrTypes)
            {
                s_dumpToClrTypes[type.Name] = t;
            }

            return t;
        }

        public static TypeCode GetTypeCode(this ClrType type)
        {
            var t = GetRealType(type);

            return t != null
                ? Type.GetTypeCode(t)
                : TypeCode.Object;
        }
    }
}