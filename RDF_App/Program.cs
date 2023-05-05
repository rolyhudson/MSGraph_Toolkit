
using System;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Ontology;
using VDS.RDF.Parsing;

OntologyGraph g = new OntologyGraph();
FileLoader.Load(g, "C:\\Users\\rhudson\\OneDrive - HKS Inc\\Documents\\Projects\\MSGraph\\DesignConcepts.owl");
OntologyClass someClass = g.CreateOntologyClass(new Uri("http://www.semanticweb.org/lseeley/ontologies/2022/6/DesignConcepts#DesignSubjects"));
foreach(var t in g.Triples) {
    Console.WriteLine("subject : "+t.Subject.ToString());
    Console.WriteLine("predicate : " + t.Predicate.ToString());
    Console.WriteLine("object: " + t.Object.ToString());
}
//Write out Super Classes
foreach (OntologyClass c in someClass.SuperClasses)
{
    Console.WriteLine("Super Class: " + c.Resource.ToString());
}
//Write out Sub Classes
foreach (OntologyClass c in someClass.SubClasses)
{
    Console.WriteLine("Sub Class: " + c.Resource.ToString());
}