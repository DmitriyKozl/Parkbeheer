// See https://aka.ms/new-console-template for more information


using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using ParkDataLayer.DataContext;

string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;


//-------------------------------------------------------------//
ParkDataContext ctx = new ParkDataContext(connectionString);
ctx.Database.EnsureDeleted();
ctx.Database.EnsureCreated();
//-------------------------------------------------------------//