﻿using GroupBy.Data.Context;
using GroupBy.Core.Entities;
using GroupBy.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly GroupByDbContext context;

        public GroupRepository(GroupByDbContext context)
        {
            this.context = context;
        }

        public Group Create(Group group)
        {
            try
            {
                var c = context.Groups.Add(group).Entity;
                return c;
            }
            catch (Exception)
            {
                // TO-DO
                throw;
            }
        }

        public bool Delete(Group domain)
        {
            try
            {
                context.Groups.Remove(context.Groups.FirstOrDefault(g => g.Id == domain.Id));
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Group Get(Group domain)
        {
            return context.Groups.FirstOrDefault(g => g.Id == domain.Id);
        }

        public IEnumerable<Group> GetAll()
        {
            return context.Groups.ToList();
        }

        public IEnumerable<Volunteer> GetVolunteers(Group group)
        {
            throw new NotImplementedException();
        }

        public bool Update(Group group)
        {
            Group dbGroup = context.Groups.FirstOrDefault(g => g.Id == group.Id);
            if (dbGroup == default)
                return false;

            dbGroup.Name = group.Name;
            dbGroup.Description = group.Description;
            context.SaveChanges();
            return true;
        }
    }
}
