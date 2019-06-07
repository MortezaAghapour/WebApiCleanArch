using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiCleanArch.Domain.Entities.Base
{
    public abstract class BaseEntity<TKey>:IEntity
    {
        public TKey Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}
