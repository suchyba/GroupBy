using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class DocumentRepository : AsyncRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context) : base(context)
        {

        }
        public override async Task<Document> CreateAsync(Document domain)
        {
            // TODO add checking if the file pointed by FilePath exists

            var projectId = domain.RelatedProject?.Id;
            Project project = null;
            if (projectId != null)
            {
                project = await context.Set<Project>().FirstOrDefaultAsync(p => p.Id == projectId);
                if (project == null)
                    throw new NotFoundException("Project", projectId);
            }
            domain.RelatedProject = project;

            var tempGroups = new List<Group>();
            foreach (var group in domain.Groups)
            {
                Group g = await context.Set<Group>().FirstOrDefaultAsync(g => g.Id == group.Id);

                if (g == null)
                    throw new NotFoundException("Group", group.Id);

                tempGroups.Add(g);
            }

            domain.Groups = tempGroups;
            
            var newDocument = await context.Set<Document>().AddAsync(domain);
            await context.SaveChangesAsync();
            return newDocument.Entity;
        }

        public override async Task<Document> GetAsync(Document domain)
        {
            var d = await context.Set<Document>()
                .Include(d => d.Groups)
                .Include(d => d.RelatedProject)
                .FirstOrDefaultAsync(d => d.Id == domain.Id);
            if (d == null)
                throw new NotFoundException("Document", domain.Id);
            return d;
        }

        public override async Task<Document> UpdateAsync(Document domain)
        {
            var toModify = await GetAsync(domain);
            toModify.Name = domain.Name;

            // to further check if this file exists
            toModify.FilePath = domain.FilePath;

            var projectId = domain.RelatedProject?.Id;
            Project project = null;
            if(projectId != null)
            {
                project = await context.Set<Project>().FirstOrDefaultAsync(p => p.Id == projectId);
                if (project == null)
                    throw new NotFoundException("Project", projectId);
            }
            toModify.RelatedProject = project;

            await context.SaveChangesAsync();
            return toModify;
        }
    }
}
