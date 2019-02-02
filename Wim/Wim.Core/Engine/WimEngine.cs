using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public sealed class WimEngine : IEngine
    {
        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string PersonExists = "Person with name {0} already exists!";
        private const string PersonCreated = "Person with name {0} was created!";
        private const string NoPeopleInApplication = "There are no people!";
        private const string NoTeamsInApplication = "There are no teams in application!";
        private const string MemberDoesNotExist = "The member does not exist!";
        
        //private const string ProductAddedToCategory = "Product {0} added to category {1}!";
        //private const string ProductRemovedCategory = "Product {0} removed from category {1}!";
        //private const string ShampooAlreadyExist = "Shampoo with name {0} already exists!";
        //private const string ShampooCreated = "Shampoo with name {0} was created!";
        //private const string ToothpasteAlreadyExist = "Toothpaste with name {0} already exists!";
        //private const string ToothpasteCreated = "Toothpaste with name {0} was created!";
        //private const string CreamAlreadyExist = "Cream with name {0} already exists!";
        //private const string CreamCreated = "Cream with name {0} was created!";
        //private const string ProductAddedToShoppingCart = "Product {0} was added to the shopping cart!";
        //private const string ProductDoesNotExistInShoppingCart = "Shopping cart does not contain product with name {0}!";
        //private const string ProductRemovedFromShoppingCart = "Product {0} was removed from the shopping cart!";
        //private const string TotalPriceInShoppingCart = "${0} total price currently in the shopping cart!";
        //private const string InvalidGenderType = "Invalid gender type!";
        //private const string InvalidUsageType = "Invalid usage type!";
        //private const string InvalidScentType = "Invalid scent type!";

        private static readonly WimEngine SingleInstance = new WimEngine();

        private readonly WimFactory factory;
        private readonly IAllMembers allMembers;
        private readonly IAllTeams allTeams;

        private WimEngine()
        {
            this.factory = new WimFactory();
            this.allMembers = new AllMembers();
            this.allTeams = new AllTeams();
        }

        public static WimEngine Instance
        {
            get
            {
                return SingleInstance;
            }
        }

        public void Start()
        {
            var commands = this.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.PrintReports(commandResult);
        }

        private IList<ICommand> ReadCommands()
        {
            var commands = new List<ICommand>();

            var currentLine = Console.ReadLine();

            while (!string.IsNullOrEmpty(currentLine))
            {
                var currentCommand = Command.Parse(currentLine);
                commands.Add(currentCommand);

                currentLine = Console.ReadLine();
            }

            return commands;
        }

        private IList<string> ProcessCommands(IList<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    var report = this.ProcessSingleCommand(command);
                    reports.Add(report);
                }
                catch (Exception ex)
                {
                    reports.Add(ex.Message);
                }
            }

            return reports;
        }

        private string ProcessSingleCommand(ICommand command)
        {
            switch (command.Name)
            {
                case "CreatePerson":
                    var personName = command.Parameters[0];
                    return this.CreatePerson(personName);

                case "ShowAllPeople":
                    //var categoryNameToAdd = command.Parameters[0];
                    //var productToAdd = command.Parameters[1];
                    return this.ShowAllPeople();

                case "ShowAllTeams":
                    //var categoryNameToRemove = command.Parameters[0];
                    //var productToRemove = command.Parameters[1];
                    return this.ShowAllTeams();

                case "ShowPersonsActivity":
                      var memberName = command.Parameters[0];
                    //    return this.ShowCategory(categoryToShow);
                    return this.ShowMemberActivityToString(memberName);

                //case "CreateShampoo":
                //    var shampooName = command.Parameters[0];
                //    var shampooBrand = command.Parameters[1];
                //    var shampooPrice = decimal.Parse(command.Parameters[2]);
                //    var shampooGender = this.GetGender(command.Parameters[3]);
                //    var shampooMilliliters = uint.Parse(command.Parameters[4]);
                //    var shampooUsage = this.GetUsage(command.Parameters[5]);
                //    return this.CreateShampoo(shampooName, shampooBrand, shampooPrice, shampooGender, shampooMilliliters, shampooUsage);

                //case "CreateToothpaste":
                //    var toothpasteName = command.Parameters[0];
                //    var toothpasteBrand = command.Parameters[1];
                //    var toothpastePrice = decimal.Parse(command.Parameters[2]);
                //    var toothpasteGender = this.GetGender(command.Parameters[3]);
                //    var toothpasteIngredients = command.Parameters[4].Trim().Split(',').ToList();
                //    return this.CreateToothpaste(toothpasteName, toothpasteBrand, toothpastePrice, toothpasteGender, toothpasteIngredients);

                //case "CreateCream":
                //    var creamName = command.Parameters[0];
                //    var creamBrand = command.Parameters[1];
                //    var creamPrice = decimal.Parse(command.Parameters[2]);
                //    var creamGender = this.GetGender(command.Parameters[3]);
                //    var creamScent = this.GetScent(command.Parameters[4]);
                //    return this.CreateCream(creamName, creamBrand, creamPrice, creamGender, creamScent);

                //case "AddToShoppingCart":
                //    var productToAddToCart = command.Parameters[0];
                //    return this.AddToShoppingCart(productToAddToCart);

                //case "RemoveFromShoppingCart":
                //    var productToRemoveFromCart = command.Parameters[0];
                //    return this.RemoveFromShoppingCart(productToRemoveFromCart);

                //case "TotalPrice":
                //    return this.shoppingCart.ProductList.Any() ? string.Format(TotalPriceInShoppingCart, this.shoppingCart.TotalPrice()) : $"No product in shopping cart!";

                default:
                    return string.Format(InvalidCommand, command.Name);
            }
        }

        private void PrintReports(IList<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
            }

            Console.Write(output.ToString());
        }


        //FOR IMPROVING!!!
        private string CreatePerson(string personName)
        {

            var allNames2 = (allMembers.AllMembersList.GetType().GetProperties());
            List<string> allNames = new List<string>();
            foreach (var item in allNames2)
            {
                allNames.Add(item.ToString());
            }

            if (allNames.Contains(personName))
            {
                return string.Format(PersonExists, personName);
            }

            var person = this.factory.CreateMember(personName);
            allMembers.AddMember(person);

            return string.Format(PersonCreated, personName);
        }

        private string ShowAllPeople()
        {

            if (this.allMembers.AllMembersList.Count == 0)
            {
                return string.Format(NoPeopleInApplication);
            }

            var peopleToDisplay = allMembers.ShowAllMembersToString(allMembers.AllMembersList);

            return string.Format(peopleToDisplay);
        }

        private string ShowAllTeams()
        {
            if (this.allTeams.AllTeamsList.Count == 0)
            {
                return string.Format(NoTeamsInApplication);
            }

            var teamsToDisplay = allTeams.ShowAllTeamsToString(allTeams.AllTeamsList);            

            return string.Format(teamsToDisplay);
        }

        private string ShowMemberActivityToString(string memberName)
        {
            if (string.IsNullOrEmpty(memberName))
            {
                return string.Format(MemberDoesNotExist);

            }

            if (!allMembers.AllMembersList.ContainsKey(memberName))
            {
                return string.Format(MemberDoesNotExist);
            }

            var category = this.categories[categoryToShow];

            return category.Print();
        }

        //private string CreateShampoo(string shampooName, string shampooBrand, decimal shampooPrice, GenderType shampooGender, uint shampooMilliliters, UsageType shampooUsage)
        //{
        //    if (this.products.ContainsKey(shampooName))
        //    {
        //        return string.Format(ShampooAlreadyExist, shampooName);
        //    }

        //    var shampoo = this.factory.CreateShampoo(shampooName, shampooBrand, shampooPrice, shampooGender, shampooMilliliters, shampooUsage);
        //    this.products.Add(shampooName, (IProduct)shampoo);

        //    return string.Format(ShampooCreated, shampooName);
        //}

        //private string CreateToothpaste(string toothpasteName, string toothpasteBrand, decimal toothpastePrice, GenderType toothpasteGender, IList<string> toothpasteIngredients)
        //{
        //    if (this.products.ContainsKey(toothpasteName))
        //    {
        //        return string.Format(ToothpasteAlreadyExist, toothpasteName);
        //    }

        //    var toothpaste = this.factory.CreateToothpaste(toothpasteName, toothpasteBrand, toothpastePrice, toothpasteGender, toothpasteIngredients);
        //    this.products.Add(toothpasteName, (IProduct)toothpaste);

        //    return string.Format(ToothpasteCreated, toothpasteName);
        //}

        //private string CreateCream(string creamName, string creamBrand, decimal creamPrice, GenderType creamGender, Scent scent)
        //{
        //    if (this.products.ContainsKey(creamName))
        //    {
        //        return string.Format(CreamAlreadyExist, creamName);
        //    }

        //    var cream = this.factory.CreateCream(creamName, creamBrand, creamPrice, creamGender, scent);
        //    this.products.Add(creamName, (IProduct)cream);

        //    return string.Format(CreamCreated, creamName);
        //}

        //private string AddToShoppingCart(string productName)
        //{
        //    if (!this.products.ContainsKey(productName))
        //    {
        //        return string.Format(ProductDoesNotExist, productName);
        //    }

        //    var product = this.products[productName];
        //    this.shoppingCart.AddProduct(product);

        //    return string.Format(ProductAddedToShoppingCart, productName);
        //}

        //private string RemoveFromShoppingCart(string productName)
        //{
        //    if (!this.products.ContainsKey(productName))
        //    {
        //        return string.Format(ProductDoesNotExist, productName);
        //    }

        //    var product = this.products[productName];

        //    if (!this.shoppingCart.ContainsProduct(product))
        //    {
        //        return string.Format(ProductDoesNotExistInShoppingCart, productName);
        //    }

        //    this.shoppingCart.RemoveProduct(product);

        //    return string.Format(ProductRemovedFromShoppingCart, productName);
        //}

        //private GenderType GetGender(string genderAsString)
        //{
        //    switch (genderAsString.ToLower())
        //    {
        //        case "men":
        //            return GenderType.Men;
        //        case "women":
        //            return GenderType.Women;
        //        case "unisex":
        //            return GenderType.Unisex;
        //        default:
        //            throw new InvalidOperationException(InvalidGenderType);
        //    }
        //}

        //private Scent GetScent(string scentAsString)
        //{
        //    switch (scentAsString.ToLower())
        //    {
        //        case "lavander":
        //            return Scent.Lavender;
        //        case "vanilla":
        //            return Scent.Vanilla;
        //        case "rose":
        //            return Scent.Rose;
        //        default:
        //            throw new InvalidOperationException(InvalidScentType);
        //    }
        //}

        //private UsageType GetUsage(string usageAsString)
        //{
        //    switch (usageAsString.ToLower())
        //    {
        //        case "everyday":
        //            return UsageType.EveryDay;
        //        case "medical":
        //            return UsageType.Medical;
        //        default:
        //            throw new InvalidOperationException(InvalidUsageType);
        //    }
        //}
    }
}
