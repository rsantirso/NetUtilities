﻿using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// This class is a handy wrapper of <seealso cref="System.Text.StringBuilder"/> class for string manipulation with minimal cost.
    /// </summary>
    public class MutableString : 
        IEnumerable, IEnumerable<char>, 
        ICollection, ICollection<char>,
        IComparable, IComparable<string>, IComparable<MutableString>, 
        IEquatable<string>, IEquatable<MutableString>
    {
        private readonly StringBuilder _builder;
        public int Length
        {
            get => _builder.Length;
            set => _builder.Length = value;
        }

        public int Capacity
        {
            get => _builder.Capacity;
            set => _builder.Capacity = value;
        }

        public int MaxCapacity => _builder.MaxCapacity;

        int ICollection<char>.Count => Length;

        bool ICollection<char>.IsReadOnly => false;

        int ICollection.Count => Length;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        public MutableString()
            => _builder = new StringBuilder();

        public MutableString(string value)
            => _builder = new StringBuilder(value);

        public MutableString(int capacity, int maxCapacity)
            => _builder = new StringBuilder(capacity, maxCapacity);

        public MutableString(int capacity)
            => _builder = new StringBuilder(capacity);

        public MutableString(string value, int capacity)
            => _builder = new StringBuilder(value, capacity);

        public MutableString(StringBuilder builder)
            => _builder = builder;

        public MutableString(string value, int startIndex, int capacity, int length)
            => _builder = new StringBuilder(value, startIndex, length, capacity);

        public override string ToString()
            => _builder.ToString();

        public static MutableString operator +(MutableString mutableString, string value)
        {
            mutableString._builder.Append(value);
            return mutableString;
        }

        public static string operator +(string value, MutableString MutableString)
            => value + MutableString.ToString();

        public static MutableString operator +(MutableString MutableString, char value)
        {
            MutableString._builder.Append(value);
            return MutableString;
        }

        public static string operator +(char value, MutableString mutableString)
            => value + mutableString.ToString();

        public static implicit operator string(MutableString mutableString)
            => mutableString.ToString();

        public static implicit operator MutableString(string value)
            => new MutableString(value);

        public static implicit operator ReadOnlySpan<char>(MutableString mutable)
            => mutable.ToString();

        public static explicit operator Span<char>(MutableString mutable)
            => new Span<char>(mutable.ToCharArray());

        public static explicit operator ReadOnlyMemory<char>(MutableString mutable)
            => new ReadOnlyMemory<char>(mutable.ToCharArray());

        public static explicit operator Memory<char>(MutableString mutable)
            => new Memory<char>(mutable.ToCharArray());

        IEnumerator<char> IEnumerable<char>.GetEnumerator()
            => ToString().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ToString().GetEnumerator();

        int IComparable.CompareTo(object obj)
            => obj switch
            {
                string str => ((IComparable<string>)this).CompareTo(str),
                MutableString mutable => ((IComparable<string>)this).CompareTo(mutable.ToString()),
                _ => throw new ArgumentException(nameof(obj))
            };

        int IComparable<string>.CompareTo(string other)
            => _builder.ToString().CompareTo(other);


        int IComparable<MutableString>.CompareTo(MutableString other)
            => ((IComparable<string>)this).CompareTo(other.ToString());

        public bool Equals(string other)
            => other.Equals(this);

        public bool Equals(MutableString other)
            => other.ToString().Equals(ToString());

        public char this[int index]
        {
            get => _builder[index];
            set => _builder[index] = value;
        }

        public override bool Equals(object obj)
            => obj switch
            {
                string str => Equals(str),
                MutableString mutable => Equals(mutable),
                _ => false
            };

        public override int GetHashCode()
            => _builder.GetHashCode();

        public static bool operator ==(MutableString left, MutableString right)
            => left.Equals(right);

        public static bool operator !=(MutableString left, MutableString right)
            => !left.Equals(right);

        public MutableString Insert(int index, object value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public MutableString Remove(int startIndex, int length)
        {
            _builder.Remove(startIndex, length);
            return this;
        }

        public MutableString Replace(char oldChar, char newChar)
            => Replace(oldChar, newChar, 0, _builder.Length);


        public MutableString Replace(char oldChar, char newChar, int startIndex, int count)
        {
            _builder.Replace(oldChar, newChar, startIndex, count);
            return this;
        }

        public MutableString Replace(string oldStr, string newStr)
            => Replace(oldStr, newStr, 0, _builder.Length);


        public MutableString Replace(string oldStr, string newStr, int startIndex, int count)
        {
            _builder.Replace(oldStr, newStr, startIndex, count);
            return this;
        }

        public MutableString AppendFormat(string format, params object[] args)
        {
            _builder.AppendFormat(format, args);
            return this;
        }

        public char[] ToCharArray() 
            => _builder.ToString().ToCharArray();

        public IEnumerable<char> AsEnumerable()
            => this;

        void ICollection<char>.Add(char item)
            => _builder.Append(item);

        public MutableString Clear()
        {
            _builder.Clear();
            return this;
        }

        void ICollection<char>.Clear() 
            => Clear();

        public bool Contains(char item)
        {
            for (int x = 0; x < Length; x++)
            {
                if (this[x] == item)
                    return true;
            }

            return false;
        }

        public bool Contains(string item)
            => ToString().Contains(item);

        void ICollection<char>.CopyTo(char[] array, int arrayIndex)
            => throw new NotSupportedException();

        bool ICollection<char>.Remove(char item)
            => throw new NotSupportedException();

        void ICollection.CopyTo(Array array, int index)
            => throw new NotSupportedException();
    }
}
