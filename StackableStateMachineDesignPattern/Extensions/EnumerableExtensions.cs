using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackableStateMachineDesignPattern.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> that, Action<T> action)
        {
            foreach (var item in that)
            {
                action(item);
            }
        }
    }
}
