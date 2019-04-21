using System;

namespace ShoppingEcommerce.Core.Abstraction
{
    /// <summary>
    /// Many objects are not fundamentally defined by their attributes, 
    /// but rather by a thread of continuity and identity.
    /// 
    /// If two instances of the same object have different attribute values, 
    /// but same identity value, so they are the same entity.
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity entity))
            {
                return false;
            }

            if (GetType() != entity.GetType())
            {
                return false;
            }

            if (Id == Guid.Empty || entity.Id == Guid.Empty)
            {
                return false;
            }

            if (ReferenceEquals(this, entity))
            {
                return true;
            }

            return Id == entity.Id;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}