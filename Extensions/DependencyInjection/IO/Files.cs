using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SamHowes.Extensions.DependencyInjection.IO
{
    public class Files
    {
        public virtual StreamReader OpenText(string path)
            => File.OpenText(path);

        public virtual StreamWriter CreateText(string path)
            => File.CreateText(path);

        public virtual StreamWriter AppendText(string path)
            => File.AppendText(path);

        public virtual void Copy(string sourceFileName, string destFileName)
            => File.Copy(sourceFileName, destFileName);

        public virtual void Copy(string sourceFileName, string destFileName, bool overwrite)
            => File.Copy(sourceFileName, destFileName, overwrite);

        public virtual FileStream Create(string path)
            => File.Create(path);

        public virtual FileStream Create(string path, int bufferSize)
            => File.Create(path, bufferSize);

        public virtual FileStream Create(string path, int bufferSize, FileOptions options)
            => File.Create(path, bufferSize, options);

        public virtual void Delete(string path)
            => File.Delete(path);

        public virtual bool Exists(string path)
            => File.Exists(path);

        public virtual FileStream Open(string path, FileMode mode)
            => File.Open(path, mode);

        public virtual FileStream Open(string path, FileMode mode, FileAccess access)
            => File.Open(path, mode, access);

        public virtual FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
            => File.Open(path, mode, access, share);

        public virtual void SetCreationTime(string path, DateTime creationTime)
            => File.SetCreationTime(path, creationTime);

        public virtual void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
            => File.SetCreationTimeUtc(path, creationTimeUtc);

        public virtual DateTime GetCreationTime(string path)
            => File.GetCreationTime(path);

        public virtual DateTime GetCreationTimeUtc(string path)
            => File.GetCreationTimeUtc(path);

        public virtual void SetLastAccessTime(string path, DateTime lastAccessTime)
            => File.SetLastAccessTime(path, lastAccessTime);

        public virtual void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
            => File.SetLastAccessTimeUtc(path, lastAccessTimeUtc);

        public virtual DateTime GetLastAccessTime(string path)
            => File.GetLastAccessTime(path);

        public virtual DateTime GetLastAccessTimeUtc(string path)
            => File.GetLastAccessTimeUtc(path);

        public virtual void SetLastWriteTime(string path, DateTime lastWriteTime)
            => File.SetLastWriteTime(path, lastWriteTime);

        public virtual void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
            => File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);

        public virtual DateTime GetLastWriteTime(string path)
            => File.GetLastWriteTime(path);

        public virtual DateTime GetLastWriteTimeUtc(string path)
            => File.GetLastWriteTimeUtc(path);

        public virtual FileAttributes GetAttributes(string path)
            => File.GetAttributes(path);

        public virtual void SetAttributes(string path, FileAttributes fileAttributes)
            => File.SetAttributes(path, fileAttributes);

        public virtual FileStream OpenRead(string path)
            => File.OpenRead(path);

        public virtual FileStream OpenWrite(string path)
            => File.OpenWrite(path);

        public virtual string ReadAllText(string path)
            => File.ReadAllText(path);

        public virtual string ReadAllText(string path, Encoding encoding)
            => File.ReadAllText(path, encoding);

        public virtual void WriteAllText(string path, string contents)
            => File.WriteAllText(path, contents);

        public virtual void WriteAllText(string path, string contents, Encoding encoding)
            => File.WriteAllText(path, contents, encoding);

        public virtual byte[] ReadAllBytes(string path)
            => File.ReadAllBytes(path);

        public virtual void WriteAllBytes(string path, byte[] bytes)
            => File.WriteAllBytes(path, bytes);

        public virtual string[] ReadAllLines(string path)
            => File.ReadAllLines(path);

        public virtual string[] ReadAllLines(string path, Encoding encoding)
            => File.ReadAllLines(path, encoding);

        public virtual IEnumerable<string> ReadLines(string path)
            => File.ReadLines(path);

        public virtual IEnumerable<string> ReadLines(string path, Encoding encoding)
            => File.ReadLines(path, encoding);

        public virtual void WriteAllLines(string path, string[] contents)
            => File.WriteAllLines(path, contents);

        public virtual void WriteAllLines(string path, IEnumerable<string> contents)
            => File.WriteAllLines(path, contents);

        public virtual void WriteAllLines(string path, string[] contents, Encoding encoding)
            => File.WriteAllLines(path, contents, encoding);

        public virtual void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            => File.WriteAllLines(path, contents, encoding);

        public virtual void AppendAllText(string path, string contents)
            => File.AppendAllText(path, contents);

        public virtual void AppendAllText(string path, string contents, Encoding encoding)
            => File.AppendAllText(path, contents, encoding);

        public virtual void AppendAllLines(string path, IEnumerable<string> contents)
            => File.AppendAllLines(path, contents);

        public virtual void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            => File.AppendAllLines(path, contents, encoding);

        public virtual void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
            => File.Replace(sourceFileName, destinationFileName, destinationBackupFileName);

        public virtual void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
            => File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);

        public virtual void Move(string sourceFileName, string destFileName)
            => File.Move(sourceFileName, destFileName);

        public virtual void Move(string sourceFileName, string destFileName, bool overwrite)
            => File.Move(sourceFileName, destFileName, overwrite);

        public virtual void Encrypt(string path)
            => File.Encrypt(path);

        public virtual void Decrypt(string path)
            => File.Decrypt(path);

        public virtual Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken)
            => File.ReadAllTextAsync(path, cancellationToken);

        public virtual Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken)
            => File.ReadAllTextAsync(path, encoding, cancellationToken);

        public virtual Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken)
            => File.WriteAllTextAsync(path, contents, cancellationToken);

        public virtual Task WriteAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken)
            => File.WriteAllTextAsync(path, contents, encoding, cancellationToken);

        public virtual Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken)
            => File.ReadAllBytesAsync(path, cancellationToken);

        public virtual Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken)
            => File.WriteAllBytesAsync(path, bytes, cancellationToken);

        public virtual Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken)
            => File.ReadAllLinesAsync(path, cancellationToken);

        public virtual Task<string[]> ReadAllLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken)
            => File.ReadAllLinesAsync(path, encoding, cancellationToken);

        public virtual Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken)
            => File.WriteAllLinesAsync(path, contents, cancellationToken);

        public virtual Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken)
            => File.WriteAllLinesAsync(path, contents, encoding, cancellationToken);

        public virtual Task AppendAllTextAsync(string path, string contents, CancellationToken cancellationToken)
            => File.AppendAllTextAsync(path, contents, cancellationToken);

        public virtual Task AppendAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken)
            => File.AppendAllTextAsync(path, contents, encoding, cancellationToken);

        public virtual Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken)
            => File.AppendAllLinesAsync(path, contents, cancellationToken);

        public virtual Task AppendAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken)
            => File.AppendAllLinesAsync(path, contents, encoding, cancellationToken);
    }
}