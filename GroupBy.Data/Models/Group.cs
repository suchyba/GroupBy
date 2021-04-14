using System.Collections.Generic;

namespace GroupBy.Data.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Group ParentGroup { get; set; }
        public IEnumerable<Group> ChildGroups { get; set; }
        public Volunteer Owner { get; set; }
        public IEnumerable<Volunteer> Members { get; set; }
        public IEnumerable<AccountingBook> AccountingBooks { get; set; }
        public IEnumerable<Resolution> Resolutions { get; set; }
        public virtual InventoryBook InventoryBook { get; set; }
    }
}
