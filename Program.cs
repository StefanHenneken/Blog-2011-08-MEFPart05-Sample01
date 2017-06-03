using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using CarContract;

namespace CarHost
{
    class Program
    {
        [ImportMany(typeof(ICarContract), AllowRecomposition=true)]
        private IEnumerable<ICarContract> CarParts { get; set; }

        static void Main(string[] args)
        {
            new Program().Run();
        }
        void Run()
        {
            var catalog = new AggregateCatalog();

            // Parameterliste mit Klassen die berücksichtigt werden sollen
            var typeCatalog = new TypeCatalog(typeof(CarMercedes.Mercedes), typeof(CarAudi.Audi));
            catalog.Catalogs.Add(typeCatalog);
           
            // Assembly wird über den FullName dynamisch geladen und dem Katalog hinzugefügt
            var assemblyCatalog = new AssemblyCatalog(Assembly.Load("Opel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
            catalog.Catalogs.Add(assemblyCatalog);
            
            // Assemblies aus dem Verzeichnis '.\AddIn' werden berücksichtigt
            var directoryCatalog = new DirectoryCatalog(@".\AddIn");
            catalog.Catalogs.Add(directoryCatalog);

            // Container mit dem entsprechenden Katalog anlegen
            var container = new CompositionContainer(catalog);

            // Composition über die Klasse CompositionBatch starten
            var batch = new CompositionBatch();
            batch.AddPart(this);
            container.Compose(batch);

            // alternativ kann die Composition auch ohne CompositionBatch gestartet werden
            //container.ComposeParts(this);
            // eine Parameterliste von Objekten kann auch angegeben werden
            //container.ComposeParts(this, obj1, obj2);

            // Anzeige alle Composable Parts
            foreach (ICarContract car in CarParts)
                Console.WriteLine(car.GetName());

            container.Dispose();
        }
    }
}
