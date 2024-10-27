using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections;

namespace DogsHouse.Services.Utility
{
    public class UpdateUtility<T> where T : class
    {
        /// <summary>
        /// Update values of <paramref name="oldEntity"/> with new values of <paramref name="newEntity"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="oldEntity"></param>
        /// <param name="newEntity"></param>
        /// <returns>An updated entity</returns>
        public static T Update(IModel model, T oldEntity, T newEntity)
        {
            // get the original type
            var type = UnProxy(model, newEntity.GetType());

            // get only needed properties
            var filteredProperties = type.GetProperties()
                                         .Where(x => (x.DeclaringType.Namespace == type.Namespace) &&
                                                      !x.Name.StartsWith("id", StringComparison.CurrentCultureIgnoreCase));

            var oldValue = default(object);
            var newValue = default(object);

            var propName = string.Empty;
            foreach (var property in filteredProperties)
            {
                propName = property.Name;
                oldValue = type.GetProperty(propName)
                               .GetValue(oldEntity);

                newValue = type.GetProperty(propName)
                               .GetValue(newEntity);

                if (newValue != null)
                {
                    if (oldValue is not ICollection)
                    {
                        property.SetValue(oldEntity, newValue);
                    }
                }

            }
            return oldEntity;
        }

        /// <summary>
        /// Get the original type of old entity which equals the <paramref name="type"/> of new one
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type UnProxy(IModel model, Type type)
        {
            // get all entity types from db context
            var entityTypes = model.GetEntityTypes();

            var originalType = default(Type);
            foreach (var entityType in entityTypes)
            {
                originalType = entityType.ClrType;
                if (originalType.Name == type.Name)
                    break;
            }

            return originalType;
        }
    }
}
