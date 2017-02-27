namespace Tasker.Core
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
