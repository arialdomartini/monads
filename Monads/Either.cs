using System;

namespace Monads
{
    public static class Either
    {
        public struct Right<TR>
        {
            internal TR Value { get; }

            internal Right(TR value)
            {
                Value = value;
            }
        }

        public struct Left<TL>
        {
            internal TL Value { get; }

            internal Left(TL value)
            {
                Value = value;
            }
        }
    }
    
    public struct Either<TL, TR>
    {
        private readonly TR _right;
        private readonly TL _left;
        private readonly bool _isRight;

        private Either(TR right)
        {
            _left = default(TL);
            _right = right;
            _isRight = true;
        }

        private Either(TL left)
        {
            _left = left;
            _right = default(TR);

            _isRight = false;
        }

        private static Either<TL, TR> Right(TR right) =>
            new Either<TL, TR>(right);

        private static Either<TL, TR> Left(TL left) =>
            new Either<TL, TR>(left);

        public static implicit operator Either<TL, TR>(TR value) =>
            Right(value);
        
        public static implicit operator Either<TL, TR>(TL value) =>
            Left(value);

        public static implicit operator Either<TL, TR>(Either.Right<TR> right) =>
            Right(right.Value);

        public static implicit operator Either<TL, TR>(Either.Left<TL> left) =>
            Left(left.Value);

        public T Match<T>(Func<TL, T> leftFunc, Func<TR, T> rightFunc) => 
            _isRight? rightFunc(_right) : leftFunc(_left);        
    }
}