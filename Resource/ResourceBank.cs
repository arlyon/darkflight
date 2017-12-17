using System;
using System.Collections.Generic;
using System.Linq;

namespace Resource
{
    public class ResourceBank : Dictionary<Resource, long>
    {
        /// <summary>
        /// Subtracts the second resource from the first.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ResourceBank operator- (ResourceBank a, ResourceBank b)
        {
            var output = a.ToResourceBank(p => p.Key, p => p.Value);
            
            foreach (var pair in b)
            {
                if (output.ContainsKey(pair.Key))
                {
                    output[pair.Key] -= pair.Value;
                }
                else
                {
                    output[pair.Key] = -pair.Value;
                }
            }

            return output;
        }
        
        /// <summary>
        /// Returns the negative resource bank to what was passed in.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static ResourceBank operator- (ResourceBank a)
        {
            return a.Select(pair => new KeyValuePair<Resource, long>(pair.Key, -pair.Value)).ToResourceBank();
        }
        
        /// <summary>
        /// Adds the second resource bank to the first.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ResourceBank operator+ (ResourceBank a, ResourceBank b)
        {
            return a.Concat(b)
                .GroupBy(pair => pair.Key)
                .ToResourceBank(pair => pair.Key, group => group.Count() == 1 ? group.First().Value : group.First().Value + group.Last().Value);
        }
        
        /// <summary>
        /// Resource bank A has more resources than B if it has more resources
        /// for ALL resources in B.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator>= (ResourceBank a, ResourceBank b)
        {
            return (a - b).All(pair => pair.Value >= 0);
        }
        
        /// <summary>
        /// Resource bank A is has fewer resources than B if B has
        /// more resources of ANY type.
        /// </summary>
        /// <remarks>
        /// If any resources as a result of A - B are less than 0,
        /// then a is less than b. The propery is non-commutative.
        /// A can be less than B and B can be less than A simultaneously.
        /// </remarks>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator<= (ResourceBank a, ResourceBank b)
        {
            return (a - b).Any(pair => pair.Value <= 0);
        }
        
        /// <summary>
        /// Divides all the resources in the bank by a scalar.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="mult"></param>
        /// <returns></returns>
        public static ResourceBank operator /(ResourceBank a, float mult)
        {
            return a.ToResourceBank(pair => pair.Key, pair => (long) (pair.Value / mult));
        }

        /// <summary>
        /// Divides all the resources in the bank by a scalar.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ResourceBank operator /(ResourceBank a, ResourceBank b)
        {
            return a.ToResourceBank(pair => pair.Key, pair => pair.Value / b[pair.Key] ); // TODO divide by zero
        }
        
        /// <summary>
        /// Multiplies all the resources in the bank with a scalar.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="mult"></param>
        /// <returns></returns>
        public static ResourceBank operator *(ResourceBank a, float mult)
        {
            return a.ToResourceBank(pair => pair.Key, pair => (long) (pair.Value * mult));
        }
        
        /// <summary>
        /// Multiplies all the resources in the bank by a scalar.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ResourceBank operator *(ResourceBank a, ResourceBank b)
        {
            return a.ToResourceBank(pair => pair.Key, pair => pair.Value * b[pair.Key] );
        }

        public static ResourceBank Empty
        {
            get
            {
                return new ResourceBank();
            }
        }

        public override string ToString()
        {
            return Count > 0 ? this.Aggregate("ResourceBank \n", (current, pair) => current + ("\t" + pair.Key.Name + ": " + pair.Value)) : "Bank Empty";
        }

        public ResourceBank GetEmptyCopy()
        {
            return this.ToResourceBank(pair => pair.Key, pair => (long) 0);
        }

        public ResourceBank GetCopy()
        {
            return this.ToResourceBank();
        }
    }

    public static class LinqExtension
    {
        public static ResourceBank ToResourceBank<TSource>(this IEnumerable<TSource> source, Func<TSource, global::Resource.Resource> keyFunc, Func<TSource, long> valFunc)
        {
            var bank = ResourceBank.Empty;
            
            foreach (var pair in source)
            {
                bank.Add(keyFunc(pair), valFunc(pair));
            }

            return bank;
        }

        public static ResourceBank ToResourceBank(this IEnumerable<KeyValuePair<Resource, long>> source)
        {
            var bank = ResourceBank.Empty;
            foreach (var pair in source)
            {
                bank.Add(pair.Key, pair.Value);
            }
            return bank;
        }
    }
}