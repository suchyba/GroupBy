using GroupBy.Data.Dependencies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Dependencies.Implementation
{
    class GeneralInformation : IGeneralInformation
    {
        public string GetAdministratorId()
        {
            throw new NotImplementedException();
        }

        public string GetOrganisationFullName()
        {
            throw new NotImplementedException();
        }

        public string GetOrganisationShortName()
        {
            throw new NotImplementedException();
        }

        public void SetAdministratorId(string id)
        {
            throw new NotImplementedException();
        }

        public void SetOgranisationShortName(string name)
        {
            throw new NotImplementedException();
        }

        public void SetOrganisationFullName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
