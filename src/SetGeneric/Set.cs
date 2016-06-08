using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetGeneric
{
    public class Set<T> : IEnumerable<T>, IEquatable<Set<T>>
    {
        private int initialArrayCapacity = 2;
        private int current = 0;
        private T[] set;

        public Set()
        {
            set = new T[initialArrayCapacity];
        }

        public Set(params T[] a)
        {
            if (a == null)
                throw new ArgumentNullException();
            set = new T[a.Length];
            Add(a);
        }
        public int Count
        {
            get
            {
                return current;
            }
        }
        public void Add(T temp)
        {
            if (Contains(temp))
            {
                return;
            }
            if (current == set.Length - 1)
            {
                NewDimensionOfArray();
            }
            set[current] = temp;
            current++;
        }
        public void Add(T[] temp)
        {
            foreach (var a in temp)
            {
                Add(a);
            }
        }
        public bool Contains(T item)
        {
            if (item == null)
                return false;
            for (int i = 0; i < current; i++)
                if (item.Equals(set[i]) == true)
                    return true;
            return false;
        }

        private void NewDimensionOfArray()
        {
            T[] newArray = new T[set.Length * 2];
            for (int i = 0; i < set.Length; i++)
            {
                newArray[i] = set[i];
            }
            set = newArray;
        }
        public bool IsEmpty()
        {
            return (current == 0);
        }

        public Set<T> Union(Set<T> temp)
        {
            Set<T> result = new Set<T>(set);

            foreach (T item in temp.set)
            {
                if (!Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public Set<T> Intersection(Set<T> temp)
        {
            Set<T> result = new Set<T>();

            foreach (T item in set)
            {
                if (temp.set.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
        public Set<T> Difference(Set<T> temp)
        {
            Set<T> result = new Set<T>();

            foreach (T item in set)
            {
                if (temp.set.Contains(item) == false)
                    result.Add(item);
            }
            return result;
        }
        public Set<T> SymmetricDifference(Set<T> temp)
        {
            Set<T> union = Union(temp);
            Set<T> intersection = Intersection(temp);

            return union.Difference(intersection);

        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < current; i++)
                yield return set[i];
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool Equals(Set<T> p)
        {
            if (p == null)
                return false;
            if (p.Count != this.Count)
                return false;
            foreach (var a in p)
                if (this.Contains(a) == false)
                    return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Set<T>))
                return false;
            return base.Equals((Set<T>)obj);
        }

        public override int GetHashCode()
        {
            return Count * GetType().ToString().Length + GetHashCode();
        }
        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            if (current == 0)
                result.Append("Set is empty");
            foreach (var a in this)
                result.AppendFormat("{0} ", a);
            return string.Format("{0}", result);
        }
    }
}
