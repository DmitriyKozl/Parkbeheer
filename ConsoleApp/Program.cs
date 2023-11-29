// See https://aka.ms/new-console-template for more information

using System.Configuration;
using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Model;
using ParkDataLayer.DataContext;
using ParkDataLayer.Repositories;

string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;


//--------------------- Aanmaken Databank ----------------------------//
Console.WriteLine("Creating Database Context...");
ParkDataContext ctx = new ParkDataContext(connectionString);
Console.WriteLine("Ensuring Database is deleted and then created...");
ctx.Database.EnsureDeleted();
ctx.Database.EnsureCreated();
//--------------------------------------------------------------------//

Console.WriteLine("Creating Repositories...");
HuizenRepositoryEF HuisREPO = new(connectionString);
HuurderRepositoryEF HuurderREPO = new(connectionString);
ContractenRepositoryEF ContractREPO = new(connectionString);
ParkRepositoryEF ParkREPO = new(connectionString);

Console.WriteLine("Creating Managers...");
BeheerHuizen HuisBeheerder = new(HuisREPO);
BeheerHuurders HuurderBeheerder = new BeheerHuurders(HuurderREPO);
BeheerContracten ContractenBeheerder = new BeheerContracten(ContractREPO);
BeheerParken ParkBeheerder = new(ParkREPO);

Console.WriteLine("Adding new park...");
Park p1 = new("1", "' T koerken", "Gent");
ParkBeheerder.VoegParkToe(p1);
Console.WriteLine("Retrieving park...");
Park geefpark = ParkBeheerder.GeefPark("1");
Console.WriteLine($"Park retrieved: {geefpark}");
p1.ZetNaam("Gent-Brugge");
ParkBeheerder.UpdatePark(p1);

Console.WriteLine("Adding new house...");
HuisBeheerder.VoegNieuwHuisToe("Holdaal", 12, p1);
Console.WriteLine("Retrieving house...");
Huis h1 = HuisBeheerder.GeefHuis(1);
Huis h3 = HuisBeheerder.GeefHuis(1);
Console.WriteLine($"House retrieved: {h1}");
h1.ZetStraat("Niet in holdaal");
HuisBeheerder.UpdateHuis(h1);
HuisBeheerder.ArchiveerHuis(h1);

Console.WriteLine("Adding new tenant...");
HuurderBeheerder.VoegNieuweHuurderToe("Dmitriy Kozlov", new("Dmitriy@kozlov.com", "0485789771", "Holdaal 36"));
var geefh1 = HuurderBeheerder.GeefHuurder(1);
Console.WriteLine($"Tenant retrieved: {geefh1}");
HuurderBeheerder.GeefHuurders("Dmitriy Kozlov");

Console.WriteLine("Creating new contract...");
ContractenBeheerder.MaakContract("1", new(DateTime.Today, 7), geefh1, h3);
var c2 = ContractenBeheerder.GeefContract("1");
Console.WriteLine($"Contract retrieved: {c2}");
var lijstContracten = ContractenBeheerder.GeefContracten(DateTime.Today, null);
Console.WriteLine($"List of contracts: {lijstContracten.Count}");
c2.ZetHuis(h3);
ContractenBeheerder.UpdateContract(c2);
ContractenBeheerder.AnnuleerContract(c2);
