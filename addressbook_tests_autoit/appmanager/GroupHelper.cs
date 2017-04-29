using System;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEWINTITLE = "Delete group";
        public static string GROUPSVIEWTREE = "WindowsForms10.SysTreeView32.app.0.2c908d51";
        public static string BUTTON_CLOSE = "WindowsForms10.BUTTON.app.0.2c908d54";
        public static string BUTTON_OPEN_EDITOR = "WindowsForms10.BUTTON.app.0.2c908d512";
        public static string BUTTON_NEW = "WindowsForms10.BUTTON.app.0.2c908d53";


        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public List<GroupData> GetGroupsList()
        {
            OpenGroupsDialog();
            List<GroupData> list = GetGroupNames();
            CloseGroupsDialog();
            return list;
        }

        public List<GroupData> GetGroupNames()
        {
            List<GroupData> list = new List<GroupData>();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", GROUPSVIEWTREE,
                "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(
                     GROUPWINTITLE, "", GROUPSVIEWTREE,
                     "GetText", "#0|#" + i, "");

                list.Add(new GroupData() { Name = item });
            }

            return list;
        }

        public void Remove(GroupData group)
        {
            OpenGroupsDialog();
            SelectGroup(group);
            DeleteButtonClick();
            CloseGroupsDialog();
        }


        public void SelectGroup(GroupData group)
        {
            aux.WinActivate(GROUPWINTITLE);

            string count = aux.ControlTreeView(GROUPWINTITLE, "", GROUPSVIEWTREE,
                                               "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(
                     GROUPWINTITLE, "", GROUPSVIEWTREE,
                     "GetText", "#0|#" + i, "");

                if (item == group.Name)
                {
                    aux.ControlTreeView(
                     GROUPWINTITLE, "", GROUPSVIEWTREE,
                     "Select", "#0|#" + i, "");

                }
            }
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialog();
            PerformCreateGroupAction(newGroup);
            CloseGroupsDialog();
        }

        public void PerformCreateGroupAction(GroupData newGroup)
        {
            aux.ControlClick(GROUPWINTITLE, "", BUTTON_NEW); 
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
        }

        public void DeleteButtonClick()
        {
            aux.WinActivate(GROUPWINTITLE);
            aux.Send("{DELETE}");
            aux.WinActivate(DELETEWINTITLE);
            aux.Send("{ENTER}");
            aux.WinActivate(GROUPWINTITLE);

        }

        public void CloseGroupsDialog()
        {
            aux.ControlClick(GROUPWINTITLE, "", BUTTON_CLOSE); 
        }

        public void OpenGroupsDialog()
        {
            aux.ControlClick(WINTITLE, "", BUTTON_OPEN_EDITOR); 
            aux.WinWait(GROUPWINTITLE);
        }
    }
}
