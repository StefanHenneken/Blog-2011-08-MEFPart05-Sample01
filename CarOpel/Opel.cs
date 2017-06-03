using System;
using System.ComponentModel.Composition;
using CarContract;

namespace CarOpel
{
    [Export(typeof(ICarContract))]
    public class Opel : ICarContract
    {
        public string GetName()
        {           
            return "Opel";
        }
    }
}
