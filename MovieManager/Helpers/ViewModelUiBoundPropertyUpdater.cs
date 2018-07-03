namespace MovieManager.Helpers
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Contracts.ViewModels;

    public static class ViewModelUiBoundPropertyUpdater
    {
        public static void UpdateValue<T, TProperty>(this T obj, Expression<Func<T, TProperty>> memberLambda, object value) where T : IViewModel
        {
            if (!(memberLambda.Body is MemberExpression memberExpression))
            {
                return;
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                return;
            }

            property.SetValue(obj, null, null);

            if (value != null)
            {
                property.SetValue(obj, value, null);
            }
        }
    }
}
