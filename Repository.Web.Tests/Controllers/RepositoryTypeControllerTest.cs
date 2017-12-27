using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Repository.DataLayer.Context;
using Repository.DomainModel.Repository;
using Repository.ServiceLayer.Contracts;
using Repository.Web.Controllers;
using Repository.Web.Tests.TestHelper;
using System;
using System.Collections.Generic;

namespace Repository.Web.Tests.Controllers
{
    [TestClass]
    public class RepositoryTypeControllerTest
    {
        #region variale
        static Mock<IRepositoryTypeService> _repositoryTypeService;
        static Mock<IUnitOfWork> _uow;
        static Mock<RepositoryTypesController> _controller;
        static List<RepositoryType> _repositoryTypes;
        #endregion
        [TestFixtureSetUp]
        public void SetUp()
        {
            _repositoryTypes = DataInitializer.GetAllRepositoryTypes();
        }
        [TestInitialize]
        public void Initialize()
        {
            _repositoryTypeService = new Mock<IRepositoryTypeService>();
            _uow = new Mock<IUnitOfWork>();
            _controller = new Mock<RepositoryTypesController>(_uow.Object, _repositoryTypeService.Object);
        }

        [TestMethod]
        public void Get_Data()
        {
            //Arrange
            _repositoryTypes = DataInitializer.GetAllRepositoryTypes();
            _repositoryTypeService.Setup(p => p.GetAll()).Returns(_repositoryTypes);

            IList<RepositoryType> list = _repositoryTypeService.Object.GetAll();

            //ACT

            //ASSERT
            NUnit.Framework.Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void Add_RepositoryType()
        {

            //_repositoryTypeService.Setup(r=>r.Add((It.IsAny<RepositoryType>())))
            //   .Callback(new Action<RepositoryType>())
            _repositoryTypes = DataInitializer.GetAllRepositoryTypes();
            _repositoryTypeService.Setup(p => p.GetAll()).Returns(_repositoryTypes);

            IList<RepositoryType> list = _repositoryTypeService.Object.GetAll();
            List<RepositoryType> repList =new List<RepositoryType> { DataInitializer.repsitoryTypeEntity };

            _repositoryTypeService.Object.Add(DataInitializer.repsitoryTypeEntity);

            //ASSERT
            NUnit.Framework.Assert.AreEqual(4, list.Count+1);
        }
        [TestFixtureTearDown]
        public void DisposeAllObject()
        {
            _repositoryTypes = null;
        }
    }
}
