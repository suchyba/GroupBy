using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class DocumentRepository : AsyncRepository<Document>, IDocumentRepository
    {
        private readonly IProjectRepository projectRepository;
        private readonly IGroupRepository groupRepository;

        public DocumentRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator,
            IProjectRepository projectRepository,
            IGroupRepository groupRepository) : base(dBcontextLocator)
        {
            this.projectRepository = projectRepository;
            this.groupRepository = groupRepository;
        }
        public override async Task<Document> CreateAsync(Document domain)
        {
            // TODO add checking if the file pointed by FilePath exists

            var projectId = domain.RelatedProject?.Id;
            Project project = null;
            if (projectId != null)
            {
                project = await projectRepository.GetAsync(domain.RelatedProject);
                if (project == null)
                    throw new NotFoundException("Project", projectId);
            }
            domain.RelatedProject = project;

            var tempGroups = new List<Group>();
            foreach (var group in domain.Groups)
            {
                Group g = await groupRepository.GetAsync(group);

                if (g == null)
                    throw new NotFoundException("Group", group.Id);

                tempGroups.Add(g);
            }

            domain.Groups = tempGroups;

            var newDocument = await context.Set<Document>().AddAsync(domain);
            return newDocument.Entity;
        }

        public override async Task<Document> UpdateAsync(Document domain)
        {
            var toModify = await GetAsync(domain);
            toModify.Name = domain.Name;

            // to further check if this file exists
            toModify.FilePath = domain.FilePath;

            var projectId = domain.RelatedProject?.Id;
            Project project = null;
            if (projectId != null)
            {
                project = await projectRepository.GetAsync(domain.RelatedProject);
                if (project == null)
                    throw new NotFoundException("Project", projectId);
            }
            toModify.RelatedProject = project;

            return toModify;
        }
    }
}
