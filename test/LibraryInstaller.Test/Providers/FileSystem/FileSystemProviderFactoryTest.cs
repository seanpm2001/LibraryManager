﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Web.LibraryInstaller.Contracts;
using Microsoft.Web.LibraryInstaller.Mocks;
using Microsoft.Web.LibraryInstaller.Providers.FileSystem;

namespace Microsoft.Web.LibraryInstaller.Test.Providers.Cdnjs
{
    [TestClass]
    public class FileSystemProviderFactoryTest
    {
        private IHostInteraction _hostInteraction;

        [TestInitialize]
        public void Setup()
        {
            string cacheFolder = Environment.ExpandEnvironmentVariables(@"%localappdata%\Microsoft\Library\");
            string projectFolder = Path.Combine(Path.GetTempPath(), "LibraryInstaller");
            _hostInteraction = new HostInteraction(projectFolder, cacheFolder);
        }

        [TestMethod]
        public void CreateProvider_Success()
        {
            var factory = new FileSystemProviderFactory();
            IProvider provider = factory.CreateProvider(_hostInteraction);

            Assert.AreSame(_hostInteraction.WorkingDirectory, provider.HostInteraction.WorkingDirectory);
            Assert.AreSame(_hostInteraction.CacheDirectory, provider.HostInteraction.CacheDirectory);
            Assert.IsFalse(string.IsNullOrEmpty(provider.Id));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateProvider_NullParameter()
        {
            var factory = new FileSystemProviderFactory();
            IProvider provider = factory.CreateProvider(null);
        }
    }
}