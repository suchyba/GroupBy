using GroupBy.Data.Dependencies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Dependencies.Implementation
{
    class GeneralInformationDevelopment : IGeneralInformation
    {
        string Administrator;
        string OrganisationFullName;
        string OrganisationShortName;
        public string GetAdministratorId()
        {
            return "00000000000";
        }

        public string GetOrganisationFullName()
        {
            return "Development Ogranisation";
        }

        public string GetOrganisationShortName()
        {
            return "DO";
        }

        public void SetAdministratorId(string id)
        {
            Administrator = id;
        }

        public void SetOgranisationShortName(string name)
        {
            OrganisationShortName = name;
        }

        public void SetOrganisationFullName(string name)
        {
            OrganisationFullName = name;
        }
    }
}
