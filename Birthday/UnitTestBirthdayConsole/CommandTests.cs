using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using Moq;
using BirthdayConsole.Commands;
using BirthdayConsole.Domain;

namespace UnitTestBirthdayConsole
{
    public class CommandTests
    {
        [Test]
        public void ExecuteTest_check_add_person_to_database()
        {
            Mock<IPersonRepository> mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(p => p.Persons).Returns(new List<Person> {
                    new Person() { Id=1, FirstName="person1", LastName="lastname1",
                                         Relation ="test1", Birthday = Convert.ToDateTime("2019-01-01") },
                    new Person() { Id=2, FirstName="person2", LastName="lastname2", Relation="test2" },
                    new Person() { Id=3, FirstName="person3", LastName="lastname3", Relation="test3" },
                    new Person() { Id=4, FirstName="person4", LastName="lastname4", Relation="test4" }
            }.AsQueryable());

            using (StringReader sr = new StringReader(
                string.Format("person5{0}lastname5{0}test5{0}2019-05-05{0}",
                Environment.NewLine)))
            {
                Console.SetIn(sr);

                var factory = new CommandFactory(mockRepo.Object);

                ICommand result = factory.GetCommand("add");

                result.Execute();

                mockRepo.Verify(r => r.SavePerson(It.Is<Person>(person => person.FirstName == "person5")));
            }
        }

        [Test]
        public void ExecuteTest_can_delete_person()
        {
            Mock<IPersonRepository> mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(p => p.Persons).Returns(new List<Person> {
                    new Person() { Id=1, FirstName="person1", LastName="lastname1",
                                         Relation ="test1", Birthday = Convert.ToDateTime("2019-01-01") },
                    new Person() { Id=2, FirstName="person2", LastName="lastname2", Relation="test2" },
                    new Person() { Id=3, FirstName="person3", LastName="lastname3", Relation="test3" },
                    new Person() { Id=4, FirstName="person4", LastName="lastname4", Relation="test4" }
            }.AsQueryable());

            using (StringReader sr = new StringReader(string.Format("1{0}", Environment.NewLine)))
            {
                Console.SetIn(sr);

                var factory = new CommandFactory(mockRepo.Object);

                ICommand result = factory.GetCommand("del");

                result.Execute();

                mockRepo.Verify(r => r.DeletePerson(1));
            }
        }

        [Test]
        public void ExecuteTest_check_change_person()
        {
            Mock<IPersonRepository> mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(p => p.Persons).Returns(new List<Person> {
                    new Person() { Id=1, FirstName="person1", LastName="lastname1",
                                         Relation ="test1", Birthday = Convert.ToDateTime("2019-01-01") },
                    new Person() { Id=2, FirstName="person2", LastName="lastname2", Relation="test2" },
                    new Person() { Id=3, FirstName="person3", LastName="lastname3", Relation="test3" },
                    new Person() { Id=4, FirstName="person4", LastName="lastname4", Relation="test4" }
            }.AsQueryable());

            using (StringReader sr = new StringReader(string.Format("1{0}new-person{0}new-lastname{0}new-rel{0}2019-05-05{0}",
                                                      Environment.NewLine)))
            {
                Console.SetIn(sr);

                var factory = new CommandFactory(mockRepo.Object);

                ICommand result = factory.GetCommand("set");

                result.Execute();

                mockRepo.Verify(r => r.SavePerson(It.Is<Person>(p => p.FirstName == "new-person")));
            }
        }

        [Test]
        public void ExecuteTest_check_get_list_of_persons()
        {
            Mock<IPersonRepository> mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(p => p.Persons).Returns(new List<Person> {
                    new Person() { Id=1, FirstName="person1", LastName="lastname1",
                                         Relation ="test1", Birthday = Convert.ToDateTime("2019-01-01") },
                    new Person() { Id=2, FirstName="person2", LastName="lastname2", Relation="test2" },
                    new Person() { Id=3, FirstName="person3", LastName="lastname3", Relation="test3" },
                    new Person() { Id=4, FirstName="person4", LastName="lastname4", Relation="test4" }
            }.AsQueryable());

            var factory = new CommandFactory(mockRepo.Object);

            ICommand result = factory.GetCommand("list");

            result.Execute();

            mockRepo.Verify(p => p.Persons, Times.Exactly(1));
        }
    }
}
