using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Dependencies.Interfaces
{
    /// <summary>
    /// Dependency to get and set the general information about the organisation
    /// </summary>
    interface IGeneralInformation
    {
        /// <summary>
        /// Method to get the administrator (owner of the organisation) id
        /// </summary>
        /// <returns>Id of organisation's administrator from database</returns>
        public string GetAdministratorId();
        /// <summary>
        /// Method to get the full name of the organisation
        /// </summary>
        /// <returns>Full name of the organisation</returns>
        public string GetOrganisationFullName();
        /// <summary>
        /// Method to get the short version of the name of the organisation
        /// </summary>
        /// <returns></returns>
        public string GetOrganisationShortName();
        /// <summary>
        /// Method to set the administrator of the organisation
        /// </summary>
        /// <param name="id">New id of administrator</param>
        public void SetAdministratorId(string id);
        /// <summary>
        /// Method to set the name of organisation
        /// </summary>
        /// <param name="name">New full name of organisation</param>
        public void SetOrganisationFullName(string name);
        /// <summary>
        /// Method to set the short version of the organisation name
        /// </summary>
        /// <param name="name">New short organisation name</param>
        public void SetOgranisationShortName(string name);
    }
}
