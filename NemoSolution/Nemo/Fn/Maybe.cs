﻿using System;
using Nemo.Extensions;

namespace Nemo.Fn
{
    [Serializable]
    public sealed class Maybe<T>
    {
        public readonly static Maybe<T> Empty = new Maybe<T>();
        public T Value { get; private set; }
        public bool HasValue { get; private set; }
        
        private Maybe()
        {
            HasValue = false;
        }

        public Maybe(T value)
        {
            Value = value;
            HasValue = true;
        }

        public static implicit operator T(Maybe<T> maybe)
        {
            maybe.ThrowIfNull("maybe");
            return maybe.Value;
        }

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public override string ToString()
        {
            if (this.HasValue)
            {
                return this.Value.ToString();
            }
            else
            {
                return string.Format("{0}::Empty", base.ToString());
            }
        }
    }
}
