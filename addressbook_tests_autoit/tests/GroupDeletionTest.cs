using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupDeletionTest : TestBase
    {
        [Test]
        public void DeleteGroupData()
        {
             GroupData group = new GroupData()
            {
                Name = "test"
            };
            app.Groups.Add(group);
            List<GroupData> oldGroups = app.Groups.GetGroupsList();

            app.Groups.Remove(group);
            List<GroupData> newGroups = app.Groups.GetGroupsList();

            oldGroups.Remove(group);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
            
        }
    }
}
