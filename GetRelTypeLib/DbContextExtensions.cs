using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace GetRelTypeLib
{
    public static class DbContextExtensions
    {
        public static RelationshipType GetRelationshipType(this DbContext dbContext, Type entityType, string propertyName)
        {
            // Get navigation property
            var edmEntityType = dbContext.GetEdmSpaceType(entityType);
            if (edmEntityType == null)
                throw new ArgumentException("Getting entity type from metadata failed.", "entityType");
            var navProp = edmEntityType.NavigationProperties
                .SingleOrDefault(p => p.Name == propertyName);
            if (navProp == null)
                throw new ArgumentException("Getting navigation property failed.", "propertyName");

            if (navProp.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One
                && (navProp.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne
                    || navProp.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One))
                return RelationshipType.OneToOne;

            if (navProp.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many
                && (navProp.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne
                    || navProp.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One))
                return RelationshipType.ManyToOne;

            if (navProp.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many
                && navProp.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                return RelationshipType.ManyToMany;

            if ((navProp.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One
                 || navProp.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
                && navProp.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                return RelationshipType.OneToMany;

            throw new Exception(string.Format("Cannot determine relationship type for {0} property on {1}.", propertyName, entityType.FullName));
        }

        public static IEnumerable<NavigationProperty> GetNavigationProperties(this DbContext dbContext, Type entityType)
        {
            var edmEntityType = dbContext.GetEdmSpaceType(entityType);
            if (edmEntityType == null)
                throw new ArgumentException("Getting entity type from metadata failed.", "entityType");
            return edmEntityType.NavigationProperties;
        }

        private static EntityType GetEdmSpaceType(this DbContext dbContext, Type entityType)
        {
            MetadataWorkspace workspace = ((IObjectContextAdapter)dbContext)
                .ObjectContext.MetadataWorkspace;

            StructuralType oType = workspace.GetItems<StructuralType>(DataSpace.OSpace)
                .Where(e => e.FullName == entityType.FullName).SingleOrDefault();

            if (oType == null) return null;

            return workspace.GetEdmSpaceType(oType) as EntityType;
        }
    }
}
