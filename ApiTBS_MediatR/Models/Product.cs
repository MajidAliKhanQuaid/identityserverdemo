using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTBS_MediatR.Models
{
    public class Product
    {
        public string Name { get; internal set; }
        public int Id { get; internal set; }
        public int Price { get; internal set; }
    }
}
