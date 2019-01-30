//using System;
//using System.Collections.Generic;
//using System.Text;
//using Wim.Models.Interfaces;

//namespace Wim.Models
//{
//    public class Person : IPerson
//    {
//        private string name;

//        public Person(string name)
//        {
//            this.Name = name;
//        }

//        public string Name
//        {
//            get
//            {
//                return this.name;
//            }
//            set
//            {
//                if (string.IsNullOrEmpty(value))
//                {
//                    throw new ArgumentNullException("");
//                }

//                this.name = value;
//            }
//        }

//    }
//}
