using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using BirthdayConsole.Commands;
using BirthdayConsole.Domain;

namespace UnitTestBirthdayConsole
{
    public class CommandFactoryTests
    {
        [Test]
        public void GetCommandTest_get_string_add_return_object_CommandAdd()
        {
            string cmd = "add";

            Mock<IPersonRepository> mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(p => p.Persons).Returns(new List<Person> { 
                    new Person() { Id=1, FirstName="person1", LastName="lastname1", Relation="test1" },
                    new Person() { Id=2, FirstName="person2", LastName="lastname2", Relation="test2" },
                    new Person() { Id=3, FirstName="person3", LastName="lastname3", Relation="test3" },
                    new Person() { Id=4, FirstName="person4", LastName="lastname4", Relation="test4" }
            }.AsQueryable());

            var factory = new CommandFactory(mockRepo.Object);

            ICommand result = factory.GetCommand(cmd);

            Assert.AreEqual(typeof(CommandAdd), result.GetType());
        }

        [Test]
        public void GetCommandTest_get_string_del_return_object_CommandDel()
        {
            string cmd = "del";

            Mock<IPersonRepository> mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(p => p.Persons).Returns(new List<Person> {
                    new Person() { Id=1, FirstName="person1", LastName="lastname1", Relation="test1" },
                    new Person() { Id=2, FirstName="person2", LastName="lastname2", Relation="test2" },
                    new Person() { Id=3, FirstName="person3", LastName="lastname3", Relation="test3" },
                    new Person() { Id=4, FirstName="person4", LastName="lastname4", Relation="test4" }
            }.AsQueryable());

            var factory = new CommandFactory(mockRepo.Object);

            ICommand result = factory.GetCommand(cmd);

            Assert.AreEqual(typeof(CommandDel), result.GetType());
        }

        [Test]
        public void GetCommandTest_get_string_set_return_object_CommandSet()
        {
            string cmd = "set";

            Mock<IPersonRepository> mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(p => p.Persons).Returns(new List<Person> {
                    new Person() { Id=1, FirstName="person1", LastName="lastname1", Relation="test1" },
                    new Person() { Id=2, FirstName="person2", LastName="lastname2", Relation="test2" },
                    new Person() { Id=3, FirstName="person3", LastName="lastname3", Relation="test3" },
                    new Person() { Id=4, FirstName="person4", LastName="lastname4", Relation="test4" }
            }.AsQueryable());

            var factory = new CommandFactory(mockRepo.Object);

            ICommand result = factory.GetCommand(cmd);

            Assert.AreEqual(typeof(CommandSet), result.GetType());
        }

        [Test]
        public void GetCommandTest_get_string_list_return_object_CommandSet()
        {
            string cmd = "list";

            Mock<IPersonRepository> mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(p => p.Persons).Returns(new List<Person> {
                    new Person() { Id=1, FirstName="person1", LastName="lastname1", Relation="test1" },
                    new Person() { Id=2, FirstName="person2", LastName="lastname2", Relation="test2" },
                    new Person() { Id=3, FirstName="person3", LastName="lastname3", Relation="test3" },
                    new Person() { Id=4, FirstName="person4", LastName="lastname4", Relation="test4" }
            }.AsQueryable());

            var factory = new CommandFactory(mockRepo.Object);

            ICommand result = factory.GetCommand(cmd);

            Assert.AreEqual(typeof(CommandList), result.GetType());
        }
    }
}