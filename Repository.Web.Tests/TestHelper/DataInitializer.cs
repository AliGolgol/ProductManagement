using Repository.DomainModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Web.Tests.TestHelper
{
    public class DataInitializer
    {
        RepositoryType rep;
        public static List<RepositoryType> GetAllRepositoryTypes()
        {
            var repositoryTypes = new List<RepositoryType> {
                    new RepositoryType() {Id=1,Name="item1" },
                    new RepositoryType() {Id=2,Name="item2" },
                    new RepositoryType() {Id=3,Name="item3" }
            };

            return repositoryTypes;
        }

        public static RepositoryType repsitoryTypeEntity
        {
            get
            {
                return new RepositoryType { Id = 4, Name = "item4" };
            }
        }
    }
}
