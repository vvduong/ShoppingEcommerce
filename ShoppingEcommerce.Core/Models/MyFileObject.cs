using System;

namespace ShoppingEcommerce.Core.Models
{
    public class MyFileObject
    {
        public MyFileObject()
        {
            Content = null;
        }

        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Ext { get; set; }

        public byte[] Content { get; set; }

        public string DownloadUrl { get; set; }
    }
}