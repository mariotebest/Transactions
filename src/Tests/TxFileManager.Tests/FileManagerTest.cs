using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Xml;
using NUnit.Framework;
using TransactionalFileManager;

namespace TxFileManager.Tests
{
    public class FileManagerTest
    {
        private readonly int _numTempFiles;
        private readonly IFileManager _target;

        public FileManagerTest()
        {
            _target = new FileManager();
            _numTempFiles = Directory.GetFiles(Path.Combine(Path.GetTempPath(), "CdFileMgr")).Length;
        }


        private byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace("-", "").Replace(" ", "");
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private byte[] GetHelloWorld()
        {
            return StringToByteArray("68-65-6C-6C-6F-20-77-6F-72-6C-64");
        }

        private byte[] GetHelloWorldAgain()
        {
            return StringToByteArray("68-65-6C-6C-6F-20-61-67-61-69-6E-20-77-6F-72-6C-64");
        }

        public void Dispose()
        {
            var numTempFiles = Directory.GetFiles(Path.Combine(Path.GetTempPath(), "CdFileMgr")).Length;
            Assert.AreEqual(_numTempFiles, numTempFiles);
        }

        #region Operations
        [Test]
        public void CanAppendText()
        {
            var f1 = _target.GetTempFileName();
            const string contents = "123";

            try
            {
                using (var scope1 = new TransactionScope())
                {
                    _target.AppendAllText(f1, contents);
                    scope1.Complete();
                }
                Assert.AreEqual(contents, File.ReadAllText(f1));
            }
            finally
            {
                File.Delete(f1);
            }
        }

        [Test]
        public void CannotAppendText()
        {
            var f1 = _target.GetTempFileName();
            const string contents = "123";

            Assert.Throws<IOException>(() =>
            {
                try
                {
                    using (var scope1 = new TransactionScope())
                    {
                        using (var fs = File.Open(f1, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                        {
                            _target.AppendAllText(f1, contents);
                        }
                    }
                }
                finally
                {
                    File.Delete(f1);
                }
            });
        }

        [Test]
        public void CanAppendTextAndRollback()
        {
            var f1 = _target.GetTempFileName();
            const string contents = "qwerty";
            using (var sc1 = new TransactionScope())
            {
                _target.AppendAllText(f1, contents);
            }

            Assert.False(File.Exists(f1), f1 + " should not exist.");
        }

        [Test]
        public void CanCopy()
        {
            var sourceFileName = _target.GetTempFileName();
            var destFileName = _target.GetTempFileName();

            try
            {
                const string expectedText = "Test 123.";
                using (var scope1 = new TransactionScope())
                {
                    File.WriteAllText(sourceFileName, expectedText);
                    _target.Copy(sourceFileName, destFileName, false);
                    scope1.Complete();
                }

                Assert.AreEqual(expectedText, File.ReadAllText(sourceFileName));
                Assert.AreEqual(expectedText, File.ReadAllText(destFileName));
            }
            finally
            {
                File.Delete(sourceFileName);
                File.Delete(destFileName);
            }
        }

        [Test]
        public void CanCopyAndRollback()
        {
            var sourceFileName = _target.GetTempFileName();
            const string expectedText = "Hello 123.";
            File.WriteAllText(sourceFileName, expectedText);
            var destFileName = _target.GetTempFileName();

            try
            {
                using (var scope1 = new TransactionScope())
                {
                    _target.Copy(sourceFileName, destFileName, false);
                    // rollback
                }

                Assert.False(File.Exists(destFileName), destFileName + " should not exist.");
            }
            finally
            {
                File.Delete(sourceFileName);
                File.Delete(destFileName);
            }
        }

        [Test]
        public void CanCreateDirectory()
        {
            var d1 = _target.GetTempFileName();
            try
            {
                using (var scope1 = new TransactionScope())
                {
                    _target.CreateDirectory(d1);
                    scope1.Complete();
                }
                Assert.True(Directory.Exists(d1), d1 + " should exist.");
            }
            finally
            {
                Directory.Delete(d1);
            }
        }

        [Test]
        public void CanRollbackNestedDirectories()
        {
            var baseDir = _target.GetTempFileName(string.Empty);
            var nested1 = Path.Combine(baseDir, "level1");
            using (new TransactionScope())
            {
                _target.CreateDirectory(nested1);
            }
            Assert.False(Directory.Exists(baseDir), baseDir + " should not exist.");
        }

        [Test]
        public void CanCreateDirectoryAndRollback()
        {
            var d1 = _target.GetTempFileName();
            using (var scope1 = new TransactionScope())
            {
                _target.CreateDirectory(d1);
            }
            Assert.False(Directory.Exists(d1), d1 + " should not exist.");
        }

        [Test]
        public void CanDeleteDirectory()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                Directory.CreateDirectory(f1);

                using (var scope1 = new TransactionScope())
                {
                    _target.DeleteDirectory(f1);
                    scope1.Complete();
                }

                Assert.False(Directory.Exists(f1), f1 + " should no longer exist.");
            }
            finally
            {
                if (Directory.Exists(f1))
                {
                    Directory.Delete(f1, true);
                }
            }
        }

        [Test]
        public void CanDeleteDirectoryAndRollback()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                Directory.CreateDirectory(f1);

                using (var scope1 = new TransactionScope())
                {
                    _target.DeleteDirectory(f1);
                }

                Assert.True(Directory.Exists(f1), f1 + " should exist.");
            }
            finally
            {
                if (Directory.Exists(f1))
                {
                    Directory.Delete(f1, true);
                }
            }
        }

        [Test]
        public void CanDeleteFile()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                const string contents = "abc";
                File.WriteAllText(f1, contents);

                using (var scope1 = new TransactionScope())
                {
                    _target.Delete(f1);
                    scope1.Complete();
                }

                Assert.False(File.Exists(f1), f1 + " should no longer exist.");
            }
            finally
            {
                if (Directory.Exists(f1))
                {
                    Directory.Delete(f1, true);
                }
            }
        }

        [Test]
        public void CanDeleteFileAndRollback()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                const string contents = "abc";
                File.WriteAllText(f1, contents);

                using (var scope1 = new TransactionScope())
                {
                    _target.Delete(f1);
                }

                Assert.True(File.Exists(f1), f1 + " should exist.");
                Assert.AreEqual(contents, File.ReadAllText(f1));
            }
            finally
            {
                File.Delete(f1);
            }
        }

        [Test]
        public void CanMoveFile()
        {
            const string contents = "abc";
            var f1 = _target.GetTempFileName();
            var f2 = _target.GetTempFileName();
            try
            {
                File.WriteAllText(f1, contents);

                using (var scope1 = new TransactionScope())
                {
                    Assert.True(File.Exists(f1));
                    Assert.False(File.Exists(f2));
                    _target.Move(f1, f2);
                    scope1.Complete();
                }
            }
            finally
            {
                File.Delete(f1);
                File.Delete(f2);
            }
        }

        [Test]
        public void CanMoveFileAndRollback()
        {
            const string contents = "abc";
            var f1 = _target.GetTempFileName();
            var f2 = _target.GetTempFileName();
            try
            {
                File.WriteAllText(f1, contents);

                using (var scope1 = new TransactionScope())
                {
                    Assert.True(File.Exists(f1));
                    Assert.False(File.Exists(f2));
                    _target.Move(f1, f2);
                }

                Assert.AreEqual(contents, File.ReadAllText(f1));
                Assert.False(File.Exists(f2));
            }
            finally
            {
                File.Delete(f1);
                File.Delete(f2);
            }
        }

        [Test]
        public void CanSnapshot()
        {
            var f1 = _target.GetTempFileName();

            using (var scope1 = new TransactionScope())
            {
                _target.Snapshot(f1);

                _target.AppendAllText(f1, "<test></test>");
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(f1);
            }

            Assert.False(File.Exists(f1), f1 + " should not exist.");
        }

        [Test]
        public void CanWriteAllText()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                const string contents = "abcdef";
                File.WriteAllText(f1, "123");

                using (var scope1 = new TransactionScope())
                {
                    _target.WriteAllText(f1, contents);
                    scope1.Complete();
                }

                Assert.AreEqual(contents, File.ReadAllText(f1));
            }
            finally
            {
                File.Delete(f1);
            }
        }

        [Test]
        public void CanWriteAllTextAndRollback()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                const string contents1 = "123";
                const string contents2 = "abcdef";
                File.WriteAllText(f1, contents1);

                using (var scope1 = new TransactionScope())
                {
                    _target.WriteAllText(f1, contents2);
                }

                Assert.AreEqual(contents1, File.ReadAllText(f1));
            }
            finally
            {
                File.Delete(f1);
            }
        }

        [Test]
        public void Scratch()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                Directory.CreateDirectory(f1);

                using (var scope1 = new TransactionScope())
                {
                    _target.DeleteDirectory(f1);
                    scope1.Complete();
                }

                Assert.False(Directory.Exists(f1), f1 + " should no longer exist.");
            }
            finally
            {
                if (Directory.Exists(f1))
                {
                    Directory.Delete(f1, true);
                }
            }            
        }

        [Test]
        public void CanWriteAllBytes()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                var contents = GetHelloWorldAgain();
                File.WriteAllBytes(f1, GetHelloWorld());

                using (var scope1 = new TransactionScope())
                {
                    _target.WriteAllBytes(f1, contents);
                    scope1.Complete();
                }

                Assert.AreEqual(contents, File.ReadAllBytes(f1));
            }
            finally
            {
                File.Delete(f1);
            }
        }

        [Test]
        public void CanWriteAllBytesAndRollback()
        {
            var f1 = _target.GetTempFileName();
            try
            {
                var contents1 = GetHelloWorld();
                var contents2 = GetHelloWorldAgain();
                File.WriteAllBytes(f1, contents1);

                using (var scope1 = new TransactionScope())
                {
                    _target.WriteAllBytes(f1, contents2);
                }

                Assert.AreEqual(contents1, File.ReadAllBytes(f1));
            }
            finally
            {
                File.Delete(f1);
            }
        }

        #endregion

        #region Error Handling

        [Test]
        public void CanHandleCopyErrors()
        {
            var f1 = _target.GetTempFileName();
            var f2 = _target.GetTempFileName();

            var fs = new FileStream(f2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            try
            {
                const string expectedText = "Test 123.";
                using (var scope1 = new TransactionScope())
                {
                    File.WriteAllText(f1, expectedText);

                    try
                    {
                        _target.Copy(f1, f2, false);
                    }
                    catch (IOException)
                    {
                        // Ignore IOException
                    }

                    //rollback
                }

            }
            finally
            {
                File.Delete(f1);
                fs.Close();
                File.Delete(f2);
            }
        }

        #endregion

        #region Transaction Support

        [Test]
        public void CannotRollback()
        {
            var f1 = _target.GetTempFileName(".txt");
            var f2 = _target.GetTempFileName(".txt");

            Assert.Throws<TransactionException>(() =>
            {
                try
                {
                    using (var scope1 = new TransactionScope())
                    {
                        _target.WriteAllText(f1, "Test.");
                        _target.WriteAllText(f2, "Test.");

                        var fi1 = new FileInfo(f1);
                        fi1.Attributes = FileAttributes.ReadOnly;

                        // rollback
                    }
                }
                finally
                {
                    var fi1 = new FileInfo(f1);
                    fi1.Attributes = FileAttributes.Normal;
                    File.Delete(f1);
                }
            });
        }

        [Test]
        public void CanReuseManager()
        {
            {
                var sourceFileName = _target.GetTempFileName();
                File.WriteAllText(sourceFileName, "Hello.");
                var destFileName = _target.GetTempFileName();

                try
                {
                    using (var scope1 = new TransactionScope())
                    {
                        _target.Copy(sourceFileName, destFileName, false);

                        // rollback
                    }

                    Assert.False(File.Exists(destFileName), destFileName + " should not exist.");
                }
                finally
                {
                    File.Delete(sourceFileName);
                    File.Delete(destFileName);
                }
            }

            {
                var sourceFileName = _target.GetTempFileName();
                File.WriteAllText(sourceFileName, "Hello.");
                var destFileName = _target.GetTempFileName();

                try
                {
                    using (var scope1 = new TransactionScope())
                    {
                        _target.Copy(sourceFileName, destFileName, false);

                        // rollback
                    }

                    Assert.False(File.Exists(destFileName), destFileName + " should not exist.");
                }
                finally
                {
                    File.Delete(sourceFileName);
                    File.Delete(destFileName);
                }
            }
        }

        [Test]
        public void CanSupportTransactionScopeOptionSuppress()
        {
            const string contents = "abc";
            var f1 = _target.GetTempFileName(".txt");
            try
            {
                using (var scope1 = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    _target.WriteAllText(f1, contents);
                }

                Assert.AreEqual(contents, File.ReadAllText(f1));
            }
            finally
            {
                File.Delete(f1);
            }
        }

        [Test]
        public void CanDoMultiThread()
        {
            const int numThreads = 10;
            var threads = new List<Thread>();
            for (var i = 0; i < numThreads; i++)
            {
                threads.Add(new Thread(CanAppendText));
                threads.Add(new Thread(CanAppendTextAndRollback));
                threads.Add(new Thread(CanCopy));
                threads.Add(new Thread(CanCopyAndRollback));
                threads.Add(new Thread(CanCreateDirectory));
                threads.Add(new Thread(CanCreateDirectoryAndRollback));
                threads.Add(new Thread(CanDeleteFile));
                threads.Add(new Thread(CanDeleteFileAndRollback));
                threads.Add(new Thread(CanMoveFile));
                threads.Add(new Thread(CanMoveFileAndRollback));
                threads.Add(new Thread(CanSnapshot));
                threads.Add(new Thread(CanWriteAllText));
                threads.Add(new Thread(CanWriteAllTextAndRollback));
            }

            foreach (var t in threads)
            {
                t.Start();
                t.Join();
            }
        }

        [Test]
        public void CanNestTransactions()
        {
            var f1 = _target.GetTempFileName(".txt");
            const string f1Contents = "f1";
            var f2 = _target.GetTempFileName(".txt");
            const string f2Contents = "f2";
            var f3 = _target.GetTempFileName(".txt");
            const string f3Contents = "f3";

            try
            {
                using (var sc1 = new TransactionScope())
                {
                    _target.WriteAllText(f1, f1Contents);

                    using (var sc2 = new TransactionScope())
                    {
                        _target.WriteAllText(f2, f2Contents);
                        sc2.Complete();
                    }

                    using (var sc3 = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        _target.WriteAllText(f3, f3Contents);
                        sc3.Complete();
                    }

                    sc1.Dispose();
                }

                Assert.False(File.Exists(f1));
                Assert.False(File.Exists(f2));
                Assert.True(File.Exists(f3));
            }
            finally
            {
                File.Delete(f1);
                File.Delete(f2);
                File.Delete(f3);
            }
        }

        #endregion

    }
}
