using System;
using System.ComponentModel.Composition;
using CarContract;

namespace CarAudi
{
    [Export(typeof(ICarContract))]
    public class Audi : ICarContract
    {
        public string GetName()
        {
            return "Audi";
        }
    }
}
