namespace MovieManager.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public static class PropertyChangedHelper
    {
        public static bool ChangeAndNotify<T>(this PropertyChangedEventHandler handler, ref T field, T value, Expression<Func<T>> memberExpression)
        {
            if (memberExpression == null)
            {
                throw new ArgumentNullException(nameof(memberExpression));
            }

            if (!(memberExpression.Body is MemberExpression body))
            {
                throw new ArgumentException("Lambda must return a property.");
            }

            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;

            if (body.Expression is ConstantExpression vmExpression)
            {
                var sender = Expression.Lambda(vmExpression).Compile().DynamicInvoke();
                handler?.Invoke(sender, new PropertyChangedEventArgs(body.Member.Name));
            }
            
            return true;
        }
    }
}