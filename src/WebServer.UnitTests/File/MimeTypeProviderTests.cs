﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.File;
using Devkoes.Restup.WebServer.UnitTests.TestHelpers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Devkoes.Restup.WebServer.UnitTests.File
{
    [TestClass]
    public class MimeTypeProviderTests
    {
        [TestMethod]
        public void ExtensionsShouldStartWithDot()
        {
            Assert.ThrowsException<Exception>(
                () => new MimeTypeProvider(new Dictionary<string, string> { { "txt", "bla" } })
            );
        }

        [TestMethod]
        public void GetMimeType_WhenFileDoesNotHaveContentType()
        {
            var expectedContentType = "application/test";
            var mimeTypeProvider = new MimeTypeProvider(new Dictionary<string, string> { { ".test", expectedContentType } });
            var mimeType = mimeTypeProvider.GetMimeType(new MockFile(string.Empty, null, ".test"));

            Assert.AreEqual(expectedContentType, mimeType);
        }

        [TestMethod]
        public void GetMimeType_WhenFileDoesNotHaveContentTypeAndExtensionDoesNotHaveSameCasing()
        {
            var expectedContentType = "application/test";
            var mimeTypeProvider = new MimeTypeProvider(new Dictionary<string, string> { { ".test", expectedContentType } });
            var mimeType = mimeTypeProvider.GetMimeType(new MockFile(string.Empty, null, ".tEsT"));

            Assert.AreEqual(expectedContentType, mimeType);
        }

        [TestMethod]
        public void GetMimeType_WhenFileHasContentTypeAndMimeTypeProviderHasnt()
        {
            var expectedContentType = "application/test";
            var mimeTypeProvider = new MimeTypeProvider(new Dictionary<string, string>());
            var mimeType = mimeTypeProvider.GetMimeType(new MockFile(string.Empty, expectedContentType, ".test"));

            Assert.AreEqual(expectedContentType, mimeType);
        }

        [TestMethod]
        public void GetMimeType_WhenBothFileAndMimeTypeProviderHaveGotContentType()
        {
            var expectedContentType = "application/test";
            var mimeTypeProvider = new MimeTypeProvider(new Dictionary<string, string> { { ".test", expectedContentType } });
            var mimeType = mimeTypeProvider.GetMimeType(new MockFile(string.Empty, "application/testing", ".test"));

            Assert.AreEqual(expectedContentType, mimeType);
        }
    }
}
