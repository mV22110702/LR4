namespace LR4
{
    public class LibraryInfo
    {
        public string Name { get; set; }
        public List<string> AvailableBooks { get; set; }
        public int MaxMembersCount { get; set; }
        public List<LibraryMember> Members { get; set; }

        public LibraryInfo()
        {
            this.Name = string.Empty;
            this.AvailableBooks = new List<string>();
        }

    }
}
